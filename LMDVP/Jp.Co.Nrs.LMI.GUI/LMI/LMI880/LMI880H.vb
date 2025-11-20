' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI880  : 篠崎運送月末在庫実績データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text
Imports System.IO

''' <summary>
''' LMI880ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI880H
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
        Dim frm As LMI880F = New LMI880F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'CSV出力データ検索処理
        Dim rtnDs As DataSet = Me.SelectCSV(frm, prmDs)

        'エラー時は終了
        If MyBase.IsMessageExist() = True Then
            prm.ReturnFlg = False
            Exit Sub
        End If

        'CSV出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeCSV(frm, rtnDs, prmDs)

        'エラー時は終了
        If MyBase.IsMessageExist() = True Then
            prm.ReturnFlg = False
            Exit Sub
        End If

        'CSV出力データ更新処理
        rtnDs = Me.UpdateCSV(frm, prmDs)

        'エラー時は終了
        If MyBase.IsMessageExist() = True Then
            prm.ReturnFlg = False
            Exit Sub
        End If

        prm.ReturnFlg = True

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
    Private Function SelectCSV(ByVal frm As LMI880F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCSV")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI880BLF", _
                                                         "SelectCSV", _
                                                         ds, _
                                                         -1, _
                                                         -1)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCSV")

        Return rtnDs

    End Function

    ''' <summary>
    ''' CSV出力データ更新処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateCSV(ByVal frm As LMI880F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateCSV")

        '==========================
        'WSAクラス呼出
        '==========================

        '更新処理
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI880BLF", "UpdateCSV", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateCSV")

        Return rtnDs

    End Function

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    Private Function MakeCSV(ByVal frm As LMI880F, ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean

        If ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows.Count = 0 Then
            Return False
        End If

        Dim cutStr() As String
        Dim strData As String = String.Empty
        Dim max As Integer = ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows.Count - 1

        Dim numValue As Integer = 0

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()

        '■①開始行
        '固定文字1
        strData = "1"
        setData.Append(strData)

        '固定文字2
        strData = "3"
        setData.Append(strData)

        'システム日付
        strData = String.Concat(MyBase.GetSystemDateTime(0), Mid(MyBase.GetSystemDateTime(1), 1, 6))
        setData.Append(strData)

        '予備
        strData = Space(240)
        setData.Append(strData)

        setData.Append(vbNewLine)

        For i As Integer = 0 To max
            '■②データ行
            '固定文字1
            strData = "2"
            setData.Append(strData)

            '商品コード
            ReDim cutStr(0)
            cutStr = Me.stringCut(AtoCoverData(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("GOODS_CD_CUST").ToString(), " ", 10), 10, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            '商品名
            ReDim cutStr(0)
            strData = ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("GOODS_NM").ToString().Replace("　", " ")
            cutStr = Me.stringCut(AtoCoverData(strData, " ", 28), 28, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            'ロット№
            ReDim cutStr(0)
            cutStr = Me.stringCut(AtoCoverData(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("LOT_NO").ToString(), " ", 10), 10, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            '数量
            ReDim cutStr(0)
            numValue = Convert.ToInt32(Convert.ToDecimal(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("QT").ToString()) * 1000)
            cutStr = Me.stringCut(MaeCoverData(Convert.ToString(numValue), "0", 8), 8, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            '入目
            ReDim cutStr(0)
            numValue = Convert.ToInt32(Convert.ToDecimal(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("IRIME").ToString()) * 1000)
            cutStr = Me.stringCut(MaeCoverData(Convert.ToString(numValue), "0", 7), 7, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            'ロケーション
            ReDim cutStr(0)
            cutStr = Me.stringCut(AtoCoverData(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("LOCA").ToString(), " ", 6), 6, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            'オーダー番号
            ReDim cutStr(0)
            cutStr = Me.stringCut(AtoCoverData(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("ORDER_NO").ToString(), " ", 10), 10, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            '棚卸日付
            strData = ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(i).Item("TANAOROSHI_BI").ToString()
            setData.Append(strData)

            '予備
            strData = Space(170)
            setData.Append(strData)

            setData.Append(vbNewLine)

        Next

        '■③データ件数行
        '固定文字1
        strData = "8"
        setData.Append(strData)

        'データ行件数
        ReDim cutStr(0)
        cutStr = Me.stringCut(MaeCoverData(Convert.ToString(ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows.Count), "0", 6), 6, 1)
        strData = cutStr(0).ToString
        setData.Append(strData)

        '予備
        strData = Space(249)
        setData.Append(strData)

        setData.Append(vbNewLine)


        '■③最終行
        '固定文字1
        strData = "9"
        setData.Append(strData)

        '予備
        strData = Space(255)
        setData.Append(strData)

        setData.Append(vbNewLine)


        '保存先のCSVファイルのパス
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F003' AND ", _
                                                                                                        "KBN_NM1 = '", ds.Tables(LMI880C.TABLE_NM_INOUT_CSV).Rows(0).Item("NRS_BR_CD").ToString, "'"))
        If kbnDr.Length = 0 Then
            Return False
        End If
        Dim CSVPath As String = String.Concat(kbnDr(0).Item("KBN_NM2").ToString, _
                                              kbnDr(0).Item("KBN_NM3").ToString, _
                                              MyBase.GetSystemDateTime(0))

        Dim CSVBackUpPath As String = String.Concat(kbnDr(0).Item("KBN_NM4").ToString, _
                                                    kbnDr(0).Item("KBN_NM3").ToString, _
                                                    MyBase.GetSystemDateTime(0), _
                                                    Mid(MyBase.GetSystemDateTime(1), 1, 6))

        'CSVファイルに書き込むときに使うEncoding
        'Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim enc As System.Text.Encoding = System.Text.UnicodeEncoding.Unicode

        'ファイルを開く
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
        Dim sr As StreamWriter = New StreamWriter(CSVPath, False, enc)
        '値の設定
        sr.Write(setData.ToString())
        'ファイルを閉じる
        sr.Close()

        'ファイルを開く(バックアップファイル)
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM4").ToString)
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

        For i As Integer = value.Length To keta - 1
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

        For i As Integer = value.Length To keta - 1
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
