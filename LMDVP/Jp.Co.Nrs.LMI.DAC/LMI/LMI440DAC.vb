' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI440  : 
'  作  成  者       :  [inoue]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMI440DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI440DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = "LMI440IN"

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_TRANSPORT As String = "LMI440IN_TRANSPORT"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = "LMI440OUT"

    End Class

#Region "カラム名定義"
    Public Class COL_NAME

        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_CD_M As String = "CUST_CD_M"
        Public Const SHIPMENT_DOCUMENT_NUMBER As String = "SHIPMENT_DOCUMENT_NUMBER"
        Public Const DELIVERY_NOTE_NUMBER As String = "DELIVERY_NOTE_NUMBER"
        Public Const CARRIER_PASTE_IN_DATES As String = "CARRIER_PASTE_IN_DATES"
        Public Const ROW_NO As String = "ROW_NO"

        Public Const ARR_PLAN_DATE_FROM As String = "ARR_PLAN_DATE_FROM"
        Public Const ARR_PLAN_DATE_TO As String = "ARR_PLAN_DATE_TO"
        Public Const TRANSPORT_DB_NAME As String = "TRANSPORT_DB_NAME"

    End Class



    Public Class INPUT_COLUMN_NM

        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const CUST_CD_L As String = COL_NAME.CUST_CD_L
        Public Const CUST_CD_M As String = COL_NAME.CUST_CD_M
        Public Const ARR_PLAN_DATE_FROM As String = COL_NAME.ARR_PLAN_DATE_FROM
        Public Const ARR_PLAN_DATE_TO As String = COL_NAME.ARR_PLAN_DATE_TO
        Public Const TRANSPORT_DB_NAME As String = COL_NAME.TRANSPORT_DB_NAME

    End Class


    Public Class IN_TRAPO_COLUMN_NM
        Public Const ROW_NO As String = COL_NAME.ROW_NO
        Public Const SHIPMENT_DOCUMENT_NUMBER As String = COL_NAME.SHIPMENT_DOCUMENT_NUMBER
        Public Const DELIVERY_NOTE_NUMBER As String = COL_NAME.DELIVERY_NOTE_NUMBER
    End Class




    Public Class OUTPUT_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const CUST_CD_L As String = COL_NAME.CUST_CD_L
        Public Const CUST_CD_M As String = COL_NAME.CUST_CD_M
        Public Const ROW_NO As String = COL_NAME.ROW_NO
        Public Const SHIPMENT_DOCUMENT_NUMBER As String = COL_NAME.SHIPMENT_DOCUMENT_NUMBER
        Public Const DELIVERY_NOTE_NUMBER As String = COL_NAME.DELIVERY_NOTE_NUMBER
        Public Const CARRIER_PASTE_IN_DATES As String = COL_NAME.CARRIER_PASTE_IN_DATES
    End Class


#End Region


#Region "関数名定義"

    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME


        ''' <summary>
        ''' 一時テーブル作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CreateTempTable As String = "CreateTempTable"

        ''' <summary>
        ''' 出荷データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InsertTempTable As String = "InsertTempTable"

        ''' <summary>
        ''' 運送データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectUnsoData As String = "SelectUnsoData"

        ''' <summary>
        ''' 輸送データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectTransportData As String = "SelectTransportData"

    End Class
#End Region

#Region "一時テーブル"

    Private Const SQL_CREATE_TEMP_TABLE As String _
        = " CREATE TABLE #LMI440_WK                        " & vbNewLine _
        & "      ( ROW_NO                   numeric        " & vbNewLine _
        & "      , SHIPMENT_DOCUMENT_NUMBER varchar(100)   " & vbNewLine _
        & "      , DELIVERY_NOTE_NUMBER     varchar(100)   " & vbNewLine _
        & "      )                                         " & vbNewLine

    Private Const SQL_INSERT_TEMP_TABLE As String _
        = " INSERT INTO #LMI440_WK                         " & vbNewLine _
        & "      ( ROW_NO                                  " & vbNewLine _
        & "      , SHIPMENT_DOCUMENT_NUMBER                " & vbNewLine _
        & "      , DELIVERY_NOTE_NUMBER                    " & vbNewLine _
        & "      )                                         " & vbNewLine _
        & " VALUES                                         " & vbNewLine _
        & "      ( @ROW_NO                                 " & vbNewLine _
        & "      , @SHIPMENT_DOCUMENT_NUMBER               " & vbNewLine _
        & "      , @DELIVERY_NOTE_NUMBER                   " & vbNewLine _
        & "      )                                         " & vbNewLine

