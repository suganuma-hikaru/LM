' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH830BLF : LMH830BLC : 未着・早着ファイル作成(UTI)
'  作  成  者       :  umano
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH830BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH830BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Private Const TABLE_NM_IN As String = "LMH830IN"
    Private Const FILE_PATH As String = "LMH830FILE_PATH"

    Private Const MICYAKU_OUT As String = "LMH830_MICYAKU_OUT"
    Private Const SOUCYAKU_OUT As String = "LMH830_SOUCYAKU_OUT"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE_DELI As String = "Delivery №:"
    Public Const EXCEL_COLTITLE_INKAEDI As String = "受信ファイル名"

#End Region

#Region "未着・早着ファイル作成(UTI)"

    ''' <summary>
    ''' 出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function OutputMisoutyakuFile(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setBlc As New LMH830BLC

        setBlc = New LMH830BLC

        '①入荷・受信合致(不整合なし)データの取得
        rtnDs = MyBase.CallBLC(setBlc, "GetAgreeDataSql", ds)

        '②未着対象データの取得
        rtnDs = MyBase.CallBLC(setBlc, "GetMicyakuDataFile", ds)
        If MyBase.GetResultCount = 0 Then

        End If


        '↓繰り返す
        Dim normalCnt As Integer = 0
        Dim errorCnt As Integer = 0

        '初期化
        'dt.Clear()
        ds.Tables("LMH830Result").Clear()
        ds.Tables("LMH830Result").Rows.Add(ds.Tables("LMH830Result").NewRow)
        ds.Tables("LMH830Result").Rows(0)("IsErrorResult") = "0"

        ''未着データのUTI入荷確認EDIデータ更新処理
        'Dim max As Integer = rtnDs.Tables(LMH830BLF.MICYAKU_OUT).Rows.Count - 1

        'For i As Integer = 0 To max

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '入荷データ更新処理(進捗区分を"入荷済=50"にする)
            rtnDs = MyBase.CallBLC(setBlc, "UpdateBInkaL", rtnDs)

            '在庫データ更新処理(入荷日をセットする)
            rtnDs = MyBase.CallBLC(setBlc, "UpdateZaiTrs", rtnDs)

            ''UTI入荷確認データ(HED)登録処理
            'rtnDs = MyBase.CallBLC(setBlc, "UpdateHInkaEdiHedUti", rtnDs)

            ' ''UTI入荷確認データ(DTL)登録処理
            'rtnDs = MyBase.CallBLC(setBlc, "UpdateHInkaEdiDtlUti", rtnDs)

            'コミット
            If rtnDs.Tables("LMH830Result").Rows(0)("IsErrorResult").ToString = "0" Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)

                ''ファイルをバックアップへコピー
                'Call Me.CopyAndDelete(fileName, backupFolderPath, rtnDs)
                normalCnt = normalCnt + 1
            Else
                'エラー行が発生した時点でコミットは行わない
                '但し、エラーチェックは行う
                errorCnt = errorCnt + 1
                'Continue For
                'Return rtnDs

            End If

        End Using

        'Next

        rtnDs.Tables("LMH830Result").Rows(0).Item("NORMALCNT") = Convert.ToString(normalCnt)
        rtnDs.Tables("LMH830Result").Rows(0).Item("ERRORCNT") = Convert.ToString(errorCnt)


        '③早着対象データの取得
        rtnDs = MyBase.CallBLC(setBlc, "GetSoucyakuDataFile", ds)
        If MyBase.GetResultCount = 0 Then

        End If

        ''ワークフォルダの取得
        ''区分マスタから取得
        'Dim micyakuFolPath As String = String.Empty
        'Dim soucyakuFolPath As String = String.Empty
        'Dim backupFolderPath As String = String.Empty
        ''Dim rcvExtention As String = String.Empty
        'rtnDs = MyBase.CallBLC(setBlc, "GetSendFolderPass", ds)
        'Dim dt As DataTable = rtnDs.Tables(LMH830BLF.FILE_PATH)
        'Dim dr As DataRow = dt.Rows(0)
        'If MyBase.GetResultCount = 0 Then
        '    Me.SetMessage("E223", New String() {"送信ファイルパス"})
        '    Return ds

        'ElseIf String.IsNullOrEmpty(dr.Item("SEND_MI_OUTPUT_DIR").ToString().Trim()) = True Then
        '    Me.SetMessage("E237", New String() {"未着送信フォルダパスが空"})
        '    Return ds
        'ElseIf String.IsNullOrEmpty(dr.Item("SEND_SOU_OUTPUT_DIR").ToString().Trim()) = True Then
        '    Me.SetMessage("E237", New String() {"早着送信フォルダパスが空"})
        '    Return ds
        'ElseIf String.IsNullOrEmpty(dr.Item("BACKUP_OUTPUT_DIR").ToString().Trim()) = True Then
        '    Me.SetMessage("E237", New String() {"バックアップフォルダパスが空"})

        'Else
        '    micyakuFolPath = dr.Item("SEND_MI_OUTPUT_DIR").ToString()
        '    soucyakuFolPath = dr.Item("SEND_SOU_OUTPUT_DIR").ToString()
        '    backupFolderPath = dr.Item("BACKUP_OUTPUT_DIR").ToString()
        '    'rcvExtention = dr.Item("RCV_FILE_EXTENTION").ToString()
        'End If

        Return rtnDs

    End Function

    ''' <summary>
    '''  処理正常時ファイルコピー
    ''' </summary>
    ''' <param name="tgtFile"></param>
    ''' <param name="CopyTOFolder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyAndDelete(ByVal tgtFile As String, ByVal CopyTOFolder As String, ByVal rtnDs As DataSet) As Boolean

        Try
            '上書きOKとしてコピー可能
            'System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", rtnDs.Tables(LMH830BLF.H_INKAEDI_HED_UTI).Rows(0).Item("FILE_NAME").ToString()), True)
            System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function

#End Region

End Class
