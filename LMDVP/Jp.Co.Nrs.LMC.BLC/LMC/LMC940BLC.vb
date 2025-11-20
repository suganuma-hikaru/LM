' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC940    : ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV(大黒)出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC940BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC940BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC940DAC = New LMC940DAC()

#End Region

#Region "Method"

#Region "CSVデータ取得処理"

    ''' <summary>
    ''' CSVデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function KangarooMagicCsv(ByVal ds As DataSet) As DataSet

        '印刷対象データの取得
        Dim rtnDs As DataSet = Me.SelectKangarooMagicCsv(ds)

        ' 集計データ枠の用意
        Dim sumDt As DataTable = rtnDs.Tables("LMC940OUT").Clone()

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC940OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        ' CSV をまとめる場合のレコード優先順でのソート
        Dim baseDr As DataRow() = rtnDs.Tables("LMC940OUT").Select(Nothing, "OUTKA_NO_L ASC")

        '取得値の加工を行う（SQLでできないもの）
        For i As Integer = 0 To baseDr.Count - 1
            Dim dr As DataRow = baseDr(i)
            Dim len As Integer = 0
            Dim orgStr As String = String.Empty
            Dim cutStr As String = String.Empty

            ' 集計データ検索
            Dim sumDr As DataRow() = sumDt.Select(String.Concat("SUMMARY_KEY = '", dr.Item("SUMMARY_KEY").ToString(), "'"))
            If sumDr.Length > 0 Then
                ' 存在する場合は集計のみ行う。
                sumDr(0).Item("KOSU") = (Convert.ToInt64(sumDr(0).Item("KOSU")) + Convert.ToInt64(dr.Item("KOSU"))).ToString()
                sumDr(0).Item("JURYO") = (Convert.ToInt64(sumDr(0).Item("JURYO")) + Convert.ToInt64(dr.Item("JURYO"))).ToString()
                Continue For
            End If

            '着荷主名を分割
            With ""
                orgStr = dr.Item("DEST_NM1").ToString()
                '⇒着荷主名１(最大40byte)
                len = System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr)
                If len > 0 Then
                    If len > 40 Then
                        cutStr = LeftB(orgStr, 40)
                    Else
                        cutStr = orgStr
                    End If
                    dr.Item("DEST_NM1") = cutStr
                    orgStr = Mid(orgStr, cutStr.Length + 1)
                End If
                '⇒着荷主名２(最大40byte)
                len = System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr)
                If len > 0 Then
                    If len > 40 Then
                        cutStr = LeftB(orgStr, 40)
                    Else
                        cutStr = orgStr
                    End If
                    dr.Item("DEST_NM2") = cutStr
                End If
            End With

            '着荷主住所を分割
            With ""
                orgStr = dr.Item("DEST_AD1").ToString()
                '⇒着荷主住所１(最大40byte)
                len = System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr)
                If len > 0 Then
                    If len > 40 Then
                        cutStr = LeftB(orgStr, 40)
                    Else
                        cutStr = orgStr
                    End If
                    dr.Item("DEST_AD1") = cutStr
                    orgStr = Mid(orgStr, cutStr.Length + 1)
                End If
                '⇒着荷主住所２(最大40byte)
                len = System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr)
                If len > 0 Then
                    If len > 40 Then
                        cutStr = LeftB(orgStr, 40)
                    Else
                        cutStr = orgStr
                    End If
                    dr.Item("DEST_AD2") = cutStr
                    orgStr = Mid(orgStr, cutStr.Length + 1)
                End If
                '⇒着荷主住所３(最大40byte)
                len = System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr)
                If len > 0 Then
                    If len > 40 Then
                        cutStr = LeftB(orgStr, 40)
                    Else
                        cutStr = orgStr
                    End If
                    dr.Item("DEST_AD3") = cutStr
                End If
            End With

            '記事を最大桁数で切る
            With ""
                '記事１(最大30byte)
                orgStr = dr.Item("KIJI1").ToString()
                If System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr) > 30 Then
                    dr.Item("KIJI1") = LeftB(orgStr, 30)
                End If
                '記事２(最大30byte)
                orgStr = dr.Item("KIJI2").ToString()
                If System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr) > 30 Then
                    dr.Item("KIJI2") = LeftB(orgStr, 30)
                End If
                '記事３(最大30byte)
                orgStr = dr.Item("KIJI3").ToString()
                If System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr) > 30 Then
                    dr.Item("KIJI3") = LeftB(orgStr, 30)
                End If
                '記事４(最大30byte)
                orgStr = dr.Item("KIJI4").ToString()
                If System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr) > 30 Then
                    dr.Item("KIJI4") = LeftB(orgStr, 30)
                End If
                '記事５(最大30byte)
                orgStr = dr.Item("KIJI5").ToString()
                If System.Text.Encoding.GetEncoding(932).GetByteCount(orgStr) > 30 Then
                    dr.Item("KIJI5") = LeftB(orgStr, 30)
                End If
            End With

            ' 集計データへの追加
            sumDt.ImportRow(dr)
        Next

        ' 集計データを印刷対象データとして設定しなおす。
        rtnDs.Tables("LMC940OUT").Clear()
        For i As Integer = 0 To sumDt.Rows.Count - 1
            rtnDs.Tables("LMC940OUT").ImportRow(sumDt.Rows(i))
        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectSLineCsvメソッド呼出</remarks>
    Private Function SelectKangarooMagicCsv(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC940IN")
        Dim outDt As DataTable = ds.Tables("LMC940OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC940IN")
        Dim setDt As DataTable = setDs.Tables("LMC940OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectKangarooMagicCsv")

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
    ''' 出荷データ（大）更新（ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateKangarooCsv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateKangarooCsv", ds)

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

    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function

#End Region

#End Region

End Class
