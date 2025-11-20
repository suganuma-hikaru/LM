' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMQ010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMQ010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "編集処理 SQL"

    ''' <summary>
    ''' 排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                                   " & vbNewLine _
                                          & "       COUNT(PATTERN_ID)     AS    SELECT_CNT            " & vbNewLine _
                                          & "FROM                                                     " & vbNewLine _
                                          & "     $LM_MST$..M_SQL                                     " & vbNewLine _
                                          & "WHERE                                                    " & vbNewLine _
                                          & "       PATTERN_ID         =   @PATTERN_ID                " & vbNewLine _
                                          & "AND    SYS_UPD_DATE       =    @HAITA_DATE               " & vbNewLine _
                                          & "AND    SYS_UPD_TIME       =    @HAITA_TIME               " & vbNewLine

 
#End Region

#Region "SELECT"

    ''' <summary>
    ''' カウント用（検索時、存在チェック時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(M_SQL.PATTERN_ID)		            AS SELECT_CNT       " & vbNewLine

    ''' <summary>
    ''' M_SQL データ抽出用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                     " & vbNewLine _
                                            & " M_SQL.PATTERN_ID                                    AS PATTERN_ID          " & vbNewLine _
                                            & ",M_SQL.EX_TYPE_KB                                    AS EX_TYPE_KB          " & vbNewLine _
                                            & ",M_SQL.EX_CONTENTS                                   AS EX_CONTENTS	       " & vbNewLine _
                                            & ",M_SQL.EX_SQL                                        AS EX_SQL              " & vbNewLine _
                                            & ",Z3.KBN_NM2                                          AS FILE_NM	           " & vbNewLine _
                                            & ",M_SQL.FILE_TITLE_NM                                 AS FILE_TITLE_NM       " & vbNewLine _
                                            & ",M_SQL.LAST_ACTION_DATE                              AS LAST_ACTION_DATE    " & vbNewLine _
                                            & ",M_SQL.SYS_ENT_DATE                                  AS SYS_ENT_DATE        " & vbNewLine _
                                            & ",M_SQL.SYS_ENT_USER                                  AS SYS_ENT_USER        " & vbNewLine _
                                            & ",M_SQL.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",M_SQL.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",M_SQL.SYS_DEL_FLG                                   AS SYS_DEL_FLG	       " & vbNewLine _
                                            & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER_NM     " & vbNewLine _
                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER_NM     " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS SYS_DEL_FLG_NM      " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS EX_TYPE_NM          " & vbNewLine

    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                   " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..S_USER    ENT_USER                    " & vbNewLine _
                                         & "ON     M_SQL.SYS_ENT_USER = ENT_USER.USER_CD              " & vbNewLine _
                                         & "AND    ENT_USER.SYS_DEL_FLG = 0                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..S_USER    UPD_USER                    " & vbNewLine _
                                         & "ON     M_SQL.SYS_UPD_USER = UPD_USER.USER_CD              " & vbNewLine _
                                         & "AND    UPD_USER.SYS_DEL_FLG = 0                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     M_SQL.SYS_DEL_FLG = Z1.KBN_CD                      " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S051'                           " & vbNewLine _
                                         & "AND    Z1.SYS_DEL_FLG = 0                                 " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     M_SQL.EX_TYPE_KB = Z3.KBN_CD                       " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'E001'                           " & vbNewLine _
                                         & "AND    Z3.SYS_DEL_FLG = 0                                 " & vbNewLine
    '& "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
    '& "ON     M_SQL.EX_TYPE_KB = Z2.KBN_CD                       " & vbNewLine _
    '& "AND    Z2.KBN_GROUP_CD = 'T014'                           " & vbNewLine _
    '& "AND    Z2.SYS_DEL_FLG = 0                                 " & vbNewLine _

    ''' <summary>
    ''' ORDER BY（①システムフラグ、②パターンID）（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY As String = "ORDER BY                                          " & vbNewLine _
                                         & " M_SQL.EX_TYPE_KB                                        " & vbNewLine _
                                         & ",M_SQL.PATTERN_ID                                        " & vbNewLine

    ''' <summary>
    ''' FROM（存在チェック時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CHECK As String = "FROM                                             " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                           " & vbNewLine

    ''' <summary>
    ''' WHERE（存在チェック時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_CHECK As String = "WHERE                                           " & vbNewLine _
                                         & "PATTERN_ID = @PATTERN_ID                                  " & vbNewLine

