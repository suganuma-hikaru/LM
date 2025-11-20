' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI180DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI180DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "出荷データ取得"

#Region "出荷データ取得 SQL"

#Region "出荷データ取得 SQL SELECT句"

    ''' <summary>
    ''' 出荷データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA As String = " SELECT                                                                     " & vbNewLine _
                                             & " OUTKAL.OUTKA_NO_L                                 AS OUTKA_NO_L            " & vbNewLine _
                                             & ",OUTKAL.CUST_CD_L                                  AS CUST_CD_L             " & vbNewLine _
                                             & ",CUST.CUST_NM_L                                    AS CUST_NM_L             " & vbNewLine _
                                             & ",CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                          " & vbNewLine _
                                             & "      ELSE OUTKAL.DEST_NM                                                   " & vbNewLine _
                                             & " END AS DEST_NM                                                             " & vbNewLine

#End Region

#Region "出荷データ取得 SQL FROM句"

    ''' <summary>
    ''' 出荷データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA As String = "FROM                                                                    " & vbNewLine _
                                                  & "$LM_TRN$..C_OUTKA_L OUTKAL                                              " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_CUST CUST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "OUTKAL.NRS_BR_CD = CUST.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "OUTKAL.CUST_CD_L = CUST.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "OUTKAL.CUST_CD_M = CUST.CUST_CD_M                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_S = '00'                                                   " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "CUST.CUST_CD_SS = '00'                                                  " & vbNewLine _
                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                  & "$LM_MST$..M_DEST DEST                                                   " & vbNewLine _
                                                  & "ON                                                                      " & vbNewLine _
                                                  & "OUTKAL.NRS_BR_CD = DEST.NRS_BR_CD                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "OUTKAL.CUST_CD_L = DEST.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND                                                                     " & vbNewLine _
                                                  & "OUTKAL.DEST_CD = DEST.DEST_CD                                           " & vbNewLine

#End Region

#Region "出荷データ取得 SQL WHERE句"

    ''' <summary>
    ''' 出荷データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_OUTKA As String = "WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                   & "  AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                  " & vbNewLine _
                                                   & "  AND OUTKAL.SYS_DEL_FLG = '0'                                         " & vbNewLine

#End Region

#End Region

#End Region

#Region "取込データ取得"

#Region "取込データ取得 SQL"

#Region "取込データ取得 SQL SELECT句"

    ''' <summary>
    ''' 取込データ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TORIKOMI As String = " SELECT                                                                     " & vbNewLine _
                                                & " NRC.NRC_REC_NO                                 AS NRC_REC_NO               " & vbNewLine _
                                                & ",NRC.NRS_BR_CD                                  AS NRS_BR_CD                " & vbNewLine _
                                                & ",NRC.OUTKA_NO_L                                 AS OUTKA_NO_L               " & vbNewLine _
                                                & ",NRC.EDA_NO                                     AS EDA_NO                   " & vbNewLine _
                                                & ",NRC.TOROKU_KB                                  AS TOROKU_KB                " & vbNewLine _
                                                & ",@SERIAL_NO                                     AS SERIAL_NO                " & vbNewLine _
                                                & ",NRC.HOKOKU_DATE                                AS HOKOKU_DATE              " & vbNewLine

#End Region

#Region "取込データ取得 SQL FROM句"

    ''' <summary>
    ''' 取込データ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_TORIKOMI As String = "FROM                                                                  " & vbNewLine _
                                                     & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                        " & vbNewLine

#End Region

#Region "取込データ取得 SQL WHERE句"

    ''' <summary>
    ''' 取込データ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_TORIKOMI As String = "WHERE NRC.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                                      & "  AND NRC.SERIAL_NO = @SERIAL_NO                                     " & vbNewLine _
                                                      & "  AND NRC.SYS_DEL_FLG = '0'                                          " & vbNewLine

#End Region

#End Region

#End Region

#Region "枝番号の最大値を取得"

#Region "枝番号の最大値を取得 SQL"

#Region "枝番号の最大値を取得 SQL SELECT句"

    ''' <summary>
    ''' 枝番号の最大値を取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAXEDA As String = " SELECT                                                                     " & vbNewLine _
                                              & " MAX(NRC.EDA_NO)                                  AS EDA_NO                 " & vbNewLine

#End Region

#Region "枝番号の最大値を取得 SQL FROM句"

    ''' <summary>
    ''' 枝番号の最大値を取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAXEDA As String = "FROM                                                                    " & vbNewLine _
                                                   & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                          " & vbNewLine

#End Region

#Region "枝番号の最大値を取得  SQL WHERE句"

    ''' <summary>
    ''' 枝番号の最大値を取得  SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_MAXEDA As String = "WHERE NRC.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                                    & "  AND NRC.OUTKA_NO_L = @OUTKA_NO_L                                    " & vbNewLine

#End Region

#Region "枝番号の最大値を取得  SQL GROUP BY句"

    ''' <summary>
    ''' 枝番号の最大値を取得  SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_MAXEDA As String = "GROUP BY                                                              " & vbNewLine _
                                                    & " NRC.NRS_BR_CD                                                        " & vbNewLine _
                                                    & ",NRC.OUTKA_NO_L                                                       " & vbNewLine

#End Region

#End Region

#End Region

#Region "NRC回収データ取得処理(保存時の入力チェック処理)"

#Region "NRC回収データ取得処理(保存時の入力チェック処理) SQL"

#Region "NRC回収データ取得処理(保存時の入力チェック処理) SQL SELECT句"

    ''' <summary>
    ''' 枝番号の最大値を取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SERIALNO As String = " SELECT                                                                     " & vbNewLine _
                                                & " NRC.SERIAL_NO                                    AS SERIAL_NO              " & vbNewLine

#End Region

#Region "NRC回収データ取得処理(保存時の入力チェック処理) SQL FROM句"

    ''' <summary>
    ''' NRC回収データ取得処理(保存時の入力チェック処理) SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SERIALNO As String = "FROM                                                                    " & vbNewLine _
                                                     & "$LM_TRN$..I_NRC_KAISHU_TBL NRC                                          " & vbNewLine

#End Region

