' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI990H : サーテック　運賃明細作成
'  作  成  者       :  sia-minagawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Com.Utility
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop

''' <summary>
''' LMI990ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI990H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

    Private Class SumUpData
        Public SHUKKO_DATE As Integer
        Public NONYU_SAKI As String
        Public SHUKKO_SU As Integer
        Public YUSO_RYO As Integer
    End Class

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI990V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI990G

    ''' <summary>
    ''' パラメータ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

    ''' <summary>
    ''' EXCELファイルディレクトリ
    ''' </summary>
    ''' <remarks></remarks>
    Private _rcvDir As String

    ''' <summary>
    ''' CANCELフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _cancelFlg As Boolean

#End Region

#Region "Method"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Dim frm As LMI990F = New LMI990F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI990G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI990V(Me, frm, Me._LMIconV, Me._LMIconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(frm, LMI990C.MSG_INIT)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

#Region "処理"

    ''' <summary>
    ''' 処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function Process(ByVal frm As LMI990F) As Boolean

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI990C.EventShubetsu.SAKUSEI) = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)
            Return False

        End If

        'DataSet設定
        Dim ds As LMI990DS = New LMI990DS()
        Dim drIn As LMI990DS.LMI990INRow = ds.LMI990IN.NewLMI990INRow
        drIn.NRS_BR_CD = frm.cmbEigyo.SelectedValue.ToString
        drIn.SEIKYU_DATE = frm.imdSeikyuDate.TextValue
        ds.LMI990IN.AddLMI990INRow(drIn)

        '取込ファイル存在チェック
        If Not File.Exists(LMI990C.FILE_PATH) Then

            'メッセージ表示
            MyBase.ShowMessage(frm, "E657")
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)

            Return False
        End If

        '取込処理
        ds = TorikomiText(frm, ds)

        '集計処理
        ds = SumUp(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blfName As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blfName, LMI990C.ACTION_ID_PRINT_DATA, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            'メッセージ表示
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)

            Return False
        End If

        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)

        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        'メッセージ蓄積の判定
        If MyBase.IsMessageStoreExist = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)

            Return False
        End If

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G002", {"印刷", ""})

        '処理終了アクション
        Call Me._LMIconH.EndAction(frm, LMI990C.MSG_INIT)

        Return True

    End Function

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し(処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI990F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '処理
        Call Me.Process(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI990F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI990F, ByVal e As FormClosingEventArgs)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function TorikomiText(ByVal frm As LMI990F, ByVal ds As LMI990DS) As LMI990DS

        'ファイルオープン
        Using sr As New System.IO.StreamReader(LMI990C.FILE_PATH, System.Text.Encoding.GetEncoding("Shift_JIS"))

            Dim strItem As String
            Dim intItem As Integer
            Dim rowNo As Integer = 0

            While (sr.Peek() > -1)

                '1行読み込み
                Dim rec As String = sr.ReadLine()

                '行番号加算
                rowNo += 1

                'DataRowに値を設定
                Dim drTxt As LMI990DS.LMI990IN_TXTRow = ds.LMI990IN_TXT.NewLMI990IN_TXTRow()
                With drTxt

                    '入出庫日
                    strItem = SubstringByte(rec, 0, 6).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .NYUSHUKKO_DATE = intItem
                    Else
                        .NYUSHUKKO_DATE = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"入出庫日"}, rowNo.ToString())
                    End If
                    '指図No
                    .SASIZU_NO = SubstringByte(rec, 6, 20).Trim()
                    '納入先
                    .NONYU_SAKI = SubstringByte(rec, 26, 60).Trim()
                    '整理No
                    strItem = SubstringByte(rec, 86, 5).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .SEIRI_NO = intItem
                    Else
                        .SEIRI_NO = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"整理No"}, rowNo.ToString())
                    End If
                    '品名
                    .HINMEI = SubstringByte(rec, 91, 60).Trim()
                    'ロットNo
                    .LOT_NO = SubstringByte(rec, 151, 10).Trim()
                    '伝票種別
                    .DENPYO_SHUBETSU = SubstringByte(rec, 161, 14).Trim()
                    '扱便
                    .ATSUKAI_BIN = SubstringByte(rec, 175, 40).Trim()
                    '入庫数
                    strItem = SubstringByte(rec, 215, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .NYUKO_SU = intItem
                    Else
                        .NYUKO_SU = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"入庫数"}, rowNo.ToString())
                    End If
                    '出庫数
                    strItem = SubstringByte(rec, 224, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .SHUKKO_SU = intItem
                    Else
                        .SHUKKO_SU = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"出庫数"}, rowNo.ToString())
                    End If
                    '在庫数
                    strItem = SubstringByte(rec, 233, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .ZAIKO_SU = intItem
                    Else
                        .ZAIKO_SU = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"在庫数"}, rowNo.ToString())
                    End If
                    '保管料
                    strItem = SubstringByte(rec, 242, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .HOKAN_RYO = intItem
                    Else
                        .HOKAN_RYO = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"保管料"}, rowNo.ToString())
                    End If
                    '荷役料
                    strItem = SubstringByte(rec, 251, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .NIYAKU_RYO = intItem
                    Else
                        .NIYAKU_RYO = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"荷役料"}, rowNo.ToString())
                    End If
                    '輸送料
                    strItem = SubstringByte(rec, 260, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .YUSO_RYO = intItem
                    Else
                        .YUSO_RYO = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"輸送料"}, rowNo.ToString())
                    End If
                    '作業料
                    strItem = SubstringByte(rec, 269, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .SAGYO_RYO = intItem
                    Else
                        .SAGYO_RYO = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"作業料"}, rowNo.ToString())
                    End If
                    'その他
                    strItem = SubstringByte(rec, 278, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .SONOTA = intItem
                    Else
                        .SONOTA = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"その他"}, rowNo.ToString())
                    End If
                    '合計
                    strItem = SubstringByte(rec, 287, 9).Trim()
                    If Integer.TryParse(strItem, intItem) Then
                        .GOUKEI = intItem
                    Else
                        .GOUKEI = 0
                        MyBase.SetMessageStore(LMI990C.GUIDANCE_KBN_ERR, LMI990C.MSG_NOT_NUMERIC, {"合計"}, rowNo.ToString())
                    End If
                    '摘要
                    .TEKIYO = SubstringByte(rec, 296, 62).Trim()
                    '連番
                    .RENBAN = SubstringByte(rec, 358, 5).Trim()

                End With

                'DataRowをDataTableに追加
                ds.LMI990IN_TXT.AddLMI990IN_TXTRow(drTxt)

            End While

        End Using

        Return ds

    End Function