#End Region

#Region "INSERT"
    ''' <summary>
    ''' INSERT（保存時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO                                                " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                          " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & "PATTERN_ID                                               " & vbNewLine _
                                         & ",EX_TYPE_KB                                              " & vbNewLine _
                                         & ",EX_CONTENTS                                             " & vbNewLine _
                                         & ",EX_SQL                                                  " & vbNewLine _
                                         & ",FILE_NM                                                 " & vbNewLine _
                                         & ",FILE_TITLE_NM                                           " & vbNewLine _
                                         & ",LAST_ACTION_DATE                                        " & vbNewLine _
                                         & ",LAST_ACTION_USER                                        " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & "@PATTERN_ID                                              " & vbNewLine _
                                         & ",@EX_TYPE_KB                                             " & vbNewLine _
                                         & ",@EX_CONTENTS                                            " & vbNewLine _
                                         & ",@EX_SQL                                                 " & vbNewLine _
                                         & ",@FILE_NM                                                " & vbNewLine _
                                         & ",@FILE_TITLE_NM                                          " & vbNewLine _
                                         & ",@LAST_ACTION_DATE                                       " & vbNewLine _
                                         & ",@LAST_ACTION_USER                                       " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "UPDATE"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

    ''' <summary>
    ''' UPDATE（保存時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE                                                     " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                          " & vbNewLine _
                                         & "SET                                                      " & vbNewLine _
                                         & "EX_TYPE_KB = @EX_TYPE_KB                                 " & vbNewLine _
                                         & ",EX_CONTENTS = @EX_CONTENTS                              " & vbNewLine _
                                         & ",EX_SQL = @EX_SQL                                        " & vbNewLine _
                                         & ",FILE_NM = @FILE_NM                                      " & vbNewLine _
                                         & ",FILE_TITLE_NM = @FILE_TITLE_NM                          " & vbNewLine _
                                         & ",LAST_ACTION_DATE = @LAST_ACTION_DATE                    " & vbNewLine _
                                         & ",LAST_ACTION_USER = @LAST_ACTION_USER                    " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "PATTERN_ID = @PATTERN_ID                                 " & vbNewLine

    ''' <summary>
    ''' UPDATE（削除・復活時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELREV As String = "UPDATE                                                     " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                          " & vbNewLine _
                                         & "SET                                                      " & vbNewLine _
                                         & "SYS_DEL_FLG = @SYS_DEL_FLG                               " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "PATTERN_ID = @PATTERN_ID                                 " & vbNewLine

    ''' <summary>
    ''' UPDATE（EXCEL作成時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXCEL_UPDATE As String = "UPDATE                                               " & vbNewLine _
                                         & "$LM_MST$..M_SQL                                          " & vbNewLine _
                                         & "SET                                                      " & vbNewLine _
                                         & "LAST_ACTION_DATE = @LAST_ACTION_DATE                     " & vbNewLine _
                                         & ",LAST_ACTION_USER = @LAST_ACTION_USER                    " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "PATTERN_ID = @PATTERN_ID                                 " & vbNewLine
