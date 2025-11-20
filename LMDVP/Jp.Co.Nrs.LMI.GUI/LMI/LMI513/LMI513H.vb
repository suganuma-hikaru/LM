' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI513H : JNC_運賃照合作成
'  作  成  者       :  daikoku
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
''' LMI513ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI513H
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
    Private _V As LMI513V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI513G

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
        Dim frm As LMI513F = New LMI513F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI513G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI513V(Me, frm, Me._LMIconV, Me._LMIconG)

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

        'コントロールの日付書式設定
        Call Me._G.SetDateControl()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(frm, LMI513C.MSG_INIT)

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
    Private Function Process(ByVal frm As LMI513F) As Boolean

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI513C.EventShubetsu.SAKUSEI) = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)
            Return False

        End If

        'DataSet設定
        Dim ds As LMI513DS = New LMI513DS()
        Dim drIn As LMI513DS.LMI513INRow = ds.LMI513IN.NewLMI513INRow
        drIn.NRS_BR_CD = frm.cmbEigyo.SelectedValue.ToString
        drIn.SYORI_DATE = frm.imdSyoriDate.TextValue
        ds.LMI513IN.AddLMI513INRow(drIn)

        '取込ファイル存在チェック()
        If Not File.Exists(LMI513C.FILE_PATH) Then

            'メッセージ表示()
            MyBase.ShowMessage(frm, "E657")
            '処理終了アクション()
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)

            Return False
        End If

        '取込処理
        ds = TorikomiText(frm, ds)

        '取込内容チェック
        If TorikomiCheck(frm, ds) = False Then

            '処理終了アクション()
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)

            Return False

        End If

        'Excel出力
        Me.ExcelOut(ds, frm)

        If MyBase.IsErrorMessageExist = True Then
            '更新失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)

            Return False

        End If

        'メッセージ蓄積の判定
        If MyBase.IsMessageStoreExist = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)

            Return False
        End If

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G002", {"エクセル作成", ""})

        '処理終了アクション
        Call Me._LMIconH.EndAction(frm, LMI513C.MSG_INIT)

        Return True

    End Function


    ''' <summary>
    ''' Excel出力処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Sub ExcelOut(ByVal ds As DataSet, ByVal frm As LMI513F)

        Try

            Dim resultFlg As Boolean = True

            resultFlg = Me.ExcelPrint(ds, "LMI513", String.Concat("JNC_運賃照合"))

        Catch ex As Exception

        End Try

    End Sub


    ''' <summary>
    ''' エクセル出力処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="pgid"></param>
    ''' <param name="fileNm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function ExcelPrint(ByVal ds As DataSet, ByVal pgid As String, ByVal fileNm As String) As Boolean

        Try
            'ExcelCreatorの生成
            Dim xls As LM.Utility.ExcelCreatorUtility8 = New LM.Utility.ExcelCreatorUtility8

            'Excel出力を開始します。
            If xls.PrintXls(pgid, ds.Tables(LMI513C.TABLE_NM_IN_TXT), pgid, fileNm) = False Then
                Return False
            End If

        Catch ex As Exception
            ' 例外が発生した時の処理(不適切なファイルを取込んだ場合)
            MyBase.SetMessage("S002")
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' EXCELファイルチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExcelFileCheck(ByVal selectKB As String, ByRef xlsPath As String, ByRef xlsFileName As String, ByRef xlsSavePath As String)

        'ファイルパス取得
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S113' AND ", _
                                                                                                             "KBN_CD = '02'"))
        xlsPath = xlsPathKbn(0).Item("KBN_NM2").ToString
        xlsFileName = xlsPathKbn(0).Item("KBN_NM3").ToString
        xlsSavePath = xlsPathKbn(0).Item("KBN_NM4").ToString

        'フォルダの存在確認
        If System.IO.Directory.Exists(xlsPath) = False Then
            MyBase.SetMessage("E296", New String() {"EXCELテンプレートフォルダ"})
            Return
        End If

        'ファイルの存在確認
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = False Then
            MyBase.SetMessage("E492", New String() {"EXCELテンプレートフォルダ", "ファイル", ""})
            Return
        End If

        'フォルダの存在確認
        If System.IO.Directory.Exists(xlsSavePath) = False Then
            '無ければ作成する
            System.IO.Directory.CreateDirectory(xlsSavePath)
        End If

    End Sub

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し(処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI513F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMI513F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByRef frm As LMI513F, ByVal e As FormClosingEventArgs)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function TorikomiText(ByVal frm As LMI513F, ByVal ds As LMI513DS) As LMI513DS

        'ファイルオープン
        Using sr As New System.IO.StreamReader(LMI513C.FILE_PATH, System.Text.Encoding.GetEncoding("Shift_JIS"))

            Dim rowNo As Integer = 0

            While (sr.Peek() > -1)

                '1行読み込み
                Dim rec As String = sr.ReadLine()

                '行番号加算
                rowNo += 1

                If rowNo <> 1 Then
                    '２件目からデータ

                    'DataRowに値を設定
                    Dim drTxt As LMI513DS.LMI513IN_TXTRow = ds.LMI513IN_TXT.NewLMI513IN_TXTRow()

                    With drTxt

                        '------------------

                        'カラムカウント
                        Dim columnCount As Integer = 1

                        For Each aryf As String In Split(rec, ",")

                            'ダブルクォーテーションで囲まれていた場合は、除外した文字列をセットする
                            If Left(aryf, 1).Equals(Chr(34)) And Right(aryf, 1).Equals(Chr(34)) Then
                                aryf = Mid(aryf, 2, Len(aryf) - 2)
                            End If

                            Select Case columnCount
                                Case 1
                                    .INFO_KBN_CD = aryf.Trim            '   情報区分コード
                                Case 2
                                    .DATA_DATE = aryf.Trim              '   データ作成日
                                Case 3
                                    .TEISEI_CD = aryf.Trim              '   訂正コード
                                Case 4
                                    .DATA_CRT_TIME = aryf.Trim          '	データ作成時刻
                                Case 5
                                    .UNSOIRAI_NO = aryf.Trim            '	運送依頼番号
                                Case 6
                                    .UNSO_SYUDAN_CD = aryf.Trim         '	運送手段コード
                                Case 7
                                    .TUMIAWASE_NO = aryf.Trim           '	積み合せ番号
                                Case 8
                                    .NIOKURI_CD = aryf.Trim             '	荷送人コード
                                Case 9
                                    .NIOKURI_NMK = aryf.Trim            '	荷送人名漢字
                                Case 10
                                    .UNSO_JIGYO_CD = aryf.Trim          '	運送事業者コード
                                Case 11
                                    .IUNSO_JIGYO_NM_K = aryf.Trim       '	運送事業者名漢字
                                Case 12
                                    .OUTBASYO_CD = aryf.Trim            '	出荷場所コード
                                Case 13
                                    .OUTBASYO_NMK = aryf.Trim           '	出荷場所名漢字
                                Case 14
                                    .OUTBASYO_BCD = aryf.Trim           '	出荷場所部門コード
                                Case 15
                                    .OUTBASYO_B_NMK = aryf.Trim         '	出荷場所部門名漢字
                                Case 16
                                    .NTODOKE_CD = aryf.Trim             '	荷届先コード
                                Case 17
                                    .NTODOKE_NMK = aryf.Trim            '	荷届先名漢字
                                Case 18
                                    .NTODOKE_ADD_CD = aryf.Trim         '	荷届先住所コード
                                Case 19
                                    .JYURYO_CD = aryf.Trim              '	受注者品名コード
                                Case 20
                                    .NISUGATA_CD = aryf.Trim            '	荷姿コード
                                Case 21
                                    .NAIRYO_SURYO_TANI_CD = aryf.Trim   '	内容数量単位コード
                                Case 22
                                    .SURYOU_HOUKOKU = aryf.Trim         '	数量報告
                                Case 23
                                    .MEISAI_NO = aryf.Trim              '	明細番号
                                Case 24
                                    .GOODS_NM1_KANA = aryf.Trim         '	商品名1カナ
                                Case 25
                                    .GOODS_NM2_KANA = aryf.Trim         '	商品名2カナ
                                Case 26
                                    .INVOICE_NO = aryf.Trim             '	請求書番号
                                Case 27
                                    .UNSO_KYORI = aryf.Trim             '	運送距離
                                Case 28
                                    .INV_MEISAI_NO = aryf.Trim          '	請求明細番号
                                Case 29
                                    .INV_YM = aryf.Trim                 '	請求年月
                                Case 30
                                    .UNCHIN_TOTAL = aryf.Trim           '	運賃総合計課税
                                Case 31
                                    .UMCHIN_MEISAI_COM = aryf.Trim      '	運賃明細コメント
                                Case 32
                                    .TAX = aryf.Trim                    '	消費税額
                                Case 33
                                    .TOTAL = aryf.Trim                  '	合計金額
                                Case 34
                                    .BIKOU_K = aryf.Trim                '	備考漢字
                                Case 35
                                    .OUT_DATE = String.Concat(Mid(aryf.Trim, 1, 4), "/", Mid(aryf.Trim, 5, 2), "/", Mid(aryf.Trim, 7, 2))                '	出荷日

                            End Select

                            columnCount += 1
                        Next

                        'DataRowをDataTableに追加
                        ds.LMI513IN_TXT.AddLMI513IN_TXTRow(drTxt)

                    End With

                End If

            End While

        End Using

        Return ds

    End Function

