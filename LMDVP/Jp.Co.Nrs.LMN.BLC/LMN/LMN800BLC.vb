' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN800BLC : 出荷報告データ送信
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMN800BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN800BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"
    'ステータス
    Private Const RESULT_SUCCESS As String = "00"
    Private Const RESULT_PARA_ERROR As String = "10"
    Private Const RESULT_SYSTEM_ERROR As String = "99"

    'テーブル名
    'ＢＰ用テーブル名
    Private Const BP_SEND As String = "N_SEND_BP"

    '区切り文字
    Private Const SEPARATE_CHARACTER_COMMA As String = ","
    Private Const SEPARATE_CHARACTER_TAB As String = vbTab

    '出力形態
    Private Enum OUTPUT_TYPE
        FixedLengthRecord = 0
        CommaSeparateValues
        TabSeparateValues
    End Enum


#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN800DAC = New LMN800DAC()

#End Region

#Region "出荷報告データ送信"

    ''' <summary>
    ''' 出荷報告データ送信
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExecuteSoushinShiji(ByVal ds As DataSet) As DataSet

        '引数のチェック
        ds = Me.IsSuccessParam(ds)
        If Me.IsMessageExist = True Then Return ds

        '荷主の振り分け
        Dim custCdKbn As String = ds.Tables("LMN800CUST_LIST").Rows(0).Item("KBN_CD").ToString()

        '出力先の設定
        ds = Me.GetOutputFilePath(ds)
        If Me.IsMessageExist = True Then Return ds

        'テキスト出力
        ds = Me.CreateShukkaShijiFile(ds)
        If Me.IsMessageExist = True Then Return ds

        '正常終了
        Me.SetResultDataTable(ds, RESULT_SUCCESS, String.Empty, String.Empty)

        Return ds

    End Function

#End Region