#End Region


#Region "検索"

    Private Const SQL_SELECT_UNSO_DATE As String _
        = " SELECT                                                                                   " & vbNewLine _
        & "        TP.ROW_NO AS ROW_NO                                                               " & vbNewLine _
        & "      , CM.CARRIER_PASTE_IN_DATES AS CARRIER_PASTE_IN_DATES                               " & vbNewLine _
        & "   FROM                                                                                   " & vbNewLine _
        & "        #LMI440_WK AS TP                                                                  " & vbNewLine _
        & "   LEFT JOIN (                                                                            " & vbNewLine _
        & "               SELECT                                                                     " & vbNewLine _
        & "                      FL.CUST_REF_NO          AS CUST_ORD_NO                              " & vbNewLine _
        & "                    , FL.ARR_PLAN_DATE        AS CARRIER_PASTE_IN_DATES                   " & vbNewLine _
        & "                 FROM                                                                     " & vbNewLine _
        & "                      $LM_TRN$..F_UNSO_L AS FL                                            " & vbNewLine _
        & "                WHERE                                                                     " & vbNewLine _
        & "                      FL.SYS_DEL_FLG          = '0'                                       " & vbNewLine _
        & "                  AND LEN(FL.CUST_REF_NO)     > 0                                         " & vbNewLine _
        & "                  AND FL.NRS_BR_CD            = @NRS_BR_CD                                " & vbNewLine _
        & "                  AND FL.CUST_CD_L            = @CUST_CD_L                                " & vbNewLine _
        & "                  {0}                                                                     " & vbNewLine _
        & "                GROUP BY                                                                  " & vbNewLine _
        & "                      FL.CUST_REF_NO                                                      " & vbNewLine _
        & "                    , FL.ARR_PLAN_DATE                                                    " & vbNewLine _
        & "              ) AS CM                                                                     " & vbNewLine _
        & "     ON                                                                                   " & vbNewLine _
        & "        CM.CUST_ORD_NO LIKE '%' + TP.SHIPMENT_DOCUMENT_NUMBER                             " & vbNewLine _
        & "     OR CM.CUST_ORD_NO LIKE '%' + TP.DELIVERY_NOTE_NUMBER                                 " & vbNewLine _
        & "  WHERE CM.CARRIER_PASTE_IN_DATES IS NOT NULL                                             " & vbNewLine


    Private Const SQL_WHERE_UNSO_DATA_ADD_CUST_CD_M As String _
        = "     AND FL.CUST_CD_M   = @CUST_CD_M                                                      " & vbNewLine

    Private Const SQL_WHERE_UNSO_DATA_ADD_ARR_PLAN_DATE_FROM As String _
        = "     AND FL.ARR_PLAN_DATE >= @ARR_PLAN_DATE_FROM                                          " & vbNewLine

    Private Const SQL_WHERE_UNSO_DATA_ADD_ARR_PLAN_DATE_TO As String _
        = "     AND FL.ARR_PLAN_DATE <= @ARR_PLAN_DATE_TO                                            " & vbNewLine



    Private Const SQL_SELECT_TRANSPORT_DATE As String _
        = " SELECT                                                                                                    " & vbNewLine _
        & "        TP.ROW_NO                                     AS ROW_NO                                            " & vbNewLine _
        & "      , CASE WHEN LEN(ISNULL(DOW.TMOR_DATE, '')) = 0                                                       " & vbNewLine _
        & "             THEN DOW.ORS_DATE            -- 卸日                                                          " & vbNewLine _
        & "             ELSE DOW.TMOR_DATE           -- 積卸日                                                        " & vbNewLine _
        & "         END                                          AS CARRIER_PASTE_IN_DATES                            " & vbNewLine _
        & "   FROM                                                                                                    " & vbNewLine _
        & "        #LMI440_WK  AS TP                                                                                  " & vbNewLine _
        & "   LEFT JOIN                                                                                               " & vbNewLine _
        & "        {0}.V_YUSOINFO_DOW AS DOW                                                                          " & vbNewLine _
        & "     ON                                                                                                    " & vbNewLine _
        & "        {1}                                                                                                " & vbNewLine _
        & "        (  RIGHT(DOW.NNUJYT_NO, 8) = TP.SHIPMENT_DOCUMENT_NUMBER COLLATE Japanese_Bushu_Kakusu_100_CI_AS   " & vbNewLine _
        & "        OR RIGHT(DOW.NNUKYK_NO, 9) = TP.DELIVERY_NOTE_NUMBER     COLLATE Japanese_Bushu_Kakusu_100_CI_AS   " & vbNewLine _
        & "        OR RIGHT(DOW.NNUJYT_NO, 8) = TP.SHIPMENT_DOCUMENT_NUMBER COLLATE Japanese_Bushu_Kakusu_100_CI_AS   " & vbNewLine _
        & "        OR RIGHT(DOW.NNUKYK_NO, 9) = TP.DELIVERY_NOTE_NUMBER     COLLATE Japanese_Bushu_Kakusu_100_CI_AS)  " & vbNewLine _
        & "  WHERE DOW.ORS_DATE IS NOT NULL                                                                           " & vbNewLine


    Private Const SQL_WHERE_TRANSPORT_DATA_ADD_ARR_PLAN_DATE_FROM As String _
        = "        DOW.ORS_DATE   >= @ARR_PLAN_DATE_FROM AND                       " & vbNewLine

    Private Const SQL_WHERE_TRANSPORT_DATA_ADD_ARR_PLAN_DATE_TO As String _
        = "        DOW.ORS_DATE   <= @ARR_PLAN_DATE_TO   AND                       " & vbNewLine

