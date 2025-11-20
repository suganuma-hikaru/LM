' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI450  : 
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System
Imports System.IO
Imports System.Reflection
Imports System.Collections
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports AdvanceSoftware.ExcelCreator        'ExcelCreator(本体)
Imports AdvanceSoftware.ExcelCreator.Xlsx   'ExcelCreator(2007以降)
Imports Microsoft.VisualBasic

''' <summary>
''' LMI450ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI450H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI450V = Nothing

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI450G = Nothing

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG = Nothing

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH = Nothing

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV = Nothing

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean = False


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly BLF_NAME As String = LMI450C.MY_FORM_ID & LMControlC.BLF

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
        Dim frm As LMI450F = New LMI450F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI450G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI450V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        'Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        '画面の入力項目の制御
        'Call Me._G.SetControlsStatus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI450C.EventShubetsu _
                           , ByVal frm As LMI450F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = New DataSet()

        Try

            '権限チェック
            If _V.IsAuthorityChk(eventShubetsu) = False Then
                MyBase.ShowMessage(frm, "E016")
                Exit Sub
            End If

            Select Case eventShubetsu

                Case LMI450C.EventShubetsu.EXECUTE

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    '入力チェック
                    If Me._V.IsSingleCheck(eventShubetsu) = False Then
                        '処理終了アクション
                        Exit Sub
                    End If
                    Dim ds As DataSet = New LMI450DS()

                    'エクセルファイル読み込み
                    If ReadExcelFile(frm, ds) = False Then
                        Me._LMIConH.EndAction(frm, "S001")
                        Exit Sub
                    End If

                    'EDI用データ読み込み
                    rtnDs = GetEdiData(frm, ds)
                    'メッセージコードの判定
                    If MyBase.IsMessageStoreExist = True Then
                        'EXCEL起動 
                        MyBase.MessageStoreDownload(True)
                        MyBase.ShowMessage(frm, "E235")
                        Exit Sub
                    End If

                    'EDIファイル作成
                    If CreateEdiFile(frm, rtnDs) = False Then
                        Me._LMIConH.EndAction(frm, "S001")
                        Exit Sub
                    End If

                    Call ExecImportEdiBatch()

                Case LMI450C.EventShubetsu.CLOSE_FORM

                    frm.Close()
                    If (frm IsNot Nothing AndAlso _
                        frm.IsDisposed = False) Then
                        frm.Dispose()
                    End If

            End Select

        Finally

            If (frm IsNot Nothing AndAlso _
                frm.IsDisposed = False) Then
                '処理終了アクション
                Me._LMIConH.EndAction(frm, LMI450C.MESSAGE_ID.NORMAL)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI450F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI450F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI450C.EventShubetsu.EXECUTE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub



    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI450F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI450C.EventShubetsu.CLOSE_FORM, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI450F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

#End Region 'イベント振分け

#Region "処理"

#Region "EDIデータ取得"
    ''' <summary>
    ''' EDIデータ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetEdiData(ByVal frm As LMI450F, ByVal ds As DataSet) As DataSet

        'Dim ds As DataSet = New LMI450DS()
        Dim dt As DataTable = ds.Tables(LMI450C.TABLE_NM.INPUT)
        Dim dr As DataRow = dt.NewRow
        dr.Item("NRS_BR_CD") = Base.LMUserInfoManager.GetNrsBrCd
        dr.Item("DEST_ADDR") = String.Empty
        dr.Item("GOODS_CD_CUST") = String.Empty
        dt.Rows.Add(dr)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI450BLF", "GetEdiData", ds)

        Return rtnDs

    End Function

#End Region