#End Region '取込処理


#Region "集計処理"

    ''' <summary>
    ''' 集計処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SumUp(ByVal frm As LMI990F, ByVal ds As LMI990DS) As LMI990DS

        '集計
        Dim querySumUp As IEnumerable(Of SumUpData) = From drTxt In ds.LMI990IN_TXT.AsEnumerable()
                    Order By drTxt.NYUSHUKKO_DATE Ascending, drTxt.NONYU_SAKI Ascending
                    Group By drTxt.NYUSHUKKO_DATE, drTxt.NONYU_SAKI Into Group
                    Select New SumUpData With {
                        .SHUKKO_DATE = NYUSHUKKO_DATE,
                        .NONYU_SAKI = NONYU_SAKI,
                        .SHUKKO_SU = Group.Sum(Function(it) it.SHUKKO_SU),
                        .YUSO_RYO = Group.Sum(Function(it) it.YUSO_RYO)
                    }

        '出庫数>0で絞り込み
        Dim queryFilter As IEnumerable(Of SumUpData) = From sumUpData In querySumUp.AsEnumerable()
                    Order By sumUpData.SHUKKO_DATE Ascending, sumUpData.NONYU_SAKI Ascending
                    Where sumUpData.SHUKKO_SU > 0
                    Select sumUpData


        Dim dtPrt As LMI990DS.LMI990OUT_PRTDataTable = ds.LMI990OUT_PRT

        For Each item As SumUpData In queryFilter
            'DataRowに値を設定
            Dim drPrt As LMI990DS.LMI990OUT_PRTRow = dtPrt.NewLMI990OUT_PRTRow()
            drPrt.SHUKKO_DATE = item.SHUKKO_DATE.ToString()
            drPrt.NONYU_SAKI = item.NONYU_SAKI
            drPrt.SHUKKO_SU = item.SHUKKO_SU.ToString()
            drPrt.YUSO_RYO = item.YUSO_RYO.ToString

            'DataRowをDataTableに追加
            dtPrt.AddLMI990OUT_PRTRow(drPrt)
        Next

        Return ds

    End Function

#End Region '集計処理

#Region "文字列操作"

    ''' <summary>
    ''' 文字列からバイト数を指定して部分文字列を取得する。
    ''' </summary>
    ''' <param name="value">対象文字列。</param>
    ''' <param name="startIndex">開始位置。（バイト数）</param>
    ''' <param name="length">長さ。（バイト数）</param>
    ''' <returns>部分文字列。</returns>
    ''' <remarks>文字列は <c>Shift_JIS</c> でエンコーディングして処理を行います。</remarks>
    Private Function SubstringByte(ByVal value As String, ByVal startIndex As Integer, ByVal length As Integer) As String

        Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")
        Dim byteArray() As Byte = sjisEnc.GetBytes(value)

        If byteArray.Length < startIndex + 1 Then
            Return ""
        End If

        If byteArray.Length < startIndex + length Then
            length = byteArray.Length - startIndex
        End If

        Dim cut As String = sjisEnc.GetString(byteArray, startIndex, length)

        ' 最初の文字が全角の途中で切れていた場合はカット
        Dim left As String = sjisEnc.GetString(byteArray, 0, startIndex + 1)
        Dim first As Char = value(left.Length - 1)
        If 0 < cut.Length AndAlso Not first = cut(0) Then
            cut = cut.Substring(1)
        End If

        ' 最後の文字が全角の途中で切れていた場合はカット
        left = sjisEnc.GetString(byteArray, 0, startIndex + length)

        Dim last As Char = value(left.Length - 1)
        If 0 < cut.Length AndAlso Not last = cut(cut.Length - 1) Then
            cut = cut.Substring(0, cut.Length - 1)
        End If

        Return cut

    End Function

#End Region '文字列操作

#End Region 'Method

End Class