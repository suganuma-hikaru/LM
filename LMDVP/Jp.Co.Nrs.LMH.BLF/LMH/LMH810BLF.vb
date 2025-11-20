' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH810BLF : 分析票取込
'  作  成  者       :  小林信
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH810BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH810BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Private Const TABLE_NM_IN As String = "LMH810IN"
    Private Const TABLE_NM_COAIN As String = "LMH810COAIN"

    '(2012.10.24)追加START 要望番号1531
    Private Const EDIT_KETA As String = "01"
    Private Const EDIT_KUGIRI As String = "02"
    '(2012.10.24)追加END 要望番号1531

#End Region

#Region "分析票取込"

    ''' <summary>
    ''' 出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function CoaTouroku(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMH810BLF.TABLE_NM_COAIN)
        Dim dr As DataRow = ds.Tables(LMH810BLF.TABLE_NM_IN).Rows(0)
        Dim rtnDs As DataSet = Nothing
        Dim coaFolder As String = String.Empty
        Dim setBlc As New LMH810BLC

        setBlc = New LMH810BLC

        'ワークフォルダの取得
        'TODO:EDI荷主マスタから取得
        'TODO:　(2012.09.27)修正START
        Dim workFolderPath As String = String.Empty
        Dim backupFolderPath As String = String.Empty
        Dim rcvExtention As String = String.Empty
        'TODO:　(2012.09.27)修正END
        rtnDs = MyBase.CallBLC(setBlc, "GetCUSTCOA_FOLDER", ds)
        If ds.Tables("LMH810COA_FOLDER").Rows.Count = 0 Then
            Me.SetMessage("E223", New String() {"分析票読み込み元フォルダ"})
            Return ds

            'TODO:　(2012.09.27)追加START
        ElseIf String.IsNullOrEmpty(ds.Tables("LMH810COA_FOLDER").Rows(0).Item("COA_FOLDER").ToString().Trim()) = True Then
            Me.SetMessage("E237", New String() {"分析票読み込み元フォルダパスが空"})
            Return ds
            'TODO:　(2012.09.27)追加END
        Else
            workFolderPath = ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_FOLDER").ToString()
            backupFolderPath = ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_BACKUP").ToString()
            rcvExtention = ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_EXTENTION").ToString()
        End If
        ds.Tables("LMH810COA_FOLDER").Clear()
        'Dim files As String() = System.IO.Directory.GetFiles(workFolderPath, "*.PDF", System.IO.SearchOption.AllDirectories)
        Dim files As String() = System.IO.Directory.GetFiles(workFolderPath, _
                                                             String.Concat("*", rcvExtention), _
                                                             System.IO.SearchOption.TopDirectoryOnly)

        If files.Length = 0 Then
            'メッセージセット
            Me.SetMessage("E460")
            Return rtnDs
        End If

        'M_COACONFIGの取得
        rtnDs = MyBase.CallBLC(setBlc, "GetM_COACONFIG", ds)

        ''入力チェック(現状はチェックなしの為コメント)
        'rtnDs = MyBase.CallBLC(setBlc, "CoaCheck", ds)

        'Z_KBNより登録先の分析票フォルダを取得
        rtnDs = MyBase.CallBLC(setBlc, "GetBRCOA_FOLDER", ds)
        If ds.Tables("LMH810COA_FOLDER").Rows.Count = 0 Then
            Me.SetMessage("E223", New String() {"分析票を保存するフォルダ"})
            Return ds
            'TODO:　(2012.09.27)追加START
        ElseIf String.IsNullOrEmpty(ds.Tables("LMH810COA_FOLDER").Rows(0).Item("COA_FOLDER").ToString().Trim()) = True Then
            Me.SetMessage("E237", New String() {"分析票を保存するフォルダパスが空"})
            Return ds
            'TODO:　(2012.09.27)追加END
        Else
            coaFolder = ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_FOLDER").ToString()
        End If

        '↓繰り返す
        Dim coaRow As DataRow
        '(2012.10.03) 要望番号1439 追加START
        Dim normalCnt As Integer = 0
        Dim errorCnt As Integer = 0
        '(2012.10.03) 要望番号1439 追加END
        For Each fileName As String In files

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added
            If (Me.IsFileSizeZero(fileName)) Then
                ' ファイルサイズが0byteの場合は処理をスキップする
                Continue For
            End If