#Region "エクセルファイル読み込み"
    ''' <summary>
    ''' エクセルファイル読み込み
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ReadExcelFile(ByVal frm As LMI450F, ByVal ds As DataSet) As Boolean

        Dim inTbl As DataTable = ds.Tables(LMI450C.TABLE_NM.EXCEL)
        Dim inRow As DataRow
        Dim brCd As String = Base.LMUserInfoManager.GetNrsBrCd

        Try

            '---------- ExcelCreatorでExcelファイルを読み込み開始 -------
            ' ExcelCreator インスタンス生成
            Dim components As System.ComponentModel.Container = New System.ComponentModel.Container()
            Dim excelCreator As AdvanceSoftware.ExcelCreator.Xlsx.XlsxCreator = New XlsxCreator(components)

            ' Excel ファイル (インポートファイル) を読み取り専用でオープンします。
            excelCreator.ReadBook(frm.txtLocalPath.TextValue)

            ' 該当のシートをアクティブ化
            excelCreator.ActiveSheet = 1

            ' データが設定されたセルの最大行と最大列の交点座標を取得します。
            Dim maxData As System.Drawing.Point = excelCreator.GetMaxData(AdvanceSoftware.ExcelCreator.MaxEndPoint.MaxPoint)

            'Excelデータの読み込み（データ設定された行を全て読み込む）
            For row As Integer = 2 To maxData.Y + 1
                If excelCreator.Cell("A" & row).Value Is Nothing _
                    OrElse IsDBNull(excelCreator.Cell("A" & row).Value) = True _
                    OrElse excelCreator.Cell("A" & row).Value.ToString.Length = 0 Then
                    Exit For
                End If
                ' データテーブルにExcelのデータを設定
                inRow = inTbl.NewRow()
                inRow.Item("SHUKKA_NO") = CStr(excelCreator.Cell("A" & row).Value)                                      '出荷伝票
                inRow.Item("PLANT_CD") = CStr(excelCreator.Cell("B" & row).Value)                                       'プラント
                inRow.Item("NOUKI") = CDate(excelCreator.Cell("C" & row).FormattedString).ToString("yyyyMMdd")          '納入期日 (開始/終了)
                inRow.Item("SEIHIN_CD") = CStr(excelCreator.Cell("D" & row).Value)                                      '品目
                inRow.Item("SEIHIN_NM") = CStr(excelCreator.Cell("E" & row).Value)                                      'テキスト
                inRow.Item("JURYO") = CStr(excelCreator.Cell("F" & row).FormattedString)                                '正味重量
                inRow.Item("JURYO_TANI") = CStr(excelCreator.Cell("G" & row).Value)                                     '重量単位
                inRow.Item("LOT_NO") = CStr(excelCreator.Cell("H" & row).Value)                                         'ロット
                inRow.Item("SHUKKA_BI") = CDate(excelCreator.Cell("I" & row).FormattedString).ToString("yyyyMMdd")      '出庫日付
                inRow.Item("HOKAN_BASHO") = CStr(excelCreator.Cell("J" & row).Value)                                    '出荷ポイント/入荷ポイント
                inRow.Item("KOSU") = CStr(excelCreator.Cell("K" & row).Value)                                           '出荷数量
                inRow.Item("SEIHIN_TANI") = CStr(excelCreator.Cell("L" & row).Value)                                    '販売単位
                inRow.Item("SHUKKA_CD") = CStr(excelCreator.Cell("M" & row).Value)                                      '出荷先
                inRow.Item("SHUKKA_NM") = CStr(excelCreator.Cell("N" & row).Value)                                      '出荷先の名称
                inRow.Item("SHUKKA_JUSHO") = CStr(excelCreator.Cell("O" & row).Value)                                   '出荷先住所
                inRow.Item("HANBAI_SOSHIKI") = CStr(excelCreator.Cell("P" & row).Value)                                 '販売組織
                inRow.Item("YUSO_KEIRO") = CStr(excelCreator.Cell("Q" & row).Value)                                     '輸送経路
                inRow.Item("SOU_JURYO") = CStr(excelCreator.Cell("R" & row).FormattedString)                            '総重量
                inTbl.Rows.Add(inRow)
            Next
            ' ブックをクリアします。
            excelCreator.CloseBook(True)
            '---------- ExcelCreatorでExcelファイルを読み込み終了 -------

            Return True

        Catch ex As Exception
            MyBase.ShowMessage(frm, "S001", New String() {"エクセルファイルの読み込み"})
            Return False
        End Try

    End Function

#End Region