#End Region '取込処理

#Region "取込内容チェック"
    Private Function TorikomiCheck(ByVal frm As LMI513F, ByVal ds As LMI513DS) As Boolean

        Dim rowCnt As Integer = ds.Tables(LMI513C.TABLE_NM_IN_TXT).Rows.Count - 1

        '画面の処理年月
        Dim syoriYM As String = frm.imdSyoriDate.TextValue.Substring(0, 6)

        For i As Integer = 0 To rowCnt

            '出荷日
            Dim outYM As String = ds.Tables(LMI513C.TABLE_NM_IN_TXT).Rows(i).Item("OUT_DATE").ToString.Substring(0, 7)
            'スラッシュを取る
            outYM = outYM.Replace("/", "")

            If syoriYM.Equals(outYM) = False Then
                MyBase.ShowMessage(frm, "E028", {"画面の処理年月とデータ出荷日が不一致", "取込"})

                Exit Function


            End If

        Next

        TorikomiCheck = True

    End Function

#End Region

#Region "集計処理"

    ''' <summary>
    ''' 集計処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SumUp(ByVal frm As LMI513F, ByVal ds As LMI513DS) As LMI513DS

        '集計
        'Dim querySumUp As IEnumerable(Of SumUpData) = From drTxt In ds.LMI513IN_TXT.AsEnumerable()
        '            Order By drTxt.NYUSHUKKO_DATE Ascending, drTxt.NONYU_SAKI Ascending
        '            Group By drTxt.NYUSHUKKO_DATE, drTxt.NONYU_SAKI Into Group
        '            Select New SumUpData With {
        '                .SHUKKO_DATE = NYUSHUKKO_DATE,
        '                .NONYU_SAKI = NONYU_SAKI,
        '                .SHUKKO_SU = Group.Sum(Function(it) it.SHUKKO_SU),
        '                .YUSO_RYO = Group.Sum(Function(it) it.YUSO_RYO)
        '            }

        ''出庫数>0で絞り込み
        'Dim queryFilter As IEnumerable(Of SumUpData) = From sumUpData In querySumUp.AsEnumerable()
        '            Order By sumUpData.SHUKKO_DATE Ascending, sumUpData.NONYU_SAKI Ascending
        '            Where sumUpData.SHUKKO_SU > 0
        '            Select sumUpData


        'Dim dtPrt As LMI513DS.LMI513OUT_PRTDataTable = ds.LMI513OUT_PRT

        'For Each item As SumUpData In queryFilter
        '    'DataRowに値を設定
        '    Dim drPrt As LMI513DS.LMI513OUT_PRTRow = dtPrt.NewLMI513OUT_PRTRow()
        '    drPrt.SHUKKO_DATE = item.SHUKKO_DATE.ToString()
        '    drPrt.NONYU_SAKI = item.NONYU_SAKI
        '    drPrt.SHUKKO_SU = item.SHUKKO_SU.ToString()
        '    drPrt.YUSO_RYO = item.YUSO_RYO.ToString

        '    'DataRowをDataTableに追加
        '    dtPrt.AddLMI513OUT_PRTRow(drPrt)
        'Next

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