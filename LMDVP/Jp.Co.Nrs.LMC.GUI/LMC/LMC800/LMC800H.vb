' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC800C : 名鉄CSV出力
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO

''' <summary>
''' LMC800ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC800H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region 'Field

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'CSV出力処理
        Me.MakeCSV(Me._PrmDs)

        '出荷(大)の更新
        Me.UpdateMeitetuCsv(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim setDs As DataSet = prmDs.Copy()
        Dim setOutDt As DataTable = setDs.Tables("LMC800OUT")
        setOutDt.Clear()

        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMC800OUT").Select(Nothing, "CUST_CD_L,JIS,NIUKENIN_CD,SYUKKABI,HAISOBI,DENPYO_NO")

        'まとめ処理、編集処理
        Dim kiji7 As String = String.Empty
        '(2013.03.13)要望番号1930 -- START --
        Dim kiji8 As String = String.Empty
        '(2013.03.13)要望番号1930 --  END  --
        Dim kosu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim cutStr() As String
        Dim matomeCnt As Integer = 0
        Dim max As Integer = outDr.Length - 1

        For i As Integer = 0 To max
            '別インスタンスのデータロウを空にする
            kiji7 = outDr(i).Item("KIJI_7").ToString
            '(2013.03.13)要望番号1930 -- START --
            kiji8 = outDr(i).Item("KIJI_8").ToString
            '(2013.03.13)要望番号1930 --  END  --
            kosu = Convert.ToDecimal(outDr(i).Item("KOSU").ToString)
            juryo = Convert.ToDecimal(outDr(i).Item("JYURYO").ToString)

            '荷送人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_MEI1").ToString, 34, 2)
            outDr(i).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            outDr(i).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD1").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD2").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD3").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD3") = cutStr(0).Trim

            '荷受人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_NM1").ToString, 34, 2)
            outDr(i).Item("NIUKENIN_NM1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_NM2") = cutStr(1).Trim

            '荷受人住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_ADD1").ToString, 34, 3)
            outDr(i).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            outDr(i).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_1").ToString, 40, 1)
            outDr(i).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_2").ToString, 40, 1)
            outDr(i).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_3").ToString, 40, 1)
            outDr(i).Item("KIJI_3") = cutStr(0).Trim

            '記事4の再設定
            If String.IsNullOrEmpty(outDr(i).Item("SHIP_NM_L").ToString) = False Then
                outDr(i).Item("KIJI_4") = String.Concat(outDr(i).Item("SHIP_NM_L").ToString, "様扱い")
            ElseIf String.IsNullOrEmpty(outDr(i).Item("DENPYO_NM").ToString) = False Then
                outDr(i).Item("KIJI_4") = String.Concat(outDr(i).Item("DENPYO_NM").ToString, "様扱い")
            Else
                outDr(i).Item("KIJI_4") = String.Concat(outDr(i).Item("CUST_NM_L").ToString, "様扱い")
            End If
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_4").ToString, 40, 1)
            outDr(i).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_5").ToString, 40, 2)
            outDr(i).Item("KIJI_5") = cutStr(0).Trim
            outDr(i).Item("KIJI_6") = cutStr(1).Trim

            If i = 0 Then
                '1件目
                matomeCnt = 0
                If String.IsNullOrEmpty(kiji7) = False Then
                    matomeCnt = 1
                End If
                setOutDt.ImportRow(outDr(i))
            ElseIf String.IsNullOrEmpty(outDr(i).Item("UNCHIN_SEIQTO_CD").ToString) = False AndAlso _
                   String.IsNullOrEmpty(outDr(i).Item("CUST_CD_L").ToString) = False AndAlso _
                   String.IsNullOrEmpty(outDr(i).Item("SYUKKABI").ToString) = False AndAlso _
                   String.IsNullOrEmpty(outDr(i).Item("HAISOBI").ToString) = False AndAlso _
                   (outDr(i).Item("UNCHIN_SEIQTO_CD").ToString).Equals(outDr(i - 1).Item("UNCHIN_SEIQTO_CD").ToString) = True AndAlso _
                   (outDr(i).Item("CUST_CD_L").ToString).Equals(outDr(i - 1).Item("CUST_CD_L").ToString) = True AndAlso _
                   (outDr(i).Item("SYUKKABI").ToString).Equals(outDr(i - 1).Item("SYUKKABI").ToString) = True AndAlso _
                   (outDr(i).Item("HAISOBI").ToString).Equals(outDr(i - 1).Item("HAISOBI").ToString) = True Then
                'まとめ対象データの場合
                '個数まとめ
                setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KOSU") = Convert.ToString( _
                                                                                       Convert.ToDecimal(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KOSU").ToString()) + _
                                                                                       kosu _
                                                                                      )
                '重量まとめ
                setOutDt.Rows(setOutDt.Rows.Count - 1).Item("JYURYO") = Convert.ToString( _
                                                                                         Convert.ToDecimal(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("JYURYO").ToString()) + _
                                                                                         juryo _
                                                                                        )

                If String.IsNullOrEmpty(kiji7) = False Then
                    matomeCnt = matomeCnt + 1
                Else
                    Continue For
                End If
                If matomeCnt <= 4 Then
                    'まとめの4件目までの場合
                    setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_7") = String.Concat(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_7").ToString, _
                                                                                          ",", _
                                                                                          kiji7)
                    '(2013.03.13)要望番号1930 -- START --
                    setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_8") = String.Concat(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_8").ToString, _
                                                                                          ",", _
                                                                                          kiji8)
                    '(2013.03.13)要望番号1930 --  END  --

                ElseIf matomeCnt = 5 Then
                    'まとめの5件目の場合
                    setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_8") = kiji7
                ElseIf 6 <= matomeCnt AndAlso matomeCnt <= 8 Then
                    'まとめの6～8件目までの場合
                    setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_8") = String.Concat(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KIJI_8").ToString, _
                                                                                          ",", _
                                                                                          kiji7)
                End If
            Else
                matomeCnt = 0
                If String.IsNullOrEmpty(kiji7) = False Then
                    matomeCnt = 1
                End If
                setOutDt.ImportRow(outDr(i))
            End If
        Next

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()

#If False Then ' フィルメニッヒ セミEDI対応  20161003 changed inoue 
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
#Else
        ' 出荷検索画面のデフォルトのソート順と同じ順序で出力する
        For Each row As DataRow In setOutDt.AsEnumerable().OrderBy(Function(s) s.Item("SYUKKABI")) _
                                                          .ThenBy(Function(s) s.Item("DENPYO_NO"))
            With row
#End If
                setData.Append(SetDblQuotation(.Item("DENPYO_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUKKABI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SHIHARAININ_CD").ToString()))
                setData.Append(",")
                '(2012.12.10)要望番号1644 荷受人はSPACE15byte設定 --- START ---
                'setData.Append(SetDblQuotation(.Item("NIUKENIN_CD").ToString()))
                setData.Append("""               """)
                '(2012.12.10)要望番号1644 荷受人はSPACE15byte設定 ---  END  ---
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAISOBI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_6").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_7").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_8").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KOSU").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("PARETTOSU").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("JYURYO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("YOSEKI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HOKENRYOU").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC800OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC800OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC800OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC800OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC800OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

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

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC800BLF", "UpdateMeitetuCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class