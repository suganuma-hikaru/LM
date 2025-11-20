' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC950    : 新潟運輸CSV出力
'  作  成  者       :  []
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC950BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC950BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC950DAC = New LMC950DAC()

#End Region

#Region "Method"

#Region "CSVデータ取得処理"


    ''' <summary>
    ''' CSVデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function NiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        ' 印刷対象データの取得
        Dim rtnDs As DataSet = Me.SelectNiigataUnyuCsv(ds)

        ' 出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC950OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        ' 取得値の加工を行う（SQLでできないか煩雑なもの）
        For i As Integer = 0 To rtnDs.Tables("LMC950OUT").Rows.Count - 1
            Dim dr As DataRow = rtnDs.Tables("LMC950OUT").Rows(i)

            ' 届け先名の分割
            If dr.Item("DEST_NM_1").ToString().Length > 22 Then
                Dim destNm As String = dr.Item("DEST_NM_1").ToString()
                dr.Item("DEST_NM_1") = destNm.Substring(0, 22)
                If destNm.Length > (22 + 22) Then
                    dr.Item("DEST_NM_2") = destNm.Substring(22, 22)
                Else
                    dr.Item("DEST_NM_2") = destNm.Substring(22)
                End If
            End If

            ' 住所1～2 の分割
            If dr.Item("DEST_AD_1").ToString().Length > 22 Then
                Dim destAd As String =
                    String.Concat(dr.Item("DEST_AD_1").ToString(), dr.Item("DEST_AD_2").ToString())
                dr.Item("DEST_AD_1") = destAd.Substring(0, 22)
                If destAd.Length > (22 + 22) Then
                    dr.Item("DEST_AD_2") = destAd.Substring(22, 22)
                Else
                    dr.Item("DEST_AD_2") = destAd.Substring(22)
                End If
            End If

            ' 住所3 の桁調整
            If dr.Item("DEST_AD_3").ToString().Length > 22 Then
                dr.Item("DEST_AD_3") = dr.Item("DEST_AD_3").ToString().Substring(0, 22)
            End If

            ' 「"扱い : " + 荷主名」の編集と桁調整
            Dim atsukaisyaNm As String = String.Concat("扱い : ", dr.Item("ATSUKAISYA_NM").ToString())
            If atsukaisyaNm.Length > 30 Then
                dr.Item("ATSUKAISYA_NM") = atsukaisyaNm.Substring(0, 30)
            Else
                dr.Item("ATSUKAISYA_NM") = atsukaisyaNm
            End If

            ' 商品名の桁調整
            If dr.Item("GOODS_NM_1").ToString().Length > 30 Then
                dr.Item("GOODS_NM_1") = dr.Item("GOODS_NM_1").ToString().Substring(0, 30)
            End If

            ' 「入り目 + 入り目単位」の桁調整
            Dim irime As String =
                String.Concat(
                    CutLastZeroOfAfterTheDecimalPoint(dr.Item("IRIME").ToString()),
                    dr.Item("IRIME_UT").ToString())
            If irime.Length > 30 Then
                dr.Item("IRIME") = irime.Substring(0, 30)
            Else
                dr.Item("IRIME") = irime
            End If
            dr.Item("IRIME_UT") = ""

            ' 「"【配達指定】 " + 配達指定」の編集と桁調整
            If dr.Item("ARR_PLAN_TIME").ToString().Length > 0 Then
                Dim arrPlanTime As String = String.Concat("【配達指定】", dr.Item("ARR_PLAN_TIME").ToString())
                If arrPlanTime.Length > 30 Then
                    dr.Item("ARR_PLAN_TIME") = arrPlanTime.Substring(0, 30)
                Else
                    dr.Item("ARR_PLAN_TIME") = arrPlanTime
                End If
            End If

            ' 配送時注意事項の桁調整
            If dr.Item("UNSO_L_REMARK").ToString().Length > 30 Then
                dr.Item("UNSO_L_REMARK") = dr.Item("UNSO_L_REMARK").ToString().Substring(0, 30)
            End If

            If dr.Item("CUST_ORD_NO").ToString().Length > 0 Then
                ' 「"ORD : " + オーダー番号」の桁調整の編集と桁調整
                Dim custOrdNo As String = String.Concat("ORD : ", dr.Item("CUST_ORD_NO").ToString())
                If custOrdNo.Length > 30 Then
                    dr.Item("CUST_ORD_NO") = custOrdNo.Substring(0, 30)
                Else
                    dr.Item("CUST_ORD_NO") = custOrdNo
                End If
            End If

            If dr.Item("BUYER_ORD_NO").ToString().Length > 0 Then
                ' 「"注文番号 : " + 注文番号」の桁調整の編集と桁調整
                Dim buyerOrdNo As String = String.Concat("注文番号 : ", dr.Item("BUYER_ORD_NO").ToString())
                If buyerOrdNo.Length > 30 Then
                    dr.Item("BUYER_ORD_NO") = buyerOrdNo.Substring(0, 30)
                Else
                    dr.Item("BUYER_ORD_NO") = buyerOrdNo
                End If
            End If

            ' 運送重量の端数調整
            Dim unsoWt As Decimal = Math.Truncate(Decimal.Parse(dr.Item("UNSO_WT").ToString()) + 0.999D)
            dr.Item("UNSO_WT") = unsoWt.ToString()
        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectSLineCsvメソッド呼出</remarks>
    Private Function SelectNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC950IN")
        Dim outDt As DataTable = ds.Tables("LMC950OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC950IN")
        Dim setDt As DataTable = setDs.Tables("LMC950OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectNiigataUnyuCsv")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新（新潟運輸CSV作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateNiigataUnyuCsv", ds)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

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

#Region "文字列編集"
    ''' <summary>
    ''' 数値を表す文字列の小数点以下の末尾のゼロをカットする。
    ''' E.g. "10.000" → "10", "4.850" → "4.85",  "9.2" → "9.2"
    ''' </summary>
    ''' <param name="src"></param>
    ''' <returns></returns>
    Private Function CutLastZeroOfAfterTheDecimalPoint(ByVal src As String) As String

        If Not IsNumeric(src) Then
            ' 数値以外
            Return src
        End If
        If src.LastIndexOf(".") < 0 Then
            ' 小数点以下なし
            Return src
        End If

        Dim cutLen As Integer = 0
        For pos As Integer = src.Length - 1 To src.LastIndexOf(".") Step -1
            If (Not (src.Substring(pos, 1).Equals("0") OrElse src.Substring(pos, 1).Equals("."))) Then
                Exit For
            End If
            cutLen += 1
        Next

        Dim ret As String = src.Substring(0, src.Length - cutLen)

        Return ret

    End Function

#End Region

#End Region

End Class