#End Region


#End Region

#Region "Field"

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder = Nothing

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = Nothing

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "取込データ取得"


    Private Function CreateTempTable(ByVal ds As DataSet) As DataSet

        Using cmd As SqlCommand = MyBase.CreateSqlCommand(LMI440DAC.SQL_CREATE_TEMP_TABLE)

            ' ログ出力
            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            ' 実行
            Me.SetResultCount(cmd.ExecuteNonQuery())

        End Using

        Return ds
    End Function

    Private Function InsertTempTable(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.IN_TRANSPORT).Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With inRow
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", .Item(IN_TRAPO_COLUMN_NM.ROW_NO).ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DOCUMENT_NUMBER", .Item(IN_TRAPO_COLUMN_NM.SHIPMENT_DOCUMENT_NUMBER).ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_NOTE_NUMBER", .Item(IN_TRAPO_COLUMN_NM.DELIVERY_NOTE_NUMBER).ToString(), DBDataType.VARCHAR))
        End With

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(LMI440DAC.SQL_INSERT_TEMP_TABLE)

            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        End Using

        Return ds

    End Function



    ''' <summary>
    ''' 出荷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectUnsoData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        ' 営業所コード
        Dim nrsBrCd As String = inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI440DAC.SQL_SELECT_UNSO_DATE)

        ' 検索条件設定
        Dim whereClause As String = Me.GetAppendOutkaSqlWhereClause(inRow)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(String.Format(Me._StrSql.ToString, whereClause), nrsBrCd)

        ' SQLパラメータ設定
        Me.SetSQLSelectDataParameter(inRow)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    'DataReader→DataTableへの転記
                    Dim map As Hashtable = New Hashtable()

                    For Each column As String In {
                                                 OUTPUT_COLUMN_NM.ROW_NO,
                                                 OUTPUT_COLUMN_NM.CARRIER_PASTE_IN_DATES
                                                }
                        map.Add(column, column)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUTPUT)

                    MyBase.SetResultCount(ds.Tables(TABLE_NAME.OUTPUT).Rows.Count)

                End If

            End Using

            Return ds

        End Using

    End Function


    Private Function SelectTransportData(ByVal ds As DataSet) As DataSet


        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder(LMI440DAC.SQL_SELECT_TRANSPORT_DATE)


        Dim dbName As String = Me.GetTranspoteDbName(inRow)

        ' 検索条件設定
        Dim whereClause As String = Me.GetAppendTranspoteSqlWhereClause(inRow)

        'スキーマ名設定
        Dim sql As String = String.Format(Me._StrSql.ToString(), dbName, whereClause)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQLパラメータ設定
        Me.SetSQLSelectDataParameter(inRow)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray())

            ' ログ出力
            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                Dim rowCount As Integer = 0

                If (reader.HasRows) Then

                    'DataReader→DataTableへの転記
                    Dim map As Hashtable = New Hashtable()

                    '取得データの格納先をマッピング
                    For Each column As String In {
                                                 OUTPUT_COLUMN_NM.ROW_NO,
                                                 OUTPUT_COLUMN_NM.CARRIER_PASTE_IN_DATES
                                                }
                        map.Add(column, column)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUTPUT)
                    rowCount = ds.Tables(TABLE_NAME.OUTPUT).Rows.Count

                End If

                MyBase.SetResultCount(rowCount)

            End Using

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' Where条件追加(出荷)
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Function GetAppendOutkaSqlWhereClause(ByVal row As DataRow) As String

        Dim where As New StringBuilder()
        If (Len(row.Item(INPUT_COLUMN_NM.CUST_CD_M)) > 0) Then
            where.Append(LMI440DAC.SQL_WHERE_UNSO_DATA_ADD_CUST_CD_M)
        End If

        If (Len(row.Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_FROM)) > 0) Then
            where.Append(LMI440DAC.SQL_WHERE_UNSO_DATA_ADD_ARR_PLAN_DATE_FROM)
        End If

        If (Len(row.Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_TO)) > 0) Then
            where.Append(LMI440DAC.SQL_WHERE_UNSO_DATA_ADD_ARR_PLAN_DATE_TO)
        End If

        Return where.ToString

    End Function


    ''' <summary>
    ''' 輸送データーベース名取得
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTranspoteDbName(ByVal row As DataRow) As String

        Dim dbName As String = ""

        If (row IsNot Nothing) Then
            dbName = If(row.Item(INPUT_COLUMN_NM.TRANSPORT_DB_NAME), "").ToString()
        End If

        Return dbName

    End Function

    ''' <summary>
    ''' Where条件追加(輸送)
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Function GetAppendTranspoteSqlWhereClause(ByVal row As DataRow) As String

        Dim where As New StringBuilder()
        If (Len(row.Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_FROM)) > 0) Then
            where.Append(LMI440DAC.SQL_WHERE_TRANSPORT_DATA_ADD_ARR_PLAN_DATE_FROM)
        End If

        If (Len(row.Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_TO)) > 0) Then
            where.Append(LMI440DAC.SQL_WHERE_TRANSPORT_DATA_ADD_ARR_PLAN_DATE_TO)
        End If

        Return where.ToString

    End Function


#End Region

#End Region

#Region "SQLパラメータ設定"

    ''' <summary>
    ''' パラメータ設定(取込ファイル取得)
    ''' </summary>
    ''' <param name="inRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectDataParameter(ByVal inRow As DataRow)

        If (inRow IsNot Nothing) Then
            With inRow
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item(INPUT_COLUMN_NM.CUST_CD_L).ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item(INPUT_COLUMN_NM.CUST_CD_M).ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_FROM", .Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_FROM).ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE_TO", .Item(INPUT_COLUMN_NM.ARR_PLAN_DATE_TO).ToString(), DBDataType.CHAR))
            End With
        End If

    End Sub

#Region "パラメータ設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter()

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter()

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function


#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class


