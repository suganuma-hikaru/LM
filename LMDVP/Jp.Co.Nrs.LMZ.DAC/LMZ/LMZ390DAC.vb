' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ390DAC : Rapidus次回分納情報取得
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ390DACクラス
''' </summary>
Public Class LMZ390DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "特定の荷主固有のテーブルが存在するか否かの判定SQL"

    ' 特定の荷主固有のテーブルが存在するか否かの判定SQL
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

#End Region ' "特定の荷主固有のテーブルが存在するか否かの判定SQL"

#Region "次回分納情報有無確認対象の出荷指示EDIデータの取得SQL"

    ''' <summary>
    ''' 次回分納情報有無確認対象の出荷指示EDIデータの取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA_EDI_DTL_RAPI_1 As String = "" _
            & "SELECT DISTINCT                                                           " & vbNewLine _
            & "      H_OUTKAEDI_DTL_RAPI.NRS_BR_CD                                       " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CRT_DATE                                        " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.FILE_NAME                                       " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.DENP_NO                                         " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.RECORD_STATUS                                   " & vbNewLine _
            & "    , @TEMPLATE_PREFIX AS TEMPLATE_PREFIX                                 " & vbNewLine _
            & "FROM                                                                      " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_RAPI                                         " & vbNewLine _
            & "WHERE                                                                     " & vbNewLine _
            & "    H_OUTKAEDI_DTL_RAPI.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.OUTKA_CTL_NO = @OUTKA_CTL_NO                      " & vbNewLine _
            & ""
    Private Const SQL_SELECT_OUTKA_EDI_DTL_RAPI_2 As String = "" _
            & "AND H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO = @EDI_CTL_NO                          " & vbNewLine _
            & ""
    Private Const SQL_SELECT_OUTKA_EDI_DTL_RAPI_3 As String = "" _
            & "AND H_OUTKAEDI_DTL_RAPI.SYS_DEL_FLG = '0'                                 " & vbNewLine _
            & "ORDER BY                                                                  " & vbNewLine _
            & "      H_OUTKAEDI_DTL_RAPI.CRT_DATE                                        " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.FILE_NAME                                       " & vbNewLine _
            & ""

#End Region ' "次回分納情報有無確認対象の出荷指示EDIデータの取得SQL"

#Region "次回分納の出荷指示EDIデータの取得SQL"

    ''' <summary>
    ''' 次回分納の出荷指示EDIデータの取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA_EDI_DTL_RAPI_JIKAI_BUNNOU As String = "" _
            & "SELECT TOP 1                                                        " & vbNewLine _
            & "      H_OUTKAEDI_DTL_RAPI.NRS_BR_CD                                 " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.CRT_DATE                                  " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.FILE_NAME                                 " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.EDI_CTL_NO                                " & vbNewLine _
            & "    , H_OUTKAEDI_DTL_RAPI.OUTKA_CTL_NO                              " & vbNewLine _
            & "FROM                                                                " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_RAPI                                   " & vbNewLine _
            & "WHERE                                                               " & vbNewLine _
            & "    H_OUTKAEDI_DTL_RAPI.CRT_DATE = @CRT_DATE                        " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.FILE_NAME LIKE @FILE_NAME                   " & vbNewLine _
            & "AND CASE WHEN ISNUMERIC(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS) = 1      " & vbNewLine _
            & "        THEN CAST(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS AS NUMERIC(20)) " & vbNewLine _
            & "        ELSE 1                                                      " & vbNewLine _
            & "    END > @RECORD_STATUS                                            " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.DENP_NO = @DENP_NO                          " & vbNewLine _
            & "AND H_OUTKAEDI_DTL_RAPI.SYS_DEL_FLG = '0'                           " & vbNewLine _
            & "ORDER BY                                                            " & vbNewLine _
            & "    CASE WHEN ISNUMERIC(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS) = 1      " & vbNewLine _
            & "        THEN CAST(H_OUTKAEDI_DTL_RAPI.RECORD_STATUS AS NUMERIC(20)) " & vbNewLine _
            & "        ELSE 0                                                      " & vbNewLine _
            & "    END                                                             " & vbNewLine _
            & ""

#End Region ' "次回分納の出荷指示EDIデータの取得SQL"

#Region "次回分納のEDI出荷L の取得SQL"

    ''' <summary>
    ''' 次回分納のEDI出荷L の取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAEDI_L_JIKAI_BUNNOU As String = "" _
            & "SELECT                                    " & vbNewLine _
            & "      H_OUTKAEDI_L.JISSEKI_FLAG           " & vbNewLine _
            & "    , H_OUTKAEDI_L.SYS_UPD_DATE           " & vbNewLine _
            & "    , H_OUTKAEDI_L.SYS_UPD_TIME           " & vbNewLine _
            & "FROM                                      " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_L                " & vbNewLine _
            & "WHERE                                     " & vbNewLine _
            & "    H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD   " & vbNewLine _
            & "AND H_OUTKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO " & vbNewLine _
            & "AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'        " & vbNewLine _
            & ""

#End Region ' "次回分納のEDI出荷L の取得SQL"

#Region "次回分納の出荷L の取得SQL"

    ''' <summary>
    ''' 次回分納の出荷L の取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA_L_JIKAI_BUNNOU As String = "" _
            & "SELECT                                    " & vbNewLine _
            & "    C_OUTKA_L.SYS_DEL_FLG                 " & vbNewLine _
            & "FROM                                      " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_L                   " & vbNewLine _
            & "WHERE                                     " & vbNewLine _
            & "    C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
            & "AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_CTL_NO  " & vbNewLine _
            & ""

#End Region ' "次回分納の出荷L の取得SQL"

#End Region ' "SQL"

#End Region ' "Const"

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索"

#Region "特定の荷主固有のテーブルが存在するか否かの判定"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ390_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMZ390DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region ' "特定の荷主固有のテーブルが存在するか否かの判定"

#Region "次回分納情報有無確認対象の出荷指示EDIデータの取得"

    ''' <summary>
    ''' 次回分納情報有無確認対象の出荷指示EDIデータの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectOutkaEdiDtlRapi(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMZ390IN"
        Const OUT_TBL_NM As String = "H_OUTKAEDI_DTL_RAPI"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sqlStr As New Text.StringBuilder()
        sqlStr.Append(LMZ390DAC.SQL_SELECT_OUTKA_EDI_DTL_RAPI_1)
        If inRow.Item("EDI_CTL_NO").ToString() <> "" Then
            sqlStr.Append(LMZ390DAC.SQL_SELECT_OUTKA_EDI_DTL_RAPI_2)
        End If
        sqlStr.Append(LMZ390DAC.SQL_SELECT_OUTKA_EDI_DTL_RAPI_3)
        Dim sql As String = Me.SetSchemaNm(sqlStr.ToString(), inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", inRow.Item("OUTKA_CTL_NO").ToString(), DBDataType.VARCHAR))
                If inRow.Item("EDI_CTL_NO").ToString() <> "" Then
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
                End If
                .Add(MyBase.GetSqlParameter("@TEMPLATE_PREFIX", inRow.Item("TEMPLATE_PREFIX").ToString(), DBDataType.VARCHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先のマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count()
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "次回分納情報有無確認対象の出荷指示EDIデータの取得"

#Region "次回分納の出荷指示EDIデータの取得"

    ''' <summary>
    ''' 次回分納の出荷指示EDIデータの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectOutkaEdiDtlRapiJikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "H_OUTKAEDI_DTL_RAPI"
        Const OUT_TBL_NM As String = "H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMZ390DAC.SQL_SELECT_OUTKA_EDI_DTL_RAPI_JIKAI_BUNNOU, inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim fileName As String
            Dim pos As Integer = inRow.Item("FILE_NAME").ToString().IndexOf(inRow.Item("TEMPLATE_PREFIX").ToString())
            If pos < 0 Then
                fileName = inRow.Item("FILE_NAME").ToString() & "%"
            Else
                fileName = inRow.Item("FILE_NAME").ToString().Substring(0, pos) & "%"
            End If
            Dim recordStatus As String
            If IsNumeric(inRow.Item("RECORD_STATUS").ToString()) Then
                recordStatus = inRow.Item("RECORD_STATUS").ToString()
            Else
                recordStatus = "1"
            End If
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@FILE_NAME", fileName, DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@DENP_NO", inRow.Item("DENP_NO").ToString(), DBDataType.VARCHAR))
                .Add(MyBase.GetSqlParameter("@RECORD_STATUS", recordStatus, DBDataType.NUMERIC))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先のマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count()
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "次回分納の出荷指示EDIデータの取得"

#Region "次回分納のEDI出荷L の取得"

    ''' <summary>
    ''' 次回分納のEDI出荷L の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectOutkaEdiL_JikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU"
        Const OUT_TBL_NM As String = "H_OUTKAEDI_L"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMZ390DAC.SQL_SELECT_OUTKAEDI_L_JIKAI_BUNNOU, inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先のマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count()
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "次回分納のEDI出荷L の取得"

#Region "次回分納の出荷L の取得"

    ''' <summary>
    ''' 次回分納の出荷L の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectOutkaL_JikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU"
        Const OUT_TBL_NM As String = "C_OUTKA_L"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMZ390DAC.SQL_SELECT_OUTKA_L_JIKAI_BUNNOU, inRow.Item("NRS_BR_CD").ToString())

        ' 取得件数
        Dim cnt As Integer = 0

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            ' SQLパラメータの設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", inRow.Item("OUTKA_CTL_NO").ToString(), DBDataType.VARCHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先のマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count()
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "次回分納の出荷L の取得"

#End Region ' "検索"

#Region "共通"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">変換元SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>変換後SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region ' "共通"

#End Region ' "Method"

End Class