#Region "EDIファイル作成"
    ''' <summary>
    '''EDIファイル作成処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CreateEdiFile(ByVal frm As LMI450F, ds As DataSet) As Boolean

        '出力ディレクトリ取得
        Dim outDir As String = GetDirectory()
        Dim outFileNm As String = GetFileName()
        Dim localPath As String = Left(frm.txtLocalPath.TextValue, InStrRev(frm.txtLocalPath.TextValue, "\"))
        Dim outFullPath As String = String.Concat(localPath, outFileNm)

        'レコードを並び替える
        Dim dv As New DataView(ds.Tables(LMI450C.TABLE_NM.EDI))
        dv.Sort = "SHUKKA_NO,SHUKKA_BI,SHUKKA_CD"
        Dim dt As DataTable = dv.ToTable

        Dim sw As New System.IO.StreamWriter(outFullPath, _
                                                False, _
                                                System.Text.Encoding.GetEncoding("shift_jis"))
        Dim line As String

        Try

            For Each dr As DataRow In dt.Rows

                line = String.Empty
                line += dr.Item("PLANT_CD").ToString
                line += dr.Item("SHUKKA_CD").ToString
                line += dr.Item("SHUKKA_NM").ToString
                line += dr.Item("SHUKKA_SHITEN_NM").ToString
                line += dr.Item("SHUKKA_JUSHO_1").ToString
                line += dr.Item("SHUKKA_JUSHO_2").ToString
                line += dr.Item("SHUKKA_JUSHO_3").ToString
                line += dr.Item("SHUKKA_BI").ToString
                line += dr.Item("NOUKI").ToString
                line += dr.Item("SEIHIN_CD").ToString
                line += dr.Item("SEIHIN_NM").ToString
                line += dr.Item("SEIHIN_TANI").ToString
                line += dr.Item("LOT_NO").ToString
                line += dr.Item("KOSU").ToString
                line += dr.Item("JURYO").ToString
                line += dr.Item("NIZAI_JURYO").ToString
                line += dr.Item("TOUSHA_ORDER").ToString
                line += dr.Item("HOKAN_BASHO").ToString
                line += dr.Item("SHUKKA_NO").ToString
                line += dr.Item("KOMOKU_1").ToString
                line += dr.Item("KOMOKU_2").ToString
                line += dr.Item("JUCHUSAKI_CD").ToString
                line += dr.Item("YOHAKU").ToString
                line += vbNewLine

                sw.Write(line)

            Next

            sw.Close()

        Catch ex As Exception
            MyBase.ShowMessage(frm, "S001", New String() {"エクセルファイルの作成"})
            sw.Close()
            Return False

        End Try

        'EDIフォルダに移動(コピー＋削除)
        Dim ediFilePath As String = String.Concat(outDir, outFileNm)
        System.IO.File.Copy(outFullPath, ediFilePath, True)
        System.IO.File.Delete(outFullPath)

        Return True

    End Function


    ''' <summary>
    ''' EDI取込バッチ実行処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExecImportEdiBatch()

        Dim pstPath As String = GetPSToolPath()
        Dim pstSvFld As String = Mid(pstPath, InStrRev(pstPath, System.IO.Path.DirectorySeparatorChar) + 1)
        If Directory.Exists(pstPath) = False Then
            'フォルダを作る
            Directory.CreateDirectory(pstPath)
            '
            If pstPath.Chars((pstPath.Length - 1)).Equals(System.IO.Path.DirectorySeparatorChar) = False Then
                pstPath = pstPath + System.IO.Path.DirectorySeparatorChar
            End If

            'コピー元のディレクトリにあるファイルをコピー
            Dim copyFrom As String = GetBatchPath()
            copyFrom = Left(copyFrom, InStrRev(copyFrom, System.IO.Path.DirectorySeparatorChar))
            copyFrom = String.Concat(copyFrom, pstSvFld)
            Dim fs As String() = System.IO.Directory.GetFiles(copyFrom)
            Dim f As String
            For Each f In fs
                System.IO.File.Copy(f, pstPath + System.IO.Path.GetFileName(f), True)
            Next

        End If

        'ProcessStartInfoオブジェクトを作成する
        Dim psi As New System.Diagnostics.ProcessStartInfo()
        '起動する実行ファイルのパスを設定する
        psi.FileName = GetBatchPath()
        '起動する
        Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(psi)

    End Sub

#End Region

#Region "選択ボタン押下"
    ''' <summary>
    ''' 選択ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnFileSelectClick(ByVal frm As LMI450F, ByVal e As System.EventArgs)

        ''初期処理
        'Me.StartAction(frm)

        With frm

            '画面解除
            Me.UnLockedControls(frm)

            Dim setFileNm As String = String.Empty
            Dim fileStream As Stream
            Dim openFileDialog As New OpenFileDialog()

            openFileDialog.InitialDirectory = "c:\"
            openFileDialog.Filter = "All files (*.*)|*.*"
            openFileDialog.FilterIndex = 1
            openFileDialog.RestoreDirectory = True
            openFileDialog.Multiselect = False

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                fileStream = openFileDialog.OpenFile()
                If Not (fileStream Is Nothing) Then

                    '格納場所定義
                    Dim strDR As String = Me.GetDirectory()
                    Dim localPath As String = String.Empty
                    Dim fileNM As String = String.Empty

                    Dim max As Integer = openFileDialog.FileNames.Count - 1
                    '選択ファイルは1件のみ
                    If max > 0 Then
                        '１件以上はエラー
                        'Cursorを元に戻す
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End If

                    'ファイル名設定
                    localPath = openFileDialog.FileNames(0)
                    fileNM = localPath.Substring(localPath.LastIndexOf("\") + 1)

                    'ローカル日付取得
                    'Dim LocalDateMD As String = Mid(MyBase.GetLocalSystemDate(), 5, 4)
                    'Dim LocalTimeHMS As String = Mid(MyBase.GetLocalSystemTime(), 1, 6)
                    'Dim setChk As Integer = InStr(fileNM, ".")
                    ''ファイル名 + "_" + 月日時分秒 + "."以降をセット（ﾌｧｲﾙﾀｲﾌﾟ）
                    'Dim setNM As String = Mid(fileNM, 1, setChk - 1) + "_" + LocalDateMD + LocalTimeHMS
                    'setFileNm = setNM + Mid(fileNM, setChk)
                    setFileNm = fileNM
                    If MyBase.IsMessageExist() = True Then
                        'Cursorを元に戻す
                        Cursor.Current = Cursors.Default
                        MyBase.ShowMessage(frm)
                        Exit Sub
                    End If

                    .txtFilePath.TextValue = strDR
                    .txtFileName.TextValue = setFileNm.ToString
                    .txtLocalPath.TextValue = localPath.ToString

                End If

                'メッセージを表示する
                Me.ShowMessage(frm, "G003")

            End If

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default

            'フォーカスの設定
            Call Me._G.SetFoucus()

        End With
    End Sub

#End Region

#Region "出力ディレクトリ取得"
    ''' <summary>
    ''' 出力ディレクトリ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetDirectory() As String

        Dim sqlKbn As String = String.Concat("KBN_GROUP_CD = '" & LMI450C.SAVE_FILE_KBN.GROUP_CD & "' AND KBN_CD = '", LMI450C.SAVE_FILE_KBN.KBN_CD, "'")
        Dim drKbn As DataRow() = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sqlKbn)

        '格納場所定義
        Dim strRootDR As String = drKbn(0).Item(LMI450C.SAVE_FILE_KBN.OUT_PATH).ToString

        Dim strDR As String = String.Concat(strRootDR, "\")

        Return strDR

    End Function
#End Region

#Region "出力ファイル名取得"
    ''' <summary>
    ''' 出力ファイル名取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetFileName() As String

        Dim sqlKbn As String = String.Concat("KBN_GROUP_CD = '" & LMI450C.SAVE_FILE_KBN.GROUP_CD & "' AND KBN_CD = '", LMI450C.SAVE_FILE_KBN.KBN_CD, "'")
        Dim drKbn As DataRow() = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sqlKbn)

        'ファイル名定義
        Dim strFileNm As String = drKbn(0).Item(LMI450C.SAVE_FILE_KBN.OUT_FILE_NM).ToString

        Return strFileNm

    End Function
#End Region

#Region "EDI取込バッチパス取得"
    ''' <summary>
    ''' EDI取込バッチパス取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetBatchPath() As String

        Dim sqlKbn As String = String.Concat("KBN_GROUP_CD = '" & LMI450C.SAVE_FILE_KBN.GROUP_CD & "' AND KBN_CD = '", LMI450C.SAVE_FILE_KBN.KBN_CD, "'")
        Dim drKbn As DataRow() = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sqlKbn)

        '格納場所定義
        Dim strBatchPath As String = drKbn(0).Item(LMI450C.SAVE_FILE_KBN.BATCH_PATH).ToString

        Return strBatchPath

    End Function
#End Region

#Region "PSTOOLパス取得"
    ''' <summary>
    ''' PSTOOLパス取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetPSToolPath() As String

        Dim sqlKbn As String = String.Concat("KBN_GROUP_CD = '" & LMI450C.SAVE_FILE_KBN.GROUP_CD & "' AND KBN_CD = '", LMI450C.SAVE_FILE_KBN.KBN_CD, "'")
        Dim drKbn As DataRow() = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sqlKbn)

        '格納場所定義
        Dim strPath As String = drKbn(0).Item(LMI450C.SAVE_FILE_KBN.PSTOOL_PATH).ToString

        Return strPath

    End Function
#End Region

#End Region

#End Region 'Method

End Class