#Region "入力チェック"

    Private Function IsSuccessParam(ByVal ds As DataSet) As DataSet

        'CUST_CDのエラーチェック
        If ds.Tables("LMN800CUST_INFO") Is Nothing OrElse ds.Tables("LMN800CUST_INFO").Rows.Count = 0 Then
            Dim errorCd As String = "S001"
            Me.SetResultDataTable(ds, RESULT_PARA_ERROR, errorCd, "荷主コード未設定")
            'SCM荷主コード設定エラー
            Me.SetMessage(errorCd)
            Return ds
        End If

        '荷主による設定
        ds = MyBase.CallDAC(Me._Dac, "SelectSCMCustCd", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count = 0 Then
            '0件の場合
            Dim errorCd As String = "S001"
            Me.SetResultDataTable(ds, RESULT_PARA_ERROR, errorCd, "荷主コード連携対象外")
            'SCM荷主コード設定エラー
            Me.SetMessage(errorCd)
            Return ds
        End If

        Dim cust_kb As String = ds.Tables("LMN800CUST_LIST").Rows(0).Item("KBN_CD").ToString

        Select Case cust_kb
            Case "00" 'BPカストロール
                If ds.Tables(BP_SEND) Is Nothing OrElse ds.Tables(BP_SEND).Rows.Count = 0 Then
                    Dim errorCd As String = "S001"
                    Me.SetResultDataTable(ds, RESULT_PARA_ERROR, errorCd, "ヘッダデータなし")
                    Me.SetMessage(errorCd)
                    Return ds

                End If

        End Select

        Return ds

    End Function

    Private Function SetResultDataTable(ByVal ds As DataSet _
                                        , ByVal result As String, ByVal errorCd As String, ByVal remark As String) As DataSet

        Dim row As DataRow = ds.Tables("RESULT").NewRow
        row.Item("RESULT") = result
        row.Item("ERROR_CD") = errorCd
        row.Item("REMARK") = remark

        ds.Tables("RESULT").Rows.Add(row)

        Return ds

    End Function

#End Region

#Region "出力パス取得"

    Private Function GetOutputFilePath(ByVal ds As DataSet) As DataSet

        '荷主による設定
        ds = MyBase.CallDAC(Me._Dac, "SelectFilePath", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count = 0 Then
            '0件の場合
            Dim errorCd As String = "S002"
            Me.SetResultDataTable(ds, RESULT_SYSTEM_ERROR, errorCd, "出力パス未設定")
            '出力パス未設定エラー
            Me.SetMessage(errorCd)
            Return ds
        End If

        Dim folderPath As String = ds.Tables("LMN800FILEPATH_OUT").Rows(0).Item("FILE_PATH").ToString

        'フォルダの存在チェック
        If System.IO.Directory.Exists(folderPath) = False Then
            '0件の場合
            Dim errorCd As String = "S002"
            Me.SetResultDataTable(ds, RESULT_SYSTEM_ERROR, errorCd, "フォルダ存在エラー")
            'フォルダ存在エラー
            Me.SetMessage(errorCd)
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "出荷報告ファイル作成"

    Private Function CreateShukkaShijiFile(ByVal ds As DataSet) As DataSet

        '荷主による設定
        Dim cust_kb As String = ds.Tables("LMN800CUST_LIST").Rows(0).Item("KBN_CD").ToString
        Dim folderFilePath As String = ds.Tables("LMN800FILEPATH_OUT").Rows(0).Item("FILE_PATH").ToString
        Dim filePath As String = String.Empty
        Dim fileExtension As String = ds.Tables("LMN800FILEPATH_OUT").Rows(0).Item("FILE_EXTENSION").ToString
        Dim createtime As String = Format(Now, "yyyyMMddHHmmss")

        Select Case cust_kb
            Case "00" 'BPカストロール
                'ヘッダファイルの出力
                filePath = String.Concat(folderFilePath, "\", ds.Tables("LMN800FILEPATH_OUT").Rows(0).Item("FILE_NAME").ToString)
                Me.CreateFile(OUTPUT_TYPE.FixedLengthRecord, ds, BP_SEND, String.Concat(filePath, createtime, ".", fileExtension))

        End Select

        Return ds

    End Function


    Private Sub CreateFile(ByVal type As OUTPUT_TYPE _
                           , ByVal ds As DataSet, ByVal tblName As String _
                           , ByVal filePath As String)

        '書き込む文字列
        Dim outPutData As String = String.Empty
        Dim outPutRowData As String = String.Empty
        Dim row As DataRow = Nothing
        Dim col As DataColumn = Nothing
        Dim sepaChar As String = String.Empty  '区切り文字列
        Dim lfChar As String = String.Empty  '改行文字列


        For Each row In ds.Tables(tblName).Rows
            '送信データの書き出し
            sepaChar = String.Empty
            outPutRowData = String.Empty
            For i As Integer = 0 To ds.Tables(tblName).Columns.Count - 1

                If i = 1 Then
                    sepaChar = Me.GetSeparateCharacter(type)
                End If
                outPutRowData = String.Concat(outPutRowData, sepaChar, Me.editData(tblName, type, ds.Tables(tblName).Columns(i), row(i).ToString()))
            Next
            outPutData = String.Concat(outPutData, lfChar, outPutRowData)
            lfChar = vbCrLf

        Next

        'Shift JISで書き込む
        '書き込むファイルが既に存在している場合は、上書きする
        Dim sw As New System.IO.StreamWriter(filePath, _
            False, _
            System.Text.Encoding.GetEncoding("shift_jis"))
        'TextBox1.Textの内容を書き込む
        sw.Write(outPutData)
        '閉じる
        sw.Close()


    End Sub

    Private Function editData(ByVal tableName As String, ByVal type As OUTPUT_TYPE, ByVal col As DataColumn, ByVal data As String) As String

        Dim editString As String = String.Empty

        '固定長以外はそのまま返す
        Select Case type
            Case OUTPUT_TYPE.FixedLengthRecord
                editString = Me.EditColumn(tableName, col, data)

            Case Else
                editString = data

        End Select

        Return editString

    End Function

    Private Function EditColumn(ByVal tableName As String, ByVal col As DataColumn, ByVal data As String) As String
        Dim rtnString As String = String.Empty

        Select Case tableName
            Case BP_SEND
                Select Case col.Caption
                    Case "KITAKU_CD"
                        rtnString = Me.PadRightB(data, 3, " ")
                    Case "SOKO_CD"
                        rtnString = Me.PadRightB(data, 3, " ")
                    Case "ORDER_TYPE"
                        rtnString = Me.PadRightB(data, 2, " ")
                    Case "INOUT_DATE"
                        rtnString = Me.PadRightB(data, 8, " ")
                    Case "FILLER_1"
                        rtnString = Me.PadRightB(data, 1, " ")
                    Case "CUST_ORD_NO"
                        rtnString = Me.PadRightB(data, 8, " ")
                    Case "DEST_CD"
                        rtnString = Me.PadRightB(data, 8, " ")
                    Case "MEI_NO"
                        rtnString = Me.PadRightB(data, 3, " ")
                    Case "GOODS_CD"
                        rtnString = Me.PadRightB(data, 15, " ")
                    Case "SEIZO_DATE"
                        rtnString = Me.PadRightB(data, 8, "0")
                    Case "LOT_NO"
                        rtnString = Me.PadRightB(data, 15, " ")
                    Case "SOKO_NO"
                        rtnString = Me.PadRightB(data, 2, " ")
                    Case "LOCA"
                        rtnString = Me.PadRightB(data, 8, " ")
                    Case "INOUT_PKG_NB"
                        rtnString = Me.PadLeftB(data, 7, "0")
                    Case "BUMON_CD"
                        rtnString = Me.PadRightB(data, 2, " ")
                    Case "JIGYOBU_CD"
                        rtnString = Me.PadRightB(data, 4, " ")
                    Case "TOKUI_CD"
                        rtnString = Me.PadLeftB(data, 8, "0")
                    Case "OKURIJO_NO"
                        rtnString = Me.PadRightB(data, 13, " ")
                    Case "FILLER_2"
                        rtnString = Me.PadRightB(data, 138, " ")

                    Case Else
                End Select
        End Select

        Return rtnString

    End Function



    Private Function GetSeparateCharacter(ByVal type As OUTPUT_TYPE) As String

        Select Case type
            Case OUTPUT_TYPE.CommaSeparateValues
                Return SEPARATE_CHARACTER_COMMA
            Case OUTPUT_TYPE.TabSeparateValues
                Return SEPARATE_CHARACTER_TAB
        End Select

        Return String.Empty

    End Function

    Private Function PadLeftB(ByVal str As String, ByVal length As Integer, ByVal padChar As String) As String
        Dim rtn As String = String.Empty
        If Me.GetPadByte(str, length) = 0 Then
            Return str
        End If

        '埋める文字数の取得
        For i As Integer = 1 To Me.GetPadByte(str, length)
            rtn = String.Concat(rtn, padChar)
        Next

        Return String.Concat(rtn, str)

    End Function


    Private Function PadRightB(ByVal str As String, ByVal length As Integer, ByVal padChar As String) As String
        Dim rtn As String = str
        If Me.GetPadByte(str, length) = 0 Then
            Return str
        End If
        '埋める文字数の取得
        For i As Integer = 1 To Me.GetPadByte(str, length)
            rtn = String.Concat(rtn, padChar)
        Next

        Return rtn

    End Function

    Private Function GetPadByte(ByVal str As String, ByVal length As Integer) As Integer

        Dim dataByte As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        Return length - dataByte

    End Function


#End Region


End Class
