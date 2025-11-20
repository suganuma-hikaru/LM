' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU020H   : スキャン取込
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.LMU020C
Imports System.IO
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMU020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' 2006/07/21 IWAMOTO
''' </histry>
Public Class LMU020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Validate As LMU020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gamen As LMU020G

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMU010F

    ''' <summary>
    ''' キャッシュデータセットを格納するフィールド
    ''' </summary>
    ''' <remarks>現在選択されているデータを保持します。</remarks>
    Private _SessionDS As DataSet

    ''' <summary>
    ''' ファイル取込処理時カウントエリア
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataMatchCnt As Integer = 0
    Private _dataUnMatchCnt As Integer = 0
    Private _dataNothingCnt As Integer = 0

    ''' <summary>
    ''' ファイル取込処理時重複（Duplication）エリア
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataMatchDupCnt As Integer = 0
    Private _dataUnMatchDupCnt As Integer = 0
    Private _dataNothingDupCnt As Integer = 0


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
        Dim frm As LMU020F = New LMU020F(Me)

        'Validateクラスの設定
        Me._Validate = New LMU020V(Me, frm)

        'Gamenクラスの設定
        Me._Gamen = New LMU020G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._Gamen.SetFunctionKey()

        'タブインデックスの設定
        Call Me._Gamen.SetTabIndex()

        'コントロール個別設定
        Call Me._Gamen.SetControl(frm)

        'スプレッドの設定
        Call Me._Gamen.InitSpread()


        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。


        'メッセージの表示
        Me.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        ''Call _Gamen.SetControlsStatus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMU020F, ByVal e As FormClosingEventArgs)

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"


#Region "ユーティリティ"

#End Region 'ユーティリティ

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        Call Me.ReadScanData(frm)

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        Call Me.OpenFolder(frm)

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。 

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。 

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "")

        'ここから各処理を呼び出してください。  
        Call Me.SaveDataListData(frm)

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMU020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey12Press")

        Call Me.Close(frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMU020F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        'ここから各処理を呼び出してください。 
        Call Me.CloseForm(frm, e)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

    ''' <summary>
    ''' データセットに条件を代入する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataToDataSet(ByRef frm As LMU020F) As DataSet

        Dim ds As DataSet = New LMU020DS

        Dim conditionRow As DataRow = ds.Tables("LMU020IN").NewRow

        With frm.sprFileImport.Sheets(0)

        End With

        ds.Tables("LMU020IN").Rows.Add(conditionRow)

        Return ds

    End Function
    ''' <summary>
    ''' データセット設定(保存)
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetImportData(ByRef ds As DataSet, ByRef frm As LMU020F)



        'フォルダパス取得
        Dim KbnDt As DataTable = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN)

        ' * 第１階層 *
        Dim fld1Dr As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD1_ROOT & "'")
        Dim fld1Nm As String = fld1Dr(0).Item("KBN_NM1").ToString()

        ' * 第２階層 *
        '   入力フォルダ
        Dim fld2DrIn As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_RESULT & "'")
        Dim fld2NmIn As String = fld2DrIn(0).Item("KBN_NM1").ToString()
        '   出力フォルダ
        'Dim fld2DrOut As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'D021' AND KBN_CD = '" & LMU020C.D021_FLD2_SAVEOUT & "'")
        'Dim fld2NmOut As String = fld2DrOut(0).Item("KBN_NM1").ToString()

        ' * 第３階層 *
        '   入力フォルダ
        Dim fld3NmBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()

        '***フォルダフルパス***
        ' 入力フォルダ
        Dim folderIn As String = String.Concat(fld1Nm, "\", _
                                             fld2NmIn, "\", _
                                             fld3NmBrCd)

        '   -- 取込実行結果出力フォルダ
        'Dim folderOut As String = String.Concat(fld1Nm, "\", _
        '                                              fld2NmOut)

        '格納場所定義
        Dim strDR As String = Me.GetDirectory(frm)
        Dim localPath As String = String.Empty
        Dim fileNM As String = String.Empty     'コピー元ファイル名
        Dim fileNMCopy As String = String.Empty 'コピー先ファイル名


        Dim chkInfiles As String() = System.IO.Directory.GetFiles(folderIn, "*", System.IO.SearchOption.TopDirectoryOnly)

        Dim dr As DataRow = Nothing
        Dim max As Integer = frm.sprFileImport.ActiveSheet.RowCount - 1
        Dim recNo As Integer = 0
        Dim sheet As FarPoint.Win.Spread.SheetView = frm.sprFileImport.ActiveSheet

        With frm

            For i As Integer = 0 To max

                dr = ds.Tables(LMU020C.TABLE_NM_OUT).NewRow()

                'ファイル名
                fileNM = Me._Gamen.GetCellValue(sheet.Cells(i, LMU020G.SprImportFileDef.FILE_NM.ColNo))

                'コピー先ファイル名設定
                Dim sysDateTime() As String = GetSystemDateTime()   'システム日時を取得
                fileNMCopy = String.Concat(sysDateTime(0), sysDateTime(1), "_", fileNM)


                dr.Item("ENT_SYSID_KBN") = "06"      'SystemID区分
                dr.Item("KEY_TYPE_KBN") = "14"        'キー種別
                dr.Item("KEY_NO") = Me._Gamen.GetCellValue(sheet.Cells(i, LMU020G.SprImportFileDef.ORD_NO.ColNo))                    'キー番号
                dr.Item("KEY_NO_SEQ") = "0"                                   '0固定
                dr.Item("FILE_TYPE_KBN") = Me._Gamen.GetCellValue(sheet.Cells(i, LMU020G.SprImportFileDef.FILE_TYPE.ColNo))
                dr.Item("REMARK") = "ファイル一括取込"
                dr.Item("FILE_PATH") = strDR
                dr.Item("FILE_NM") = fileNMCopy
                dr.Item("UPD_FLG") = "0"
                dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                ds.Tables(LMU020C.TABLE_NM_OUT).Rows.Add(dr)

                File.Move(folderIn + "\" + fileNM, strDR + "\" + fileNMCopy)

            Next

        End With


    End Sub
    ''' <summary>
    ''' 取込データ削除
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub DeleteImportData(ByRef ds As DataSet, ByRef frm As LMU020F)

        'フォルダパス取得
        Dim KbnDt As DataTable = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN)

        ' * 第１階層 *
        Dim fld1Dr As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD1_ROOT & "'")
        Dim fld1Nm As String = fld1Dr(0).Item("KBN_NM1").ToString()

        ' * 第２階層 *
        '   入力フォルダ
        Dim fld2DrIn As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_RESULT & "'")
        Dim fld2NmIn As String = fld2DrIn(0).Item("KBN_NM1").ToString()

        '***フォルダフルパス***
        ' 入力フォルダ
        Dim folderIn As String = String.Concat(fld1Nm, "\", _
                                             fld2NmIn)

        '格納場所定義
        Dim strDR As String = Me.GetDirectory(frm)
        Dim localPath As String = String.Empty
        Dim fileNM As String = String.Empty     'コピー元ファイル名
        Dim fileNMCopy As String = String.Empty 'コピー先ファイル名


        Dim chkInfiles As String() = System.IO.Directory.GetFiles(folderIn, "*", System.IO.SearchOption.TopDirectoryOnly)

        Dim dr As DataRow = Nothing
        Dim max As Integer = frm.sprFileImport.ActiveSheet.RowCount - 1
        Dim recNo As Integer = 0
        Dim sheet As FarPoint.Win.Spread.SheetView = frm.sprFileImport.ActiveSheet

        With frm

            For i As Integer = 0 To max

                File.Delete(chkInfiles(i))

            Next

        End With


    End Sub
    ''' <summary>
    ''' スプレッドにフォーカスをセットする
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetFoucusToSpread(ByRef frm As LMU020F)

        frm.ActiveControl = frm.sprFileImport

        frm.sprFileImport.Focus()

        With frm.sprFileImport.Sheets(0)
            If 1 < .RowCount Then
                .SetActiveCell(1, 0)
            Else
                .SetActiveCell(0, 1)
            End If
        End With
    End Sub

#End Region 'Method

#Region "スキャンファイル取込 処理"

    ''' <summary>
    ''' スキャンファイル取込 処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ReadScanData(ByVal frm As LMU020F)

        '権限チェック
        If Me._Validate.IsAuthorityChk(LMU020C.EVENT_IMPORT) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'フォルダパス取得
        Dim KbnDt As DataTable = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN)

        ' * 第１階層 *
        Dim fld1Dr As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD1_ROOT & "'")
        Dim fld1Nm As String = fld1Dr(0).Item("KBN_NM1").ToString()

        ' * 第２階層 *
        '   入力フォルダ
        Dim fld2DrIn As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_IN & "'")
        Dim fld2NmIn As String = fld2DrIn(0).Item("KBN_NM1").ToString()
        '   出力フォルダ
        Dim fld2DrOut As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_RESULT & "'")
        Dim fld2NmOut As String = fld2DrOut(0).Item("KBN_NM1").ToString()

        ' * 第３階層 *
        '   入力フォルダ
        Dim fld3NmBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()


        '***フォルダフルパス***
        ' 入力フォルダ
        Dim folderIn As String = String.Concat(fld1Nm, "\", _
                                               fld2NmIn, "\", _
                                               fld3NmBrCd)

        '   -- 取込実行結果出力フォルダ
        Dim folderOut As String = String.Concat(fld1Nm, "\", _
                                                fld2NmOut, "\", _
                                                fld3NmBrCd)


        '***フォルダフルパス***
        ' 入力フォルダ
        Dim chkInfiles As String() = System.IO.Directory.GetFiles(folderIn, "*", System.IO.SearchOption.TopDirectoryOnly)
        Dim ordNo As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim fileTypexx As String = String.Empty
        Dim fileType As String = String.Empty


        If chkInfiles.Length = 0 Then

            Me.ShowMessage(frm, "E078", New String() {"対象フォルダ"})
            Exit Sub

        Else

            For Each fileName As String In chkInfiles

                If Right(System.IO.Path.GetFileName(fileName), 3) = "pdf" Then

                    System.IO.File.Move(String.Concat(folderIn, "\", System.IO.Path.GetFileName(fileName)), String.Concat(folderOut, "\", System.IO.Path.GetFileName(fileName)))

                    ordNo = Left(System.IO.Path.GetFileName(fileName), 9)
                    fileTypexx = Right(System.IO.Path.GetFileName(fileName), 6)
                    fileType = Left(fileTypexx, 2)
                    fileNm = System.IO.Path.GetFileName(fileName)

                    Call _Gamen.SetSpread(ordNo, fileNm, fileType)

                End If

            Next

        End If

    End Sub

#End Region 'スキャンファイル取込 処理

#Region "取込結果フォルダOpen 処理"
    ''' <summary>
    ''' 取込結果フォルダOpen 処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenFolder(ByVal frm As LMU020F)

        '権限チェック
        If Me._Validate.IsAuthorityChk(LMU020C.EVENT_FOLDEROPEN) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        Me.StartAction(frm)

        'フォルダパス取得
        Dim KbnDt As DataTable = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN)

        ' * 第１階層 *
        Dim fld1Dr As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD1_ROOT & "'")
        Dim fld1Nm As String = fld1Dr(0).Item("KBN_NM1").ToString()

        ' * 第２階層 *
        '   入力フォルダ
        Dim fld2DrIn As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_IN & "'")
        Dim fld2NmIn As String = fld2DrIn(0).Item("KBN_NM1").ToString()
        '   出力フォルダ
        Dim fld2DrOut As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_RESULT & "'")
        Dim fld2NmOut As String = fld2DrOut(0).Item("KBN_NM1").ToString()

        ' * 第３階層 *
        '   入力フォルダ
        Dim fld3NmBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()

        '***フォルダフルパス***
        ' 入力フォルダ
        Dim folderIn As String = String.Concat(fld1Nm, "\", _
                                               fld2NmIn, "\", _
                                               fld3NmBrCd)

        '   -- 取込実行結果出力フォルダ
        Dim folderOut As String = String.Concat(fld1Nm, "\", _
                                                fld2NmOut, "\", _
                                                fld3NmBrCd)


        '***フォルダフルパス***
        ' 入力フォルダ
        Dim chkInfiles As String() = System.IO.Directory.GetFiles(folderOut, "*", System.IO.SearchOption.TopDirectoryOnly)
        Dim ordNo As String = String.Empty
        If chkInfiles.Length = 0 Then

            Me.ShowMessage(frm, "E078", New String() {"対象フォルダ"})
            Me.EndAction(frm)
            Exit Sub

        Else

            '主処理
            System.Diagnostics.Process.Start( _
            "EXPLORER.EXE", String.Concat("/e,", folderOut))

        End If

        Me.EndAction(frm)

    End Sub



#End Region '取込結果フォルダOpen 処理

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SaveDataListData(ByRef frm As LMU020F) As Boolean

        '権限チェック
        If Me._Validate.IsAuthorityChk(LMU020C.EVENT_SAVE) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Function
        End If

        If frm.sprFileImport.ActiveSheet.RowCount = 0 Then

            Me.ShowMessage(frm, "E024")
            Exit Function

        End If


        '初期処理
        Me.StartAction(frm)

        '権限チェック

        'Spread部入力チェック

        '画面全ロック解除
        Me.UnLockedControls(frm)

        If Me.ShowMessageC001(frm, "Save") = True Then

            Dim ds As DataSet = New LMU020DS


            Me.SetDatasetImportData(ds, frm)

            '==========================
            'WSAクラス呼出
            '==========================
            ds = Me.CallWSA("LMU020BLF", "SaveDataList", ds)

            '再検索
            'Call Me.SelectDataAgain(frm, ds, False, True)

            'フォーカスの設定
            'Call Me._Gamen.SetFoucus()

            'カーソルを元に戻す
            Cursor.Current = Cursors.Default

            'スプレッドクリア
            frm.sprFileImport.CrearSpread()

            Me.ShowMessage(frm, "G015", New String() {""})

            Return True

        End If

    End Function
#Region "閉じる 処理"
    ''' <summary>
    ''' 閉じる 処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub Close(ByVal frm As LMU020F)

        Me.StartAction(frm)

        'フォルダパス取得
        Dim KbnDt As DataTable = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN)

        ' * 第１階層 *
        Dim fld1Dr As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD1_ROOT & "'")
        Dim fld1Nm As String = fld1Dr(0).Item("KBN_NM1").ToString()

        ' * 第２階層 *
        '   入力フォルダ
        Dim fld2DrIn As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_IN & "'")
        Dim fld2NmIn As String = fld2DrIn(0).Item("KBN_NM1").ToString()
        '   出力フォルダ
        Dim fld2DrOut As DataRow() = KbnDt.Select("KBN_GROUP_CD = 'C028' AND KBN_CD = '" & LMU020C.C028_FLD2_IMP_RESULT & "'")
        Dim fld2NmOut As String = fld2DrOut(0).Item("KBN_NM1").ToString()

        ' * 第３階層 *
        '   入力フォルダ
        Dim fld3NmBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()


        '***フォルダフルパス***
        ' 入力フォルダ
        Dim folderIn As String = String.Concat(fld1Nm, "\", _
                                               fld2NmIn, "\", _
                                               fld3NmBrCd)

        '   -- 取込実行結果出力フォルダ
        Dim folderOut As String = String.Concat(fld1Nm, "\", _
                                                fld2NmOut, "\", _
                                                fld3NmBrCd)


        '***フォルダフルパス***
        ' 入力フォルダ
        Dim chkInfiles As String() = System.IO.Directory.GetFiles(folderOut, "*", System.IO.SearchOption.TopDirectoryOnly)
        Dim ordNo As String = String.Empty
        If chkInfiles.Length > 0 Then

            Me.ShowMessage(frm, "E751")
            Me.EndAction(frm)
            Exit Sub

        Else

            '主処理
            frm.Close()

        End If

    End Sub



#End Region '閉じる 処理

    ''' <summary>
    ''' 開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMU020F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        'メッセージエリアのクリア
        MyBase.ClearMessageAria(frm)

    End Sub

    ''' <summary>
    ''' 終了処理処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMU020F)

        '画面解除
        Me.UnLockedControls(frm)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub
    ''' <summary>
    ''' 処理確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="replaceMsg">処理メッセージ</param>
    ''' <returns>True：処理続行、False：処理中断</returns>
    ''' <remarks></remarks>
    Friend Function ShowMessageC001(ByVal frm As LMU020F, ByVal replaceMsg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {replaceMsg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 格納ディレクトリ取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function GetDirectory(ByRef frm As LMU020F) As String
        Dim sheet As FarPoint.Win.Spread.SheetView = frm.sprFileImport.ActiveSheet
        Dim KBDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)   'NFS用フォルダ
        Dim drKbn As DataRow() = KBDt.Select("KBN_GROUP_CD = 'C027' " & _
                                              "AND KBN_CD   = '01'   ")

        '格納場所定義
        Dim strRootDR As String = drKbn(0).Item("KBN_NM1").ToString
        Dim strKeyNoDR As String = Me._Gamen.GetCellValue(sheet.Cells(0, LMU020G.SprImportFileDef.ORD_NO.ColNo))
        Dim strDR As String = strRootDR

        Return strDR

    End Function


End Class
