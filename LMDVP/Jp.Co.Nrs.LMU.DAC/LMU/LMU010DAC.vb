' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010DAC : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

Public Class LMU010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "CONST"

    Private Const SQL_SELECT_DATALIST_FRM As String = " SELECT  MFL.FILE_NO                                                                 " & vbNewLine & _
                                                      "       , MFL.KEY_TYPE_KBN                                                            " & vbNewLine & _
                                                      "       , MFL.KEY_NO                                                                  " & vbNewLine & _
                                                      "       , MFL.KEY_NO_SEQ                                                              " & vbNewLine & _
                                                      "       , MFL.FILE_TYPE_KBN                                                           " & vbNewLine & _
                                                      "       , Z02.KBN_NM1         AS FILE_TYPE_NM                                         " & vbNewLine & _
                                                      "       , MFL.CONT_TYPE_KBN                                                           " & vbNewLine & _
                                                      "       , MFL.CONT_NO                                                                 " & vbNewLine & _
                                                      "       , MFL.REMARK                                                                  " & vbNewLine & _
                                                      "       , MFL.FILE_PATH                                                               " & vbNewLine & _
                                                      "       , MFL.FILE_NM                                                                 " & vbNewLine & _
                                                      "       , MFL.ENT_SYSID_KBN   AS ENT_SYSID_KBN                                        " & vbNewLine & _
                                                      "       , Z01.KBN_NM6         AS ENT_SYSID_NM                                         " & vbNewLine & _
                                                      "       , ''                  AS LOCAL_FULL_PATH                                      " & vbNewLine & _
                                                      "       , 1                   AS UPD_FLG                                              " & vbNewLine & _
                                                      "       , MFL.SYS_ENT_DATE                                                            " & vbNewLine & _
                                                      "       , MFL.SYS_ENT_TIME                                                            " & vbNewLine & _
                                                      "       , MFL.SYS_ENT_USER                                                            " & vbNewLine & _
                                                      "       , SUSER.USER_NM       AS SYS_ENT_USER_NM                                      " & vbNewLine & _
                                                      "       , MFL.SYS_UPD_DATE                                                            " & vbNewLine & _
                                                      "       , MFL.SYS_UPD_TIME                                                            " & vbNewLine & _
                                                      "       , MFL.SYS_UPD_USER                                                            " & vbNewLine & _
                                                      "       , MFL.SYS_DEL_FLG                                                             " & vbNewLine & _
                                                      "  FROM LM_MST..M_FILE MFL                                                            " & vbNewLine & _
                                                      "  LEFT JOIN COM_DB..Z_KBN Z01 ON (Z01.KBN_GROUP_CD = 'S072'                          " & vbNewLine & _
                                                      "                                  AND Z01.KBN_CD = MFL.ENT_SYSID_KBN                 " & vbNewLine & _
                                                      "                                  AND Z01.SYS_DEL_FLG = '0' )                        " & vbNewLine & _
                                                      "  LEFT JOIN COM_DB..Z_KBN Z02 ON (Z02.KBN_GROUP_CD = 'F014'                          " & vbNewLine & _
                                                      "                                  AND Z02.KBN_CD = MFL.FILE_TYPE_KBN                 " & vbNewLine & _
                                                      "                                  AND Z02.SYS_DEL_FLG = '0')                         " & vbNewLine & _
                                                      "  LEFT JOIN COM_DB..S_USER SUSER ON (SUSER.USER_ID = MFL.SYS_ENT_USER                " & vbNewLine & _
                                                      "                                     AND SUSER.SYS_DEL_FLG = '0')                    " & vbNewLine & _
                                                      "  WHERE MFL.SYS_DEL_FLG = '0'                                                        " & vbNewLine & _
                                                      "  --  AND MFL.KEY_TYPE_KBN = @KEY_TYPE_KBN                                           " & vbNewLine & _
                                                      "  --  AND MFL.KEY_NO = @KEY_NO                                                       " & vbNewLine

    Private Const SQL_SELECT_DATAINFO_SEL As String = "SELECT   FILE_NO                                                                     " & vbNewLine & _
                                                      "         , SYS_UPD_DATE                                                              " & vbNewLine & _
                                                      "         , SYS_UPD_TIME                                                              " & vbNewLine & _
                                                      "    FROM LM_MST..M_FILE                                                              " & vbNewLine & _
                                                      "   WHERE SYS_DEL_FLG = '0'                                                           " & vbNewLine & _
                                                      "     AND FILE_NO = @FILE_NO                                                          " & vbNewLine

    Private Const SQL_SELECT_SYSID_SELECT As String = "SELECT  KBN.KBN_CD                                                                   " & vbNewLine & _
                                                      "          , KBN.KBN_NM3                                                              " & vbNewLine & _
                                                      "      FROM  COM_DB..Z_KBN AS KBN                                                     " & vbNewLine & _
                                                      "     WHERE  KBN.KBN_GROUP_CD = 'S072'                                                " & vbNewLine

    Private Const SQL_SELECT_KANRITYPE_SELECT As String = "SELECT  KBN.KBN_CD                                                               " & vbNewLine & _
                                                    "          , KBN.KBN_NM1                                                                " & vbNewLine & _
                                                    "      FROM  COM_DB..Z_KBN AS KBN                                                       " & vbNewLine & _
                                                    "     WHERE  KBN.KBN_GROUP_CD = 'F012'                                                  " & vbNewLine

    Private Const SQL_SELECT_FILETYPE_SELECT As String = "SELECT  KBN.KBN_CD                                                               " & vbNewLine & _
                                                    "          , KBN.KBN_NM1                                                                " & vbNewLine & _
                                                    "      FROM  COM_DB..Z_KBN AS KBN                                                       " & vbNewLine & _
                                                    "     WHERE  KBN.KBN_GROUP_CD = 'F014'                                                  " & vbNewLine & _
                                                    "       AND  KBN.KBN_CD LIKE 'L%'                                                       " & vbNewLine

    Private Const SQL_UPDATE_WEB_ORDER As String = "  UPDATE $LM_TRN$..WK_WEB_ORDER_HED SET                                                 " & vbNewLine _
                                                & "     WEB_CHECK_KBN = '00'                                                                " & vbNewLine _
                                                & "    ,SYS_UPD_DATE = @SYS_UPD_DATE                                                        " & vbNewLine _
                                                & "    ,SYS_UPD_TIME = @SYS_UPD_TIME                                                        " & vbNewLine _
                                                & "    ,SYS_UPD_PGID = @SYS_UPD_PGID                                                        " & vbNewLine _
                                                & "    ,SYS_UPD_USER = @SYS_UPD_USER                                                        " & vbNewLine _
                                                & "  WHERE                                                                                  " & vbNewLine _
                                                & "    NRS_BR_CD = @NRS_BR_CD                                                               " & vbNewLine _
                                                & "    AND OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine

#End Region

#Region "Field"

    Private _row As Data.DataRow

    Private _strSql As StringBuilder

    Private _sqlPrmList As ArrayList

#End Region 'Field

#Region "Property"

    Private Property Row() As Data.DataRow
        Get
            Return _row
        End Get
        Set(ByVal value As Data.DataRow)
            _row = value
        End Set
    End Property

    Private Property StrSql() As StringBuilder
        Get
            Return _strSql
        End Get
        Set(ByVal value As StringBuilder)
            _strSql = value
        End Set
    End Property

    Private Property SqlPrmList() As ArrayList
        Get
            Return _sqlPrmList
        End Get
        Set(ByVal value As ArrayList)
            _sqlPrmList = value
        End Set
    End Property

#End Region 'Proprety

#Region "Method"

    ''' ====================================================================
    ''' <summary>データ一覧検索件数取得（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ一覧検索件数取得SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function SelectDataCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("LMU010IN").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築  
        '  (SELECT句)
        sbSql.Append("SELECT                              ").AppendLine()
        sbSql.Append("          COUNT(*)    AS REC_CNT    ").AppendLine()
        sbSql.Append("  FROM (                            ").AppendLine()
        'sbSql.Append("        SELECT   *                  ").AppendLine()

        '  (FROM句)
        sbSql.Append(SQL_SELECT_DATALIST_FRM & vbNewLine)

        '条件部の設定
        Call Me.SetConditionDataList(dr, sbSql, arrParam)

        sbSql.Append("       ) CNT                        ").AppendLine()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "SelectDataCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = Me.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Me.SetResultCount(CInt(reader("REC_CNT")))

        reader.Close()

        Return ds

    End Function

    ''' ====================================================================
    ''' <summary>データ一覧検索結果取得（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ一覧検索結果取得SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function SelectDataList(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("LMU010IN").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築
        '  (SELECT句)
        sbSql.Append(SQL_SELECT_DATALIST_FRM & vbNewLine)

        '条件部の設定
        Call Me.SetConditionDataList(dr, sbSql, arrParam)

        '並び順
        sbSql.Append("ORDER BY MFL.FILE_TYPE_KBN ,MFL.SYS_ENT_DATE DESC" & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "SelectDataList", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = Me.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable

        '取得データの格納先をマッピング
        map.Add("FILE_NO", "FILE_NO")
        map.Add("KEY_TYPE_KBN", "KEY_TYPE_KBN")
        map.Add("KEY_NO", "KEY_NO")
        map.Add("FILE_TYPE_KBN", "FILE_TYPE_KBN")
        map.Add("FILE_TYPE_NM", "FILE_TYPE_NM")
        map.Add("CONT_TYPE_KBN", "CONT_TYPE_KBN")
        map.Add("CONT_NO", "CONT_NO")
        map.Add("REMARK", "REMARK")
        map.Add("FILE_PATH", "FILE_PATH")
        map.Add("FILE_NM", "FILE_NM")
        map.Add("ENT_SYSID_KBN", "ENT_SYSID_KBN")
        map.Add("ENT_SYSID_NM", "ENT_SYSID_NM")
        map.Add("LOCAL_FULL_PATH", "LOCAL_FULL_PATH")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
      

        ds = Me.SetSelectResultToDataSet(map, ds, reader, "M_FILE")

        Return ds

    End Function

    ''' <summary>
    ''' コンボ作成データ(システムID)取得　2015/1/6 大野ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataSysID(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Me._strSql.Append(LMU010DAC.SQL_SELECT_SYSID_SELECT)      'SQL構築(データ抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM3", "KBN_NM3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMU010_COMBO")

        Return ds

    End Function
    ''' <summary>
    ''' コンボ作成データ(管理タイプ)取得　2015/1/6 大野ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataKanriType(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Me._strSql.Append(LMU010DAC.SQL_SELECT_KANRITYPE_SELECT)      'SQL構築(データ抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_KCD", "KBN_CD")
        map.Add("KBN_KNM1", "KBN_NM1")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMU010_KANRITYPE")

        Return ds

    End Function
    ''' <summary>
    ''' コンボ作成データ(ファイルタイプ)取得　2015/1/6 大野ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataFileType(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Me._strSql.Append(LMU010DAC.SQL_SELECT_FILETYPE_SELECT)      'SQL構築(データ抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_FCD", "KBN_CD")
        map.Add("KBN_FNM1", "KBN_NM1")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMU010_FILETYPE")

        Return ds

    End Function

    ''' ====================================================================
    ''' <summary>排他チェック用データ情報検索結果取得（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>排他チェック用データ情報検索結果取得SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function SelectDataListForChkHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("M_FILE").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築
        '  (SELECT～FROM句)
        sbSql.Append(SQL_SELECT_DATAINFO_SEL & vbNewLine)

        arrParam.Add(GetSqlParameter("@FILE_NO", dr.Item("FILE_NO"), DBDataType.NUMERIC))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "SelectDataListForChkHaita", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = Me.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable

        '取得データの格納先をマッピング
        map.Add("FILE_NO", "FILE_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = Me.SetSelectResultToDataSet(map, ds, reader, "M_FILE")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="conditionRow">条件設定行</param>
    ''' <param name="editSql">編集SQL格納変数</param>
    ''' <param name="prmList">SQLパラメータ格納配列</param>
    ''' <remarks></remarks>
    Private Sub SetConditionDataList(ByVal conditionRow As Data.DataRow, ByVal editSql As StringBuilder, ByVal prmList As ArrayList)

        'SYSTEM ID区分(完全一致)
        If conditionRow("ENT_SYSID_KBN").Equals("") = False Then
            editSql.Append("AND  MFL.ENT_SYSID_KBN = @ENT_SYSID_KBN " & vbNewLine)
            prmList.Add(GetSqlParameter("@ENT_SYSID_KBN", conditionRow("ENT_SYSID_KBN"), DBDataType.CHAR))
        End If

        'キータイプ区分(完全一致)
        If conditionRow("KEY_TYPE_KBN").Equals("") = False Then
            editSql.Append("AND  MFL.KEY_TYPE_KBN = @KEY_TYPE_KBN " & vbNewLine)
            prmList.Add(GetSqlParameter("@KEY_TYPE_KBN", conditionRow("KEY_TYPE_KBN"), DBDataType.CHAR))
        End If

        'キー番号(完全一致)
        If conditionRow("KEY_NO").Equals("") = False Then
            editSql.Append("AND  MFL.KEY_NO = @KEY_NO " & vbNewLine)
            prmList.Add(GetSqlParameter("@KEY_NO", conditionRow("KEY_NO"), DBDataType.NVARCHAR))
        End If


    End Sub



    ''' ====================================================================
    ''' <summary>新規登録処理（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>新規登録処理SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("M_FILE").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築
        '  (Update句)
        sbSql.Append("INSERT INTO LM_MST..M_FILE               ")
        sbSql.Append("              (                  ")
        sbSql.Append("               KEY_TYPE_KBN,     ")
        sbSql.Append("               KEY_NO,           ")
        sbSql.Append("               KEY_NO_SEQ,       ")
        sbSql.Append("               FILE_TYPE_KBN,    ")
        sbSql.Append("               CONT_TYPE_KBN,    ")
        sbSql.Append("               CONT_NO,          ")
        sbSql.Append("               REMARK,           ")
        sbSql.Append("               FILE_PATH,        ")
        sbSql.Append("               FILE_NM,          ")
        sbSql.Append("               ENT_SYSID_KBN,    ")
        sbSql.Append("               SYS_ENT_DATE,     ")
        sbSql.Append("               SYS_ENT_TIME,     ")
        sbSql.Append("               SYS_ENT_PGID,     ")
        sbSql.Append("               SYS_ENT_USER,     ")
        sbSql.Append("               SYS_UPD_DATE,     ")
        sbSql.Append("               SYS_UPD_TIME,     ")
        sbSql.Append("               SYS_UPD_PGID,     ")
        sbSql.Append("               SYS_UPD_USER,     ")
        sbSql.Append("               SYS_DEL_FLG       ")
        sbSql.Append("                             )   ")
        sbSql.Append("       VALUES (                  ")
        sbSql.Append("               @KEY_TYPE_KBN,    ")
        sbSql.Append("               @KEY_NO,          ")
        sbSql.Append("               @KEY_NO_SEQ,      ")
        sbSql.Append("               @FILE_TYPE_KBN,   ")
        sbSql.Append("               @CONT_TYPE_KBN,   ")
        sbSql.Append("               @CONT_NO,         ")
        sbSql.Append("               @REMARK,          ")
        sbSql.Append("               @FILE_PATH,       ")
        sbSql.Append("               @FILE_NM,         ")
        sbSql.Append("               @ENT_SYSID_KBN,   ")
        sbSql.Append("               @SYS_ENT_DATE,    ")
        sbSql.Append("               @SYS_ENT_TIME,    ")
        sbSql.Append("               @SYS_ENT_PGID,    ")
        sbSql.Append("               @SYS_ENT_USER,    ")
        sbSql.Append("               @SYS_UPD_DATE,    ")
        sbSql.Append("               @SYS_UPD_TIME,    ")
        sbSql.Append("               @SYS_UPD_PGID,    ")
        sbSql.Append("               @SYS_UPD_USER,    ")
        sbSql.Append("               @SYS_DEL_FLG      ")
        sbSql.Append("                             )   ")

        arrParam.Add(GetSqlParameter("@KEY_TYPE_KBN", dr.Item("KEY_TYPE_KBN").ToString, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@KEY_NO", dr.Item("KEY_NO").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@KEY_NO_SEQ", dr.Item("KEY_NO_SEQ").ToString, DBDataType.NUMERIC))
        arrParam.Add(GetSqlParameter("@FILE_TYPE_KBN", dr.Item("FILE_TYPE_KBN").ToString, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@CONT_TYPE_KBN", dr.Item("CONT_TYPE_KBN").ToString, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@CONT_NO", dr.Item("CONT_NO").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@REMARK", dr.Item("REMARK").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@FILE_PATH", dr.Item("FILE_PATH").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@FILE_NM", dr.Item("FILE_NM").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@ENT_SYSID_KBN", dr.Item("ENT_SYSID_KBN").ToString, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_ENT_USER", Me.GetUserID, DBDataType.VARCHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID, DBDataType.VARCHAR))
        arrParam.Add(GetSqlParameter("@SYS_DEL_FLG", dr.Item("SYS_DEL_FLG").ToString, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "InsertData", cmd)

        'SQLの発行
        Dim rtn As Integer = Me.GetInsertResult(cmd)

        Return ds

    End Function

    ''' ====================================================================
    ''' <summary>更新処理（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>更新処理SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("M_FILE").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築
        '  (Update句)
        sbSql.Append("UPDATE LM_MST..M_FILE SET   FILE_TYPE_KBN = @FILE_TYPE_KBN          ")
        sbSql.Append("                  , REMARK = @REMARK                ")
        sbSql.Append("                  , SYS_UPD_DATE = @SYS_UPD_DATE    ")
        sbSql.Append("                  , SYS_UPD_TIME = @SYS_UPD_TIME    ")
        sbSql.Append("                  , SYS_UPD_PGID = @SYS_UPD_PGID    ")
        sbSql.Append("                  , SYS_UPD_USER = @SYS_UPD_USER    ")
        sbSql.Append("              WHERE FILE_NO = @FILE_NO              ")
        sbSql.Append("                AND SYS_DEL_FLG = '0'               ")

        arrParam.Add(GetSqlParameter("@FILE_TYPE_KBN", dr.Item("FILE_TYPE_KBN").ToString, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@REMARK", dr.Item("REMARK").ToString, DBDataType.NVARCHAR))
        arrParam.Add(GetSqlParameter("@FILE_NO", dr.Item("FILE_NO"), DBDataType.NUMERIC))
        arrParam.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID, DBDataType.VARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "UpdateData", cmd)

        'SQLの発行
        Dim rtn As Integer = Me.GetUpdateResult(cmd)

        Return ds

    End Function
    ''' ====================================================================
    ''' <summary>更新処理（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>更新処理SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function UpdateWebData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_FILE")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL構築
        Me._strSql.Append(LMU010DAC.SQL_UPDATE_WEB_ORDER)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._strSql.ToString(), Me._row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        With Me._row
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("KEY_NO").ToString(), DBDataType.VARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMU010DAC", "UpdateWebData", cmd)

        'SQLの発行
        Dim rtn As Integer = Me.GetUpdateResult(cmd)

        Return ds

    End Function
    ''' ====================================================================
    ''' <summary>論理削除処理（DAC）</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>論理削除SQLの構築・発行</remarks>
    ''' ====================================================================
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("M_FILE").Rows(0)
        Dim sbSql As StringBuilder = New StringBuilder
        Dim arrParam As ArrayList = New ArrayList

        'SQL構築
        '  (Update句)
        sbSql.Append("UPDATE LM_MST..M_FILE SET   SYS_DEL_FLG = '1'               ")
        sbSql.Append("                  , SYS_UPD_DATE = @SYS_UPD_DATE    ")
        sbSql.Append("                  , SYS_UPD_TIME = @SYS_UPD_TIME    ")
        sbSql.Append("                  , SYS_UPD_PGID = @SYS_UPD_PGID    ")
        sbSql.Append("                  , SYS_UPD_USER = @SYS_UPD_USER    ")
        sbSql.Append("              WHERE FILE_NO = @FILE_NO              ")
        sbSql.Append("                AND SYS_DEL_FLG = '0'               ")

        arrParam.Add(GetSqlParameter("@FILE_NO", dr.Item("FILE_NO"), DBDataType.NUMERIC))
        arrParam.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID, DBDataType.CHAR))
        arrParam.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID, DBDataType.VARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sbSql.ToString)

        'パラメータの反映
        For Each obj As Object In arrParam
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMU010DAC", "DeleteData", cmd)

        'SQLの発行
        Dim rtn As Integer = Me.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region 'Method

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <param name="sverFlg">サーバー切り替え有無フラグTrue:有り</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 , Optional ByVal sverFlg As Boolean = False) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

End Class