#End If

            '(2012.10.24)修正START 要望番号1531
            '(M080)分析票取込マスタのファイル名バイト数チェック
            If (rtnDs.Tables("LMH810COACONFIG").Rows(0).Item("EDIT_MODE").ToString()).Equals(LMH810BLF.EDIT_KETA) = True Then

                If Me.ServerValidateChk(rtnDs, fileName) = False Then

                    MyBase.SetMessageStore("00" _
                                         , "E521" _
                                         , New String() {String.Empty} _
                                         , String.Empty _
                                         , "エラー項目" _
                                         , String.Concat("ファイル名：", fileName))
                    '(2012.10.03) 要望番号1439 追加START
                    errorCnt = errorCnt + 1
                    '(2012.10.03) 要望番号1439 追加END
                    Continue For
                End If

            End If
            '(2012.10.24)修正END 要望番号1531

            '初期化
            dt.Clear()
            coaRow = dt.NewRow
            coaRow = Me.SetCoaRow(coaRow, dr, fileName, coaFolder, rtnDs)
            dt.Rows.Add(coaRow)

            ds.Tables("LMH810GOODS").Clear()
            ds.Tables("LMH810Result").Clear()
            ds.Tables("LMH810Result").Rows.Add(ds.Tables("LMH810Result").NewRow)
            ds.Tables("LMH810Result").Rows(0)("IsErrorResult") = "0"

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'M_COA登録処理
                rtnDs = MyBase.CallBLC(setBlc, "CoaTouroku", ds)
                'コミット
                If rtnDs.Tables("LMH810Result").Rows(0)("IsErrorResult").ToString = "0" Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)

                    'ファイルをバックアップへコピー
                    'Call Me.CopyAndDelete(fileName, coaFolder)
                    Call Me.CopyAndDelete(fileName, backupFolderPath)
                    '(2012.10.03) 要望番号1439 追加START
                    normalCnt = normalCnt + 1
                    '(2012.10.03) 要望番号1439 追加END
                Else
                    'エラー行が発生した時点でコミットは行わない
                    '但し、エラーチェックは行う
                    '(2012.10.03) 要望番号1439 追加START
                    errorCnt = errorCnt + 1
                    '(2012.10.03) 要望番号1439 追加END
                    Continue For
                    'Return rtnDs

                End If

            End Using

        Next

        If (ds.Tables("LMH810Result").Rows.Count = 0) Then
            ds.Tables("LMH810Result").Rows.Add(ds.Tables("LMH810Result").NewRow)
        End If

        '(2012.10.03) 要望番号1439 追加START
        rtnDs.Tables("LMH810Result").Rows(0).Item("NORMALCNT") = Convert.ToString(normalCnt)
        rtnDs.Tables("LMH810Result").Rows(0).Item("ERRORCNT") = Convert.ToString(errorCnt)
        '(2012.10.03) 要望番号1439 追加END

        Return rtnDs

    End Function

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added

    ''' <summary>
    ''' ファイルサイズチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsFileSizeZero(ByVal filePath As String) As Boolean

        If (String.IsNullOrEmpty(filePath) = False AndAlso _
            System.IO.File.Exists(filePath)) Then

            Dim info As New System.IO.FileInfo(filePath)
            info.Refresh()
            If (info.Length > 0) Then
                Return False
            End If

        End If

        Return True

    End Function