#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _ROW As Data.DataRow

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' SQLデータ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索結果取得SQLの構築・発行</remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMQ010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMQ010DAC.SQL_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._ROW.Item("NRS_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "HaitaChk", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._ROW

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", .Item("PATTERN_ID").ToString(), DBDataType.NVARCHAR))
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With


        '主キー

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMQ010IN")

        'INTableの条件rowの格納
        Me._ROW = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMQ010IN")

        'INTableの条件rowの格納
        Me._ROW = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_ORDER_BY)  'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PATTERN_ID", "PATTERN_ID")
        map.Add("EX_TYPE_KB", "EX_TYPE_KB")
        map.Add("EX_CONTENTS", "EX_CONTENTS")
        map.Add("EX_SQL", "EX_SQL")
        map.Add("FILE_NM", "FILE_NM")
        map.Add("FILE_TITLE_NM", "FILE_TITLE_NM")
        map.Add("LAST_ACTION_DATE", "LAST_ACTION_DATE")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG_NM", "SYS_DEL_FLG_NM")
        map.Add("EX_TYPE_NM", "EX_TYPE_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMQ010INOUT")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhere()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("WHERE                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("(M_SQL.SYS_DEL_FLG  = '0'                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OR M_SQL.SYS_DEL_FLG  = '1')                                 ")
        Me._StrSql.Append(vbNewLine)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._ROW

            'パターンID
            whereStr = .Item("PATTERN_ID").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.PATTERN_ID LIKE @PATTERN_ID")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '抽出種別区分
            whereStr = .Item("EX_TYPE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.EX_TYPE_KB = @EX_TYPE_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_TYPE_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '抽出内容
            whereStr = .Item("EX_CONTENTS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.EX_CONTENTS LIKE @EX_CONTENTS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_CONTENTS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '抽出SQL
            whereStr = .Item("EX_SQL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.EX_SQL LIKE @EX_SQL")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_SQL", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EXCELファイル名
            whereStr = .Item("FILE_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.FILE_NM LIKE @FILE_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EXCELタイトル名
            whereStr = .Item("FILE_TITLE_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.FILE_TITLE_NM LIKE @FILE_TITLE_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_TITLE_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '削除フラグ
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SQL.SYS_DEL_FLG = @SYS_DEL_FLG")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", String.Concat(whereStr), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' Excel出力データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>Excel出力データ取得SQLの構築・発行</remarks>
    Private Function ExcelMake(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMQ010_EXCEL")

        'INTableの条件rowの格納
        Me._ROW = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(_ROW.Item("SQL").ToString())

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'タイムアウトの設定
        cmd.CommandTimeout = 1200

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "ExcelMake", cmd)

        Try
            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            Dim max As Integer = reader.GetSchemaTable.Rows.Count - 1
            Dim dt As DataTable = New DataTable

            ds = New DataSet("LMQ000OUT")
            dt = ds.Tables.Add("LMQ000OUT")

            'START YANAI 要望番号392
            Dim sysPageKeyFlg As Boolean = False
            'END YANAI 要望番号392
            'SqlDataReaderからDataTableに値を設定
            For i As Integer = 0 To max
                dt.Columns.Add(DirectCast(reader.GetSchemaTable.Rows(i).Item(0), String), GetType(String))
                'START YANAI 要望番号392
                If ("SYS_PAGE_KEY").Equals(reader.GetSchemaTable.Rows(i).Item(0)) = True Then
                    sysPageKeyFlg = True
                End If
                'END YANAI 要望番号392
            Next
            '末尾にシステム日付、時間を設定する
            dt.Columns.Add("LMQ010_SYS_ENT_DATE", GetType(String))
            dt.Columns.Add("LMQ010_SYS_ENT_TIME", GetType(String))
            'START YANAI 要望番号392
            'dt.Columns.Add("SYS_PAGE_KEY", GetType(String))
            If sysPageKeyFlg = False Then
                dt.Columns.Add("SYS_PAGE_KEY", GetType(String))
            End If
            'END YANAI 要望番号392

            Dim readerCnt As Integer = 0
            max = reader.FieldCount - 1
            Do While reader.Read
                Dim row As DataRow = dt.NewRow
                For i As Integer = 0 To max
                    row(i) = reader.Item(i)
                Next
                '末尾にシステム日付、時間を設定する
                row.Item("LMQ010_SYS_ENT_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(MyBase.GetSystemDate)
                row.Item("LMQ010_SYS_ENT_TIME") = String.Concat(MyBase.GetSystemTime.Substring(0, 2), ":", MyBase.GetSystemTime.Substring(2, 2), ":", MyBase.GetSystemTime.Substring(4, 2))
                'START YANAI 要望番号392
                'row.Item("SYS_PAGE_KEY") = String.Empty
                If sysPageKeyFlg = False Then
                    row.Item("SYS_PAGE_KEY") = String.Empty
                End If
                'END YANAI 要望番号392

                dt.Rows.Add(row)
                readerCnt = readerCnt + 1
            Loop

            '件数設定
            MyBase.SetResultCount(readerCnt)

            Return ds

        Catch ex As Exception

            '件数設定（エラー時は-1を設定）
            MyBase.SetResultCount(-1)

            ds = Nothing
            Return ds

        End Try

    End Function

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMSQLData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_COUNT)       'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_FROM_CHECK)  'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMQ010DAC.SQL_SELECT_WHERE_CHECK) 'SQL構築(データ抽出用Where句)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat(Me._ROW.Item("PATTERN_ID").ToString), DBDataType.NVARCHAR))

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "SelectMSQLData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "変更処理"

    ''' <summary>
    ''' データ抽出SQLマスタ 更新（保存時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        '件数格納変数
        Dim saveCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMQ010INOUT")

        'INTableの条件rowの格納
        Me._ROW = dt.Rows(0)

        '存在チェック用SQL実行
        Me.SelectMSQLData(ds)

        '新規・更新判定
        Dim mode As String = String.Empty '新規：1、更新：2
        If RecordStatus.NEW_REC.Equals(_ROW.Item("RECORD_STATUS").ToString) = True Then
            If 0 < MyBase.GetResultCount() Then
                '既に存在するため、エラー
                MyBase.SetMessage("E160", New String() {"データ抽出SQLマスタ", String.Concat("[パターンID=", Me._ROW.Item("PATTERN_ID").ToString, "]")})
                MyBase.SetResultCount(1)
                Return ds
            End If
            mode = "1"
        Else
            If 0 = MyBase.GetResultCount() Then
                '存在しないため、エラー
                MyBase.SetMessage("E079", New String() {"データ抽出SQLマスタ", String.Concat("[パターンID=", Me._ROW.Item("PATTERN_ID").ToString, "]")})
                MyBase.SetResultCount(1)
                Return ds
            End If
            mode = "2"
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If "1".Equals(mode) = True Then
            'パターンIDが設定されていない場合はINSERT
            Call Me.SetInsertSQL(dt, "M_SQL")    'SQL構築
        Else
            Call Me.SetUpdateSQL(dt, "M_SQL")    'SQL構築
        End If

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "SaveData", cmd)

        'SQLの発行 + 処理件数登録
        If "1".Equals(mode) = True Then
            saveCnt = saveCnt + MyBase.GetInsertResult(cmd)
        Else
            saveCnt = saveCnt + MyBase.GetUpdateResult(cmd)
        End If

        MyBase.SetResultCount(saveCnt)

        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLマスタ 更新（削除・復活時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelRevSQL(ByVal ds As DataSet) As DataSet

        '件数格納変数
        Dim saveCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMQ010INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'INTableの条件rowの格納
        Me._ROW = dt.Rows(0)

        'SQL作成
        Call Me.SetDelRevSQL(dt, "M_SQL")    'SQL構築

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "DelRevSQL", cmd)

        'SQLの発行 + 処理件数登録
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLマスタ 更新（Excel作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveExcelData(ByVal ds As DataSet) As DataSet

        '件数格納変数
        Dim saveCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMQ010INOUT")

        'INTableの条件rowの格納
        Me._ROW = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Call Me.SetUpdateExcelSQL(dt, "M_SQL")    'SQL構築

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, Me._ROW.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMQ010DAC", "SaveExcelData", cmd)

        'SQLの発行 + 処理件数登録
        saveCnt = saveCnt + MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(saveCnt)

        Return ds

    End Function

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール(データ抽出SQL 新規追加）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsertSQL(ByVal ds As DataTable, ByVal updateKeyword As String)

        'SQL作成
        Me._StrSql.Append(LMQ010DAC.SQL_INSERT)     'SQL構築(INSERT文)

        'パラメータ
        With Me._ROW
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat(.Item("PATTERN_ID").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_TYPE_KB", String.Concat(.Item("EX_TYPE_KB").ToString), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_CONTENTS", String.Concat(.Item("EX_CONTENTS").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_SQL", String.Concat(.Item("EX_SQL").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NM", String.Concat(.Item("FILE_NM").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_TITLE_NM", String.Concat(.Item("FILE_TITLE_NM").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", String.Concat(MyBase.GetSystemTime), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", String.Concat(MyBase.GetPGID), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat(MyBase.GetUserID), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", String.Concat(MyBase.GetSystemTime), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", String.Concat(MyBase.GetPGID), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", String.Concat(0), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール(データ抽出SQL 更新）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdateSQL(ByVal ds As DataTable, ByVal updateKeyword As String)

        'SQL作成
        Me._StrSql.Append(String.Concat(LMQ010DAC.SQL_UPDATE, LMQ010DAC.SQL_COM_UPDATE_CONDITION))     'SQL構築(UPDATE文)

        'パラメータ
        With Me._ROW
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat(.Item("PATTERN_ID").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_TYPE_KB", String.Concat(.Item("EX_TYPE_KB").ToString), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_CONTENTS", String.Concat(.Item("EX_CONTENTS").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_SQL", String.Concat(.Item("EX_SQL").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NM", String.Concat(.Item("FILE_NM").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_TITLE_NM", String.Concat(.Item("FILE_TITLE_NM").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", String.Concat(MyBase.GetSystemTime), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", String.Concat(MyBase.GetPGID), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", String.Concat(0), DBDataType.CHAR))
        End With
        Call Me.SetSysDateTime(Me._ROW, Me._SqlPrmList)

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール(データ抽出SQL 更新）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdateExcelSQL(ByVal ds As DataTable, ByVal updateKeyword As String)

        'SQL作成
        Me._StrSql.Append(String.Concat(LMQ010DAC.SQL_EXCEL_UPDATE, LMQ010DAC.SQL_COM_UPDATE_CONDITION))     'SQL構築(UPDATE文)

        'パラメータ
        With Me._ROW
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat(.Item("PATTERN_ID").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_ACTION_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
        End With
        Call Me.SetSysDateTime(Me._ROW, Me._SqlPrmList)

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール(データ抽出SQL 削除・復活）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDelRevSQL(ByVal ds As DataTable, ByVal updateKeyword As String)

        'SQL作成
        Me._StrSql.Append(String.Concat(LMQ010DAC.SQL_DELREV, LMQ010DAC.SQL_COM_UPDATE_CONDITION))     'SQL構築(UPDATE文)

        'パラメータ
        With Me._ROW
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PATTERN_ID", String.Concat(.Item("PATTERN_ID").ToString), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", String.Concat(MyBase.GetSystemDate), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", String.Concat(MyBase.GetSystemTime), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", String.Concat(MyBase.GetPGID), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat(MyBase.GetUserID), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", String.Concat(.Item("SYS_DEL_FLG").ToString), DBDataType.CHAR))
        End With
        Call Me.SetSysDateTime(Me._ROW, Me._SqlPrmList)

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
