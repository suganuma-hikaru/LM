' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC960    : トールCSV出力
'  作  成  者       :  []
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC960BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC960BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    Private Const USER_ID As String = "73080080"
    Private Const SENDER_CD As String = "80080"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC960DAC = New LMC960DAC()
    Private _Dac020 As LMC020DAC = New LMC020DAC()

#End Region

#Region "Method"

#Region "CSVデータ取得処理"


    ''' <summary>
    ''' CSVデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function TollCsv(ByVal ds As DataSet) As DataSet

        ' 印刷対象データの取得
        Dim rtnDs As DataSet = Me.SelectTollCsv(ds)

        ' 出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC960OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        ' 取得値の加工を行う（SQLでできないか煩雑なもの）
        For i As Integer = 0 To rtnDs.Tables("LMC960OUT").Rows.Count - 1
            Dim dr As DataRow = rtnDs.Tables("LMC960OUT").Rows(i)
            ' 重量の整数化(小数部切上げ)
            Dim juryo As Decimal
            If Decimal.TryParse(dr.Item("JURYO").ToString(), juryo) Then
                dr.Item("JURYO") = Math.Ceiling(juryo).ToString()
            End If

            ' お届け先名の分割
            If dr.Item("DEST_NM_1").ToString().Length > 12 Then
                Dim destNm As String = dr.Item("DEST_NM_1").ToString()
                dr.Item("DEST_NM_1") = destNm.Substring(0, 12)
                If destNm.Length > (12 + 12) Then
                    dr.Item("DEST_NM_2") = destNm.Substring(12, 12)
                    If destNm.Length > (12 + 12 + 12) Then
                        dr.Item("DEST_NM_3") = destNm.Substring(12 + 12, 12)
                    Else
                        dr.Item("DEST_NM_3") = destNm.Substring(12 + 12)
                    End If
                Else
                    dr.Item("DEST_NM_2") = destNm.Substring(12)
                End If
            End If

            ' お届け先電話番号にハイフンが含まれない場合のクリア
            ' (項目として設定必須ではないが、値設定時は「ハイフン必要」項目のため)
            If dr.Item("DEST_TEL").ToString().IndexOf("-") < 0 Then
                dr.Item("DEST_TEL") = String.Empty
            End If

            ' お届け先郵便番号のハイフン編集
            If dr.Item("DEST_ZIP").ToString().Length > 3 AndAlso dr.Item("DEST_ZIP").ToString().IndexOf("-") < 0 Then
                dr.Item("DEST_ZIP") =
                    String.Concat(
                        dr.Item("DEST_ZIP").ToString().Substring(0, 3),
                        "-",
                        dr.Item("DEST_ZIP").ToString().Substring(3))
            End If

            ' お届け先住所１～２の分割
            If dr.Item("DEST_AD_1").ToString().Length > 28 Then
                Dim destAd As String =
                    String.Concat(dr.Item("DEST_AD_1").ToString(), dr.Item("DEST_AD_2").ToString())
                dr.Item("DEST_AD_1") = destAd.Substring(0, 28)
                If destAd.Length > (28 + 28) Then
                    dr.Item("DEST_AD_2") = destAd.Substring(28, 28)
                Else
                    dr.Item("DEST_AD_2") = destAd.Substring(28)
                End If
            End If

            ' 荷送人名１～３の分割
            If dr.Item("SENDER_NM_1").ToString().Length > 12 Then
                Dim senderNm As String = dr.Item("SENDER_NM_1").ToString()
                dr.Item("SENDER_NM_1") = senderNm.Substring(0, 12)
                If senderNm.Length > (12 + 12) Then
                    dr.Item("SENDER_NM_2") = senderNm.Substring(12, 12)
                    If senderNm.Length > (12 + 12 + 12) Then
                        dr.Item("SENDER_NM_3") = senderNm.Substring(12 + 12, 12)
                    Else
                        dr.Item("SENDER_NM_3") = senderNm.Substring(12 + 12)
                    End If
                Else
                    dr.Item("SENDER_NM_2") = senderNm.Substring(12)
                End If
            End If

            ' 荷送人住所１～２の分割
            If dr.Item("SENDER_AD_1").ToString().Length > 28 Then
                Dim senderAd As String = dr.Item("SENDER_AD_1").ToString()
                dr.Item("SENDER_AD_1") = senderAd.Substring(0, 28)
                If senderAd.Length > (28 + 28) Then
                    dr.Item("SENDER_AD_2") = senderAd.Substring(28, 28)
                Else
                    dr.Item("SENDER_AD_2") = senderAd.Substring(28)
                End If
            End If

            ' 商品名の桁調整
            For hinNmIdx As Integer = 1 To 8
                Dim goodsNmField As String = String.Concat("HIN_NM_", hinNmIdx.ToString())
                If dr.Item(goodsNmField).ToString().Length > 20 Then
                    dr.Item(goodsNmField) = dr.Item(goodsNmField).ToString().Substring(0, 20)
                End If
            Next
        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectSLineCsvメソッド呼出</remarks>
    Private Function SelectTollCsv(ByVal ds As DataSet) As DataSet

        ' 元のデータ
        Dim dt As DataTable = ds.Tables("LMC960IN")
        Dim outDt As DataTable = ds.Tables("LMC960OUT")
        Dim max As Integer = dt.Rows.Count - 1

        ' 別インスタンス
        Dim setDs As DataSet = ds.Copy()

        Dim dr As DataRow

        Dim cntOutkaL As Integer
        Dim cntOutkaM As Integer
        Dim cntUnsoL As Integer

        Dim ret As Boolean = False

        Dim renban As Integer = 0

        For i As Integer = 0 To max
            ' 以下、出荷(大) および運送(大) のデータ取得は「出荷データ編集」の LMC020DDAC にて行う。
            ' （LMC020DDAC の運送(大) の取得のコピー実装を回避するため。
            ' 　加えて、運送(大) の取得条件値の判定項目として、LMC020DDAC 内で、直前に取得した
            ' 　出荷(大) の項目値を参照しており、判定処理が変わった場合の追従の煩雑さ回避も考慮し、
            ' 　出荷(大) も LMC020DDAC にて取得することとする）

            Dim ds020 As DataSet = New DSL.LMC020DS

            dr = ds020.Tables("LMC020IN").NewRow()
            dr.Item("NRS_BR_CD") = dt.Rows(i).Item("NRS_BR_CD").ToString()
            dr.Item("OUTKA_NO_L") = dt.Rows(i).Item("OUTKA_NO_L").ToString()
            ds020.Tables("LMC020IN").Rows.Add(dr)

            ' 出荷(大)のデータ取得
            ds020 = MyBase.CallDAC(Me._Dac020, "SelectOutkaLData", ds020)
            If MyBase.IsMessageExist() Then
                Return ds
            End If
            cntOutkaL = MyBase.GetResultCount()
            ' 0件の場合は次のデータへ
            If cntOutkaL = 0 Then
                Continue For
            End If

            ' 運送(大)のデータ取得
            ds020 = MyBase.CallDAC(Me._Dac020, "SelectUnsoLData", ds020)
            If MyBase.IsMessageExist() Then
                Return ds
            End If
            cntUnsoL = MyBase.GetResultCount()

            Call setDs.Tables("LMC960IN").Clear()
            dr = setDs.Tables("LMC960IN").NewRow()
            dr.Item("NRS_BR_CD") = dt.Rows(i).Item("NRS_BR_CD").ToString()
            dr.Item("OUTKA_NO_L") = dt.Rows(i).Item("OUTKA_NO_L").ToString()
            setDs.Tables("LMC960IN").Rows.Add(dr)

            If setDs.Tables("LMC960OUT_KBN").Rows.Count = 0 Then
                ' 区分マスタの抽出
                ret = Me.ServerChkJudge(setDs, "SelectTollCsvZKbnN010")
                If Not ret Then
                    Return ds
                End If
            End If
            Dim nrsBrDr() As DataRow =
                setDs.Tables("LMC960OUT_NRS_BR").Select(String.Concat("NRS_BR_CD = '",
                    setDs.Tables("LMC960IN").Rows(0).Item("NRS_BR_CD").ToString(), "'"))
            If nrsBrDr.Length = 0 Then
                ' 営業所マスタの抽出
                ret = Me.ServerChkJudge(setDs, "SelectTollCsvNrsBr")
                If Not ret Then
                    Return ds
                End If
            End If

            ' 出荷(中)のデータ取得
            ret = Me.ServerChkJudge(setDs, "SelectTollCsvOutkaM", cntOutkaM)
            If Not ret Then
                Return ds
            End If

            ' 出荷L.納入予定時刻よりの納入予定区分の導出
            Dim kbNmN010 As String = ""
            Dim kbnDr() As DataRow =
                setDs.Tables("LMC960OUT_KBN").Select(String.Concat("KBN_GROUP_CD = 'N010' ",
                        "AND KBN_CD = '", ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("ARR_PLAN_TIME").ToString(), "'"))
            If kbnDr.Length > 0 Then
                kbNmN010 = String.Concat("納入指定：", kbnDr(0).Item("KBN_NM1").ToString())
            End If

            Dim hinNmIdx As Integer = 0
            For j As Integer = 0 To cntOutkaM - 1
                ' 出荷M の商品名を、出力データテーブル(LMC960OUT) の HIN_NM_1～HIN_NM_8 に設定し、
                ' 出荷M 8件ごと、および取得した出荷M の最後のレコードのタイミングで、
                ' 出力データテーブル(LMC960OUT) の行に追加する。
                Dim addRow As Boolean = CBool((j + 1) Mod 8 = 0 OrElse j = cntOutkaM - 1)
                Dim editRow As Boolean = CBool((j + 1) Mod 8 = 1)
                If editRow Then
                    ' 値設定
                    renban += 1
                    dr = outDt.NewRow()
                    dr.Item("RENBAN") = renban.ToString()
                    dr.Item("OUT_YMD") = dt.Rows(i).Item("SYS_DATE").ToString()
                    dr.Item("USER_ID") = LMC960BLC.USER_ID
                    dr.Item("YOBI1_1") = kbNmN010
                    dr.Item("SYUKKA_DATE") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_PLAN_DATE").ToString()
                    dr.Item("DELIV_PREF_DATE") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("ARR_PLAN_DATE").ToString()
                    dr.Item("SYUKKA_NO") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString()
                    If cntUnsoL > 0 Then
                        dr.Item("JURYO") = ds020.Tables("LMC020_UNSO_L").Rows(0).Item("UNSO_WT").ToString()
                    End If
                    dr.Item("TANI") = "0"
                    dr.Item("KOSU") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_PKG_NB").ToString()
                    If ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_KB").ToString().Equals("00") Then
                        dr.Item("DEST_NM_1") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_NM").ToString()
                    Else
                        dr.Item("DEST_NM_1") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_NM2").ToString()
                    End If
                    dr.Item("DEST_TEL") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_TEL").ToString()
                    If ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_KB").ToString().Equals("00") Then
                        dr.Item("DEST_ZIP") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("ZIP").ToString()
                        dr.Item("DEST_AD_1") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("AD_1").ToString()
                        dr.Item("DEST_AD_2") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("AD_2").ToString()
                    Else
                        dr.Item("DEST_AD_1") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_AD_1").ToString()
                        dr.Item("DEST_AD_2") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_AD_2").ToString()
                    End If
                    dr.Item("YOBI_3_1") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("DEST_AD_3").ToString()
                    dr.Item("SENDER_CD") = LMC960BLC.SENDER_CD
                    If setDs.Tables("LMC960OUT_NRS_BR").Rows.Count > 0 Then
                        dr.Item("SENDER_NM_1") = setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("NRS_BR_NM").ToString()
                        dr.Item("SENDER_TEL") = setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("TEL").ToString()
                        dr.Item("SENDER_ZIP") = setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("ZIP").ToString()
                        dr.Item("SENDER_AD_1") = String.Concat(
                            setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("AD_1").ToString(),
                            setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("AD_2").ToString(),
                            setDs.Tables("LMC960OUT_NRS_BR").Rows(0).Item("AD_3").ToString())
                    End If
                End If
                If j <= (cntOutkaM - 1) Then
                    hinNmIdx = If(hinNmIdx >= 8, 1, hinNmIdx + 1)
                    dr.Item(String.Concat("HIN_NM_", hinNmIdx.ToString())) =
                        setDs.Tables("LMC960OUT_OUTKA_M").Rows(j).Item("GOODS_NM").ToString()
                End If
                If editRow Then
                    dr.Item("NRS_BR_CD") = dt.Rows(i).Item("NRS_BR_CD").ToString()
                    dr.Item("OUTKA_NO_L") = dt.Rows(i).Item("OUTKA_NO_L").ToString()
                    dr.Item("ROW_NO") = dt.Rows(i).Item("ROW_NO").ToString()
                    dr.Item("SYS_UPD_DATE") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("SYS_UPD_DATE").ToString()
                    dr.Item("SYS_UPD_TIME") = ds020.Tables("LMC020_OUTKA_L").Rows(0).Item("SYS_UPD_TIME").ToString()
                    dr.Item("FILEPATH") = dt.Rows(i).Item("FILEPATH").ToString()
                    dr.Item("FILENAME") = dt.Rows(i).Item("FILENAME").ToString()
                    dr.Item("SYS_DATE") = dt.Rows(i).Item("SYS_DATE").ToString()
                    dr.Item("SYS_TIME") = dt.Rows(i).Item("SYS_TIME").ToString()
                End If
                If addRow Then
                    outDt.Rows.Add(dr)
                End If
            Next
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新（トールCSV作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateTollCsv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateTollCsv", ds)

    End Function

#End Region

#Region "チェック"
    ''' <summary> 
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="count">件数を返す</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String, Optional ByRef count As Integer = 0) As Boolean

        ' DACアクセス
        ds = Me.DacAccess(ds, actionId)

        If Not IsNothing(count) Then
            If Not MyBase.IsMessageExist() Then
                count = MyBase.GetResultCount
            End If
        End If

        ' エラーがあるかを判定
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