#End If


    ''' <summary>
    '''  (M080)分析票取込マスタのファイル名バイト数チェック
    ''' </summary>
    ''' <param name="coaConfigDs"></param>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerValidateChk(ByVal coaConfigDs As DataSet, ByVal filePath As String) As Boolean

        Dim intDEST_CD_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_START_POS").ToString())
        Dim intDEST_CD_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_LEN").ToString())
        Dim intGOODS_CD_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_START_POS").ToString())
        Dim intGOODS_CD_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_LEN").ToString())
        Dim intLOT_NO_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_START_POS").ToString())
        Dim intLOT_NO_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_LEN").ToString())

        Dim fileName As String = IO.Path.GetFileName(filePath)
        Dim nameByte As Integer = fileName.Length
        Dim torikomiByte As Integer = 0
        Dim intUNDER_BAR As Integer = 1             'ファイル名のアンダーバー

        torikomiByte = intDEST_CD_START_POS + intDEST_CD_LEN + intUNDER_BAR + intGOODS_CD_LEN + intUNDER_BAR + intLOT_NO_LEN

        If nameByte < torikomiByte Then

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''  分析票レコードセット
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCoaRow(ByVal dr As DataRow, ByVal inDr As DataRow _
                               , ByVal filePath As String, ByVal coaFolder As String, ByVal coaConfigDs As DataSet) As DataRow

        '(2012.10.24)追加START 要望番号1531
        Dim strEDIT_MODE As String = coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("EDIT_MODE").ToString()
        '(2012.10.24)追加START 要望番号1531
        Dim intDEST_CD_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_START_POS").ToString())
        Dim intDEST_CD_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_LEN").ToString())
        Dim intGOODS_CD_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_START_POS").ToString())
        Dim intGOODS_CD_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_LEN").ToString())
        Dim intLOT_NO_START_POS As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_START_POS").ToString())
        Dim intLOT_NO_LEN As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_LEN").ToString())
        '(2012.10.24)追加START 要望番号1531
        Dim strSEPARATE_CHAR As String = coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("SEPARATE_CHAR").ToString()
        Dim intDEST_CD_COL_NO As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_COL_NO").ToString())
        Dim intGOODS_CD_COL_NO As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_COL_NO").ToString())
        Dim intLOT_NO_COL_NO As Integer = Integer.Parse(coaConfigDs.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_COL_NO").ToString())
        '(2012.10.24)追加START 要望番号1531

        Dim fileName As String = IO.Path.GetFileName(filePath)
        Dim splitStr As String()
        dr("NRS_BR_CD") = inDr("NRS_BR_CD")
        dr("CUST_CD_L") = inDr("CUST_CD_L")
        dr("CUST_CD_M") = inDr("CUST_CD_M")
        dr("GOODS_CD_NRS") = "" '後ほど商品マスタから取得(BLCにて設定)
        dr("COA_LINK") = coaFolder
        dr("COA_NAME") = IO.Path.GetFileName(filePath)
        dr("INPUT_COA_FILE") = filePath
        '(2012.10.24)修正START 要望番号1531
        If strEDIT_MODE.Equals(LMH810BLF.EDIT_KETA) = True Then

            dr("GOODS_CD_CUST") = fileName.Substring(intGOODS_CD_START_POS, intGOODS_CD_LEN)
            dr("LOT_NO") = fileName.Substring(intLOT_NO_START_POS, intLOT_NO_LEN)
            dr("DEST_CD") = fileName.Substring(intDEST_CD_START_POS, intDEST_CD_LEN)

        ElseIf strEDIT_MODE.Equals(LMH810BLF.EDIT_KUGIRI) = True Then

            splitStr = Split(fileName, strSEPARATE_CHAR)
            dr("GOODS_CD_CUST") = splitStr(intGOODS_CD_COL_NO)
            dr("LOT_NO") = splitStr(intLOT_NO_COL_NO)
            dr("DEST_CD") = splitStr(intDEST_CD_COL_NO)

        End If
        '(2012.10.24)修正END 要望番号1531

        Return dr

    End Function


    ''' <summary>
    '''  処理正常時ファイルコピー
    ''' </summary>
    ''' <param name="tgtFile"></param>
    ''' <param name="CopyTOFolder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyAndDelete(ByVal tgtFile As String, ByVal CopyTOFolder As String) As Boolean

        Try
            '上書きOKとしてコピー可能
            System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", IO.Path.GetFileName(tgtFile)), True)
            System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function

#End Region

End Class
