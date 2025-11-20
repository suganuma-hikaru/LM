' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI850H : 日医工在庫照合データCSV作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'Imports Microsoft.Office.Interop
Imports System.Text
Imports System.IO

''' <summary>
''' LMI850ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI850H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI850F = New LMI850F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'CSV出力データ検索処理
        Dim rtnDs As DataSet = Me.SelectCSV(frm, prmDs)

        'CSV出力データ編集処理
        rtnDs = Me.editCSV(rtnDs)

        'CSV出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeCSV(rtnDs)

        prm.ReturnFlg = rtnFlg

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectCSV(ByVal frm As LMI850F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCSV")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI850BLF", _
                                                         "SelectCSV", _
                                                         ds, _
                                                         -1, _
                                                         -1)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCSV")

        Return rtnDs

    End Function

    ''' <summary>
    ''' CSV出力データ編集処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    Private Function EditCSV(ByVal ds As DataSet) As DataSet

        If ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows.Count = 0 Then
            Return ds
        End If

        Dim goodsCdNrs As String = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(0).Item("GOODS_CD_NRS").ToString
        Dim lotNo As String = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(0).Item("LOT_NO").ToString
        Dim ltDate As String = String.Concat(Mid(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(0).Item("LT_DATE").ToString, 1, 6), "00")
        Dim sumNb As Decimal = 0
        Dim sumNbOther As Decimal = 0
        Dim max As Integer = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows.Count - 1

        For i As Integer = 0 To max

            If (goodsCdNrs).Equals(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("GOODS_CD_NRS").ToString) = False OrElse _
                (lotNo).Equals(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LOT_NO").ToString) = False OrElse _
                (ltDate).Equals(String.Concat(Mid(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LT_DATE").ToString, 1, 6), "00")) = False Then
                'まとめ条件が異なる場合

                '総バラ数良品の再設定
                ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i - 1).Item("SUM_NB") = sumNb

                '総バラ数良品以外の再設定
                ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i - 1).Item("SUM_NB_OTHER") = sumNbOther

                'まとめ先頭フラグの再設定
                ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i - 1).Item("MATOME_FLG") = "01"

                goodsCdNrs = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("GOODS_CD_NRS").ToString
                lotNo = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LOT_NO").ToString
                ltDate = String.Concat(Mid(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LT_DATE").ToString, 1, 6), "00")
                sumNb = 0
                sumNbOther = 0

            End If

            '有効期限の再設定
            If String.IsNullOrEmpty(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LT_DATE").ToString) = False Then
                ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LT_DATE") = ltDate
            Else
                ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("LT_DATE") = "00000000"
            End If

            '実レコード数の再設定
            ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("RECORD_CNT") = max + 1

            If String.IsNullOrEmpty(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("GOODS_COND_KB_1").ToString) = True AndAlso _
                String.IsNullOrEmpty(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("GOODS_COND_KB_2").ToString) = True AndAlso _
                ("01").Equals(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("INFERIOR_GOODS_KB").ToString) = False Then
                '良品の場合
                sumNb = sumNb + Convert.ToDecimal(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("PORA_ZAI_NB").ToString)
            Else
                '良品以外の場合
                sumNbOther = sumNbOther + Convert.ToDecimal(ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("PORA_ZAI_NB").ToString)
            End If

            'まとめ先頭フラグの再設定
            ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(i).Item("MATOME_FLG") = "02"

        Next

        '最終レコードに対して設定
        '総バラ数良品の再設定
        ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(max).Item("SUM_NB") = sumNb

        '総バラ数良品以外の再設定
        ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(max).Item("SUM_NB_OTHER") = sumNbOther

        'まとめ先頭フラグの再設定
        ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows(max).Item("MATOME_FLG") = "01"

        Return ds

    End Function

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    Private Function MakeCSV(ByVal ds As DataSet) As Boolean

        If ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows.Count = 0 Then
            Return False
        End If

        Dim strData As String = String.Empty
        Dim max As Integer = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Rows.Count - 1
        Dim dr() As DataRow = ds.Tables(LMI850C.TABLE_NM_OUT_CSV).Select(Nothing, "MATOME_FLG,GOODS_CD_NRS,LOT_NO,LT_DATE")
        Dim cutStr() As String
        Dim recCnt As Integer = 0
        Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd()

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()

        '■開始行
        'レコード区分
        strData = "S"
        setData.Append(strData)

        'データ識別
        strData = "41"
        setData.Append(strData)

        '送信元グループコード
        strData = "00000"
        setData.Append(strData)

        '送信元コード
        strData = "NRS01"
        setData.Append(strData)

        '送信先グループコード
        strData = "00000"
        setData.Append(strData)

        '送信先コード
        strData = "37601"
        setData.Append(strData)

        'サブファイルSEQNO
        strData = "0000"
        setData.Append(strData)

        'データ作成日付
        strData = Mid(MyBase.GetSystemDateTime(0), 1, 8)
        setData.Append(strData)

        'データ作成時刻
        strData = Mid(MyBase.GetSystemDateTime(1), 1, 6)
        setData.Append(strData)

        'データ月度
        strData = Mid(MyBase.GetSystemDateTime(0), 1, 6)
        setData.Append(strData)

        '最終データ区分
        strData = Space(1)
        setData.Append(strData)

        '物理レコードサイズ
        strData = "128"
        setData.Append(strData)

        '論理レコードサイズ
        strData = "128"
        setData.Append(strData)

        '送信済みレコード区分
        strData = Space(1)
        setData.Append(strData)

        '予備
        strData = Space(73)
        setData.Append(strData)
        '20120525 先方から改行コード不要言われたので削除
        'setData.Append(vbLf)

        For i As Integer = 0 To max

            If ("02").Equals(dr(i).Item("MATOME_FLG").ToString) = True Then
                Exit For
            End If

            '■データ行
            'レコード区分
            strData = "D"
            setData.Append(strData)

            'データ識別
            strData = "41"
            setData.Append(strData)

            'メーカーコード
            strData = "376"
            setData.Append(strData)

            '拠点コード
            If nrsBrCd = "10" Then
                strData = String.Concat("01", Space(3))
            ElseIf nrsBrCd = "15" Then
                strData = String.Concat("01", Space(3))
            ElseIf nrsBrCd = "20" Then
                strData = String.Concat("02", Space(3))
            End If

            setData.Append(strData)

            '統一商品コード
            ReDim cutStr(0)
            cutStr = Me.stringCut(dr(i).Item("GOODS_CD_CUST").ToString, 6, 1)
            strData = String.Concat("376", Me.AtoCoverData(cutStr(0), Space(1), 6))
            setData.Append(strData)

            '製造番号コード(LOT_NOは40byteなので、stringCutで先頭20byte取得して、その状態から後ろ20byteをスペース埋めしている)
            ReDim cutStr(0)
            cutStr = Me.stringCut(dr(i).Item("LOT_NO").ToString, 20, 1)
            strData = Me.AtoCoverData(cutStr(0), Space(1), 20)
            setData.Append(strData)

            '有効期限
            strData = dr(i).Item("LT_DATE").ToString
            setData.Append(strData)

            '総バラ数良品
            strData = Me.MaeCoverData(dr(i).Item("SUM_NB").ToString, "0", 12)
            setData.Append(strData)

            '総バラ数良品以外
            strData = Me.MaeCoverData(dr(i).Item("SUM_NB_OTHER").ToString, "0", 12)
            setData.Append(strData)

            '入数
            strData = Me.MaeCoverData(Mid(dr(i).Item("PKG_NB").ToString, 1, 4), "0", 4)
            setData.Append(strData)

            '予備
            strData = Space(52)
            setData.Append(strData)

            'setData.Append(vbLf)
            recCnt = recCnt + 1
        Next

        '■最終行
        'レコード区分
        strData = "E"
        setData.Append(strData)

        'データ識別
        strData = "41"
        setData.Append(strData)

        'レコード件数
        strData = Me.MaeCoverData(Convert.ToString(recCnt), "0", 6)
        setData.Append(strData)

        '実レコード件数
        strData = Me.MaeCoverData(Convert.ToString(recCnt), "0", 6)
        setData.Append(strData)

        '予備
        strData = Space(113)
        setData.Append(strData)

        'setData.Append(vbLf)

        '保存先のCSVファイルのパス
        'Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C010' AND ", _
        '                                                                                                "KBN_CD = '00'"))

        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C010' AND ", _
                                                                                                        "KBN_CD = '" & nrsBrCd & "'"))

        '正規ファイル保存先
        Dim CSVPath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, _
                                              kbnDr(0).Item("KBN_NM2").ToString)
        'バックファイル保存先
        Dim CSVBackUpPath As String = String.Concat(kbnDr(0).Item("KBN_NM3").ToString, _
                                                    kbnDr(0).Item("KBN_NM2").ToString, _
                                                    "_", _
                                                    Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                    Mid(MyBase.GetSystemDateTime(1), 1, 6))

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く(正規ファイル)
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM1").ToString)
        Dim sr As StreamWriter = New StreamWriter(CSVPath, False, enc)
        '値の設定
        sr.Write(setData.ToString())
        'ファイルを閉じる
        sr.Close()


        'ファイルを開く(バックアップファイル)
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM3").ToString)
        sr = New StreamWriter(CSVBackUpPath, False, enc)
        '値の設定
        sr.Write(setData.ToString())
        'ファイルを閉じる
        sr.Close()

        Return True

    End Function

#End Region

#Region "DataSet設定"

#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value) To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "後埋め設定"

    ''' <summary>
    ''' 後埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">後埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>後埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function AtoCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value) To keta - 1
            value = String.Concat(value, value2)
        Next

        Return value

    End Function

#End Region

#Region "文字分割"

    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function stringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

        Dim newCnt As Integer = inCnt - 1
        Dim newByte As Integer = inByte - 1
        Dim oldStr(newCnt) As String
        Dim newStr(newCnt) As String
        Dim byteCnt As Integer = 1

        For i As Integer = 0 To newCnt
            For j As Integer = 0 To newByte
                oldStr(i) = String.Concat(oldStr(i), Mid(inStr, byteCnt, 1))
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(oldStr(i)) <= newByte + 1 Then
                    newStr(i) = oldStr(i)
                    byteCnt = byteCnt + 1
                Else
                    Exit For
                End If
            Next
        Next

        Return newStr

    End Function

#End Region

#End Region 'Method

End Class
