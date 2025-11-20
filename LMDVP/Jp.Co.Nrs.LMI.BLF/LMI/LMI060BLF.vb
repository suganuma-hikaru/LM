' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060  : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI060BLC = New LMI060BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectUnchinRecastDataメソッドに飛ぶ</remarks>
    Private Function SelectUnchinRecastData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectUnchinRecastData", ds)

    End Function

    ''' <summary>
    ''' 作成処理時、既存データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectUnchinRecastDataメソッドに飛ぶ</remarks>
    Private Function MakeDataCHK(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "MakeDataCHK", ds)

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        'データ削除処理
        Return Me.SimpleScopeStartEnd(ds, "DeleteSaveAction")

    End Function

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function MakeData(ByVal ds As DataSet) As DataSet

        '作成対象データの取得
        Dim rtnDs As DataSet = MyBase.CallBLC(Me._Blc, "SelectMakeData", ds)

        If rtnDs.Tables("LMI060INOUT").Rows.Count = 0 Then
            '作成対象データが0件の場合は終了
            MyBase.SetMessage("G001")
            Return ds
        End If

        'TODO 別メソッド化
        '風袋加算対応 風袋加算を加えた重量を計算する
        For Each row As DataRow In rtnDs.Tables("LMI060INOUT").Rows

            If (String.IsNullOrEmpty(row.Item("FUTAI").ToString()) = False) Then
                Dim futai As Decimal = Convert.ToDecimal(row.Item("FUTAI").ToString)
                Dim wt As Decimal = Convert.ToDecimal(row.Item("WT").ToString)
#If True Then   'UPD 2019/05/15 ]004183【LMS】特定荷主機能_群馬大阪三井化学 運賃請求1割増機能_計算差異(営業藤島/SYS山本)
                '①風袋の加算式がおかしい											
                '　現状：出荷(中)1レコードあたり、風袋を加算											
                '　改修：個数で乗算が必要
                If (String.IsNullOrEmpty(row.Item("TTL_NB").ToString()) = False) Then
                    Dim dTTL_NB As Decimal = Convert.ToDecimal(row.Item("TTL_NB").ToString)

                    wt = wt + (dTTL_NB * futai)

                Else
                    wt = wt + futai
                End If

#Else
                wt = wt + futai
#End If

                '重量を更新する
                row.Item("WT") = wt

            End If
        Next

        '風袋加算された重量でSEIQ_WTを再計算
        For i As Integer = 0 To rtnDs.Tables("LMI060INOUT").Rows.Count - 1

            If (String.IsNullOrEmpty(rtnDs.Tables("LMI060INOUT").Rows(i).Item("FUTAI").ToString()) = False) Then

                '運送管理番号Lでデータを抽出
                Dim drs As DataRow() = rtnDs.Tables("LMI060INOUT").Select(String.Concat("UNSO_NO_L = '", rtnDs.Tables("LMI060INOUT").Rows(i).Item("UNSO_NO_L").ToString(), "' "))
                Dim sumWt As Decimal = 0

                For j As Integer = 0 To drs.Count - 1
                    sumWt = sumWt + Convert.ToDecimal(drs(j).Item("WT").ToString())

                Next

                If (sumWt > 0) Then
                    rtnDs.Tables("LMI060INOUT").Rows(i).Item("SEIQ_WT") = sumWt
                End If

            End If
        Next

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '作成処理
            ds = MyBase.CallBLC(Me._Blc, "MakeData", rtnDs)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI060IN"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printType As String = dt.Rows(0).Item("PRINT_KB").ToString()

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        '要望番号:1482 KIM 2012/10/10 START
        'For i As Integer = 0 To max
        '要望番号:1482 KIM 2012/10/10 END

        '印刷処理
        setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt Is Nothing = False Then
            cnt = setRptDt.Rows.Count - 1
            For j As Integer = 0 To cnt
                rtnDt.ImportRow(setRptDt.Rows(j))
            Next
        End If

        '要望番号:1482 KIM 2012/10/10 START
        'Next
        '要望番号:1482 KIM 2012/10/10 END

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        Select Case printType

            Case "01" '運賃請求明細書(三井化学ポリウレタン)

                prtBlc = New Com.Base.BaseBLC() {New LMI510BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI510InData(ds)}

        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If
        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMI510DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI510InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMI510InData(ds, New LMI510DS(), "LMI510IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI510InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)

        '要望番号:1482 KIM 2012/10/10 START
        'Dim dr As DataRow = dt.NewRow()
        'Dim setDr As DataRow = ds.Tables("LMI060IN").Rows(0)
        'dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        'dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        'dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        'dr.Item("F_DATE") = setDr.Item("DATE_FROM").ToString()
        'dr.Item("T_DATE") = setDr.Item("DATE_TO").ToString()
        ''START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        'dr.Item("CUST_CD_S") = setDr.Item("CUST_CD_S").ToString()
        'dr.Item("CUST_CD_SS") = setDr.Item("CUST_CD_SS").ToString()
        'dt.Rows.Add(dr)
        ''END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

        Dim dr As DataRow = Nothing
        Dim setDr As DataRow = Nothing
        For i As Integer = 0 To ds.Tables("LMI060IN").Rows.Count - 1
            dr = dt.NewRow()
            setDr = ds.Tables("LMI060IN").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
            dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
            dr.Item("F_DATE") = setDr.Item("DATE_FROM").ToString()
            dr.Item("T_DATE") = setDr.Item("DATE_TO").ToString()
            dr.Item("CUST_CD_S") = setDr.Item("CUST_CD_S").ToString()
            dr.Item("CUST_CD_SS") = setDr.Item("CUST_CD_SS").ToString()
            dt.Rows.Add(dr)
        Next
        '要望番号:1482 KIM 2012/10/10 END

        Return inDs

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SimpleScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region

End Class