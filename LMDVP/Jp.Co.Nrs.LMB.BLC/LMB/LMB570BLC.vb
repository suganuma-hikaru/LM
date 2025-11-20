' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB570    : コンテナ番号ラベル印刷
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB570BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB570BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB570DAC = New LMB570DAC()


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

        ' 元データ
        Dim tableNmIn As String = "LMB570IN"
        Dim tableNmOut As String = "LMB570OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        ' 別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        ' ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

        ' IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            ' 0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        ' TSMC請求明細データテーブル 存在チェック
        Dim isSekyMeisaiTsmcExists As Boolean = CheckSekyMeisaiTsmcExists(ds)
        Dim countAll As Integer = 0

        For i As Integer = 0 To max

            ' 値のクリア
            setDs.Clear()

            ' 条件の設定
            inTbl.ImportRow(dt.Rows(i))

            Dim count As Integer = 0

            If isSekyMeisaiTsmcExists Then
                ' 使用帳票ID取得
                setDs = Me.SelectMPrt(setDs)
                count = MyBase.GetResultCount()
            End If

            If count < 1 Then
                ' 0件の場合
                If inTbl.Rows(0).Item("INKA_NO_M").ToString().Trim().Length = 0 Then
                    ' 入荷管理番号(中) に値なしの場合(入荷検索よりの印刷の場合)
                    MyBase.SetMessageStore("00", "E078", New String() {String.Concat("コンテナ番号ラベル ", inTbl.Rows(0).Item("INKA_NO_L").ToString())})
                End If
                Continue For
            End If

            countAll += count

            ' 検索結果取得
            setDs = Me.SelectPrintData(setDs)

            ' 検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            ' 取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            ' OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
            Next

            ' RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        If countAll = 0 Then
            MyBase.SetMessage("G021")
        End If

        ' ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        ' ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")
        ' ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone
        ' ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        ' キーブレイク用(NRS_BR_CD, PTN_ID, RPT_ID)
        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        ' 重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする)
        For l As Integer = 0 To sortDtRpt.Rows.Count - 1
            ' 営業所コード、パターンID、レポートID が一致するレコードは除外する
            If keyBrCd <> sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString() OrElse
               keyPtnId <> sortDtRpt.Rows(l).Item("PTN_ID").ToString() OrElse
               keyRptId <> sortDtRpt.Rows(l).Item("RPT_ID").ToString() Then

                ' 営業所コード、パターンID、レポートID のいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next


        ' レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            ' レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            ' 印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            ' 指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMB570OUT"), dr.Item("RPT_ID").ToString())
            ' 帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            ' 帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                                  dr.Item("PTN_ID").ToString(),
                                  dr.Item("PTN_CD").ToString(),
                                  dr.Item("RPT_ID").ToString(),
                                  prtDs.Tables("LMB570OUT"),
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

        Select Case prtId
            Case ""
            Case Else

        End Select

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細データテーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckSekyMeisaiTsmcExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMB570_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMB570_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMB570IN").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "I_SEKY_MEISAI_TSMC"
        ds.Tables("LMB570_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMB570_TBL_EXISTS").Select("TBL_NM = 'I_SEKY_MEISAI_TSMC'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

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

#End Region ' "印刷処理"

#End Region ' "Method"

End Class