#Region "NRC回収データ取得処理(保存時の入力チェック処理)  SQL WHERE句"

    ''' <summary>
    ''' NRC回収データ取得処理(保存時の入力チェック処理)  SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SERIALNO As String = "WHERE NRC.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                                      & "  AND (NRC.SERIAL_NO >= @SERIAL_NO_FROM                               " & vbNewLine _
                                                      & "  AND NRC.SERIAL_NO <= @SERIAL_NO_TO)                                 " & vbNewLine _
                                                      & "  AND NRC.SYS_DEL_FLG = '0'                                           " & vbNewLine

#End Region

#Region "NRC回収データ取得処理(保存時の入力チェック処理)  SQL ORDER BY句"

    ''' <summary>
    ''' NRC回収データ取得処理(保存時の入力チェック処理)  SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SERIALNO As String = "ORDER BY                                                              " & vbNewLine _
                                                      & " NRC.SERIAL_NO                                                        " & vbNewLine

#End Region

#End Region

#End Region

#Region "保存処理(追加の場合)"

#Region "保存処理(追加の場合) SQL"

    ''' <summary>
    ''' 保存処理(追加の場合) SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_NRC As String = "INSERT INTO $LM_TRN$..I_NRC_KAISHU_TBL              " & vbNewLine _
                                           & " ( 		                                          " & vbNewLine _
                                           & " NRC_REC_NO,                                        " & vbNewLine _
                                           & " NRS_BR_CD,                                         " & vbNewLine _
                                           & " OUTKA_NO_L,                                        " & vbNewLine _
                                           & " EDA_NO,                                            " & vbNewLine _
                                           & " TOROKU_KB,                                         " & vbNewLine _
                                           & " SERIAL_NO,                                         " & vbNewLine _
                                           & " HOKOKU_DATE,                                       " & vbNewLine _
                                           & " SYS_ENT_DATE,                                      " & vbNewLine _
                                           & " SYS_ENT_TIME,                                      " & vbNewLine _
                                           & " SYS_ENT_PGID,                                      " & vbNewLine _
                                           & " SYS_ENT_USER,                                      " & vbNewLine _
                                           & " SYS_UPD_DATE,                                      " & vbNewLine _
                                           & " SYS_UPD_TIME,                                      " & vbNewLine _
                                           & " SYS_UPD_PGID,                                      " & vbNewLine _
                                           & " SYS_UPD_USER,                                      " & vbNewLine _
                                           & " SYS_DEL_FLG                                        " & vbNewLine _
                                           & " ) VALUES (                                         " & vbNewLine _
                                           & " @NRC_REC_NO,                                       " & vbNewLine _
                                           & " @NRS_BR_CD,                                        " & vbNewLine _
                                           & " @OUTKA_NO_L,                                       " & vbNewLine _
                                           & " @EDA_NO,                                           " & vbNewLine _
                                           & " @TOROKU_KB,                                        " & vbNewLine _
                                           & " @SERIAL_NO,                                        " & vbNewLine _
                                           & " @HOKOKU_DATE,                                      " & vbNewLine _
                                           & " @SYS_ENT_DATE,                                     " & vbNewLine _
                                           & " @SYS_ENT_TIME,                                     " & vbNewLine _
                                           & " @SYS_ENT_PGID,                                     " & vbNewLine _
                                           & " @SYS_ENT_USER,                                     " & vbNewLine _
                                           & " @SYS_UPD_DATE,                                     " & vbNewLine _
                                           & " @SYS_UPD_TIME,                                     " & vbNewLine _
                                           & " @SYS_UPD_PGID,                                     " & vbNewLine _
                                           & " @SYS_UPD_USER,                                     " & vbNewLine _
                                           & " @SYS_DEL_FLG                                       " & vbNewLine _
                                           & " )                                                  " & vbNewLine

#End Region

#End Region

#Region "保存処理(更新の場合)"

#Region "保存処理(更新の場合) SQL"

    ''' <summary>
    ''' 保存処理(更新の場合) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_NRC As String = "UPDATE $LM_TRN$..I_NRC_KAISHU_TBL SET               " & vbNewLine _
                                           & "       TOROKU_KB              = @TOROKU_KB          " & vbNewLine _
                                           & "      ,HOKOKU_DATE            = @HOKOKU_DATE        " & vbNewLine _
                                           & "      ,SYS_UPD_DATE           = @SYS_UPD_DATE       " & vbNewLine _
                                           & "      ,SYS_UPD_TIME           = @SYS_UPD_TIME       " & vbNewLine _
                                           & "      ,SYS_UPD_PGID           = @SYS_UPD_PGID       " & vbNewLine _
                                           & "      ,SYS_UPD_USER           = @SYS_UPD_USER       " & vbNewLine _
                                           & " WHERE                                              " & vbNewLine _
                                           & "       NRC_REC_NO             = @NRC_REC_NO         " & vbNewLine

#End Region

#End Region

#Region "保存処理(取消の場合)"

#Region "保存処理(取消の場合) SQL"

    ''' <summary>
    ''' 保存処理(取消の場合) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_NRC_TORIKESHI As String = "UPDATE $LM_TRN$..I_NRC_KAISHU_TBL SET               " & vbNewLine _
                                                     & "       SYS_DEL_FLG            = '1'                 " & vbNewLine _
                                                     & "      ,SYS_UPD_DATE           = @SYS_UPD_DATE       " & vbNewLine _
                                                     & "      ,SYS_UPD_TIME           = @SYS_UPD_TIME       " & vbNewLine _
                                                     & "      ,SYS_UPD_PGID           = @SYS_UPD_PGID       " & vbNewLine _
                                                     & "      ,SYS_UPD_USER           = @SYS_UPD_USER       " & vbNewLine _
                                                     & " WHERE                                              " & vbNewLine _
                                                     & "       NRS_BR_CD              = @NRS_BR_CD          " & vbNewLine _
                                                     & "   AND OUTKA_NO_L             = @OUTKA_NO_L         " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

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

#Region "SQLメイン処理"

#Region "出荷データ取得"

    ''' <summary>
    ''' 出荷データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_OUTKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_FROM_OUTKA)  'SQL構築(From句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_WHERE_OUTKA) 'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLOutkaParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI180DAC", "SelectOutkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("DEST_NM", "DEST_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI180OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "取込データ取得"

    ''' <summary>
    ''' 取込データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function TorikomiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_TORIKOMI)       'SQL構築(Select句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_FROM_TORIKOMI)  'SQL構築(From句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_WHERE_TORIKOMI) 'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLTorikomiParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI180DAC", "TorikomiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRC_REC_NO", "NRC_REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("EDA_NO", "EDA_NO")
        map.Add("TOROKU_KB", "TOROKU_KB")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI180OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "枝番号の最大値を取得"

    ''' <summary>
    ''' 枝番号の最大値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMaxEdaNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_MAXEDA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_FROM_MAXEDA)  'SQL構築(From句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_WHERE_MAXEDA) 'SQL構築(Where句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_GROUP_MAXEDA) 'SQL構築(Group句)

        'パラメータの設定
        Call SetSQLMaxEdaParameter(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI180DAC", "SelectMaxEdaNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDA_NO", "EDA_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI180OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "NRC回収データ取得(保存時の入力チェック処理)"

    ''' <summary>
    ''' NRC回収データ取得(保存時の入力チェック処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShukkaCheckData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_SERIALNO)       'SQL構築(Select句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_FROM_SERIALNO)  'SQL構築(From句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_WHERE_SERIALNO) 'SQL構築(Where句)
        Me._StrSql.Append(LMI180DAC.SQL_SELECT_ORDER_SERIALNO) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLSerialNoParameter(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI180DAC", "ShukkaCheckData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SERIAL_NO", "SERIAL_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI180OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "保存処理(出荷の場合)"

    ''' <summary>
    ''' 保存処理(出荷の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertNrcShukkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_INSERT_NRC)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLInsertDataParameter(inTbl.Rows(i))

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI180DAC", "InsertNrcShukkaData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "保存処理(回収の追加の場合)"

    ''' <summary>
    ''' 保存処理(回収の追加の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertNrcKaishuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_INSERT_NRC)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLInsertDataParameter(inTbl.Rows(i))

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI180DAC", "InsertNrcKaishuData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "保存処理(回収の更新の場合)"

    ''' <summary>
    ''' 保存処理(回収の更新の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateNrcKaishuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_UPDATE_NRC)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLUpdateDataParameter(inTbl.Rows(i))

            'SQLパラメータ（システム項目）設定
            Call Me.SetSysdataParameter()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI180DAC", "UpdateNrcKaishuData", cmd)

            'SQLの発行
            Me.UpdateResultChk(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "保存処理(取消の場合)"

    ''' <summary>
    ''' 保存処理(取消の更新の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateNrcTorikeshiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI180IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI180DAC.SQL_UPDATE_NRC_TORIKESHI)         'SQL構築(INSERT句)
        Call SetSQLWhereTorikeshi(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSQLTorikeshiDataParameter(inTbl.Rows(0))

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI180DAC", "UpdateNrcTorikeshiData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 出荷データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLOutkaParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 取込データ取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLTorikomiParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 枝番号の最大値を取得"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLMaxEdaParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 NRC回収データ取得(保存時の入力チェック処理)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLSerialNoParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO_FROM", .Item("SERIAL_NO_FROM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO_TO", .Item("SERIAL_NO_TO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 保存処理(追加の場合)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInsertDataParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRC_REC_NO", .Item("NRC_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", .Item("EDA_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOROKU_KB", .Item("TOROKU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 保存処理(更新の場合)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLUpdateDataParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRC_REC_NO", .Item("NRC_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOROKU_KB", .Item("TOROKU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 保存処理(取消の場合)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLTorikeshiDataParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereTorikeshi(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            'シリアル№FROM
            whereStr = .Item("SERIAL_NO_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SERIAL_NO >= @SERIAL_NO_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            'シリアル№TO
            whereStr = .Item("SERIAL_NO_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SERIAL_NO <= @SERIAL_NO_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO_TO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

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
