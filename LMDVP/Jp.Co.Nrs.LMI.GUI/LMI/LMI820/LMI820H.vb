' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI820H : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO

''' <summary>
''' LMI820ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI820H
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
        Dim frm As LMI820F = New LMI820F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'TXT出力データ検索処理
        Dim rtnDs As DataSet = Me.SelectTXT(frm, prmDs)

        'TXT出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeTXT(frm, rtnDs, prmDs)

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
    ''' TXT出力データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectTXT(ByVal frm As LMI820F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectTXT")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI820BLF", _
                                                         "SelectTXT", _
                                                         ds, _
                                                         -1, _
                                                         -1)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectTXT")

        Return rtnDs

    End Function

    ''' <summary>
    ''' TXT作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeTXTメソッド呼出</remarks>
    Private Function MakeTXT(ByVal frm As LMI820F, ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean

        If ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows.Count = 0 Then
            Return False
        End If

        Dim cutStr() As String
        Dim strData As String = String.Empty
        Dim max As Integer = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows.Count - 1

        'TXT出力処理
        Dim setData As StringBuilder = New StringBuilder()

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("ID").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("ID").ToString
            Else
                strData = "5"
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHORI_YM").ToString) = False Then
                strData = String.Concat("00", ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHORI_YM").ToString)
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("IN_KAISHA").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("IN_KAISHA").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("KAISHA_CD").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("KAISHA_CD").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            ReDim cutStr(0)
            cutStr = Me.stringCut(AtoCoverData(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("DV_NO").ToString(), " ", 9), 9, 1)
            strData = cutStr(0).ToString
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("GMID").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("GMID").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("COST").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("COST").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("HIYO").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("HIYO").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("TUKA").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("TUKA").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("GAKU").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("GAKU").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("FUGO").ToString) = False Then
                strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("FUGO").ToString
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("HASSEI_YM").ToString) = False Then
                strData = String.Concat("00", ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("HASSEI_YM").ToString)
            Else
                strData = String.Empty
            End If
            setData.Append(strData)

            Dim ByteLength As Integer
            If String.IsNullOrEmpty(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString) = False Then
                ByteLength = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString)
                ''全角文字が入っていた場合は0埋めしない
                'If Len(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString) <> ByteLength Then
                '    strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString
                'Else
                '    strData = MaeCoverData(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString, "0", 8)
                'End If
                '20150915 ma-takahashi 文字が入っていればOKに変更
                If ByteLength <> 0 Then
                    strData = ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString
                Else
                    strData = MaeCoverData(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIP_NO").ToString, "0", 8)
                End If
            Else
                strData = Space(8)
            End If
            setData.Append(strData)

            Dim splitData() As String
            splitData = (ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("WT").ToString).Split("."c)
            strData = MaeCoverData(splitData(0), "0", 8)
            setData.Append(strData)

            strData = MaeCoverData(ds.Tables(LMI820C.TABLE_NM_OUT_TXT).Rows(i).Item("KYORI").ToString, "0", 8)
            setData.Append(strData)
            setData.Append(vbNewLine)

        Next

        '保存先のTXTファイルのパス
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T019' AND ", _
                                                                                                        "KBN_CD = '01'"))

        Dim TXTPath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, _
                                              kbnDr(0).Item("KBN_NM2").ToString, _
                                              Mid(inDs.Tables(LMI820C.TABLE_NM_IN).Rows(0).Item("DATE_FROM").ToString, 1, 6), _
                                              kbnDr(0).Item("KBN_NM3").ToString, _
                                              ".txt")

        'TXTファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM1").ToString)
        Dim sr As StreamWriter = New StreamWriter(TXTPath, False, enc)

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
