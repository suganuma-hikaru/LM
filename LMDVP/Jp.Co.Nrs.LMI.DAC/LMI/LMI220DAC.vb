' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI220  : 定期検査管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI220DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI220DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' INデータテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI220IN"

    ''' <summary>
    ''' OUTデータテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI220OUT"

#Region "検索データ取得処理"

    Private Const SQL_SELECT_DATA As String = "SELECT                                             " & vbNewLine _
                                            & "   TEIKEN.NRS_BR_CD           AS NRS_BR_CD         " & vbNewLine _
                                            & "  ,TEIKEN.SERIAL_NO           AS SERIAL_NO         " & vbNewLine _
                                            & "  ,TEIKEN.SIZE                AS SIZE              " & vbNewLine _
                                            & "  ,TEIKEN.PROD_DATE           AS PROD_DATE         " & vbNewLine _
                                            & "  ,TEIKEN.LAST_TEST_DATE      AS LAST_TEST_DATE    " & vbNewLine _
                                            & "  ,TEIKEN.NEXT_TEST_DATE      AS NEXT_TEST_DATE    " & vbNewLine _
                                            & "  ,TEIKEN.SYS_ENT_DATE        AS SYS_ENT_DATE      " & vbNewLine _
                                            & "  ,TEIKEN.SYS_ENT_TIME        AS SYS_ENT_TIME      " & vbNewLine _
                                            & "  ,USER1.USER_NM              AS SYS_ENT_USER_NM   " & vbNewLine _
                                            & "  ,TEIKEN.SYS_UPD_DATE        AS SYS_UPD_DATE      " & vbNewLine _
                                            & "  ,TEIKEN.SYS_UPD_TIME        AS SYS_UPD_TIME      " & vbNewLine _
                                            & "  ,USER2.USER_NM              AS SYS_UPD_USER_NM   " & vbNewLine _
                                            & "  ,TEIKEN.SYS_DEL_FLG         AS SYS_DEL_FLG       " & vbNewLine _
                                            & "  ,KB1.KBN_NM1                AS SYS_DEL_NM        " & vbNewLine _
                                            & "  ,KB2.KBN_NM1                AS SIZE_NM           " & vbNewLine _
                                            & "FROM                                               " & vbNewLine _
                                            & "$LM_TRN$..I_HON_TEIKEN     TEIKEN                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN  KB1                     " & vbNewLine _
                                            & " ON KB1.KBN_GROUP_CD = 'S051'                      " & vbNewLine _
                                            & "AND KB1.KBN_CD       = TEIKEN.SYS_DEL_FLG          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN  KB2                     " & vbNewLine _
                                            & " ON KB2.KBN_GROUP_CD = 'S085'                      " & vbNewLine _
                                            & "AND KB2.KBN_CD       = TEIKEN.SIZE                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER USER1                   " & vbNewLine _
                                            & " ON TEIKEN.SYS_ENT_USER = USER1.USER_CD            " & vbNewLine _
                                            & "AND USER1.SYS_DEL_FLG   = '0'                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER USER2                   " & vbNewLine _
                                            & " ON TEIKEN.SYS_UPD_USER = USER2.USER_CD            " & vbNewLine _
                                            & "AND USER2.SYS_DEL_FLG   = '0'                      " & vbNewLine _
                                            & "WHERE TEIKEN.NRS_BR_CD  = @NRS_BR_CD               " & vbNewLine


#End Region

#Region "データ存在チェック"

    Private Const SQL_EXIST_DATA As String = "SELECT                                        " & vbNewLine _
                                           & "   TEIKEN.NRS_BR_CD           AS NRS_BR_CD    " & vbNewLine _
                                           & "  ,TEIKEN.SERIAL_NO           AS SERIAL_NO    " & vbNewLine _
                                           & "  ,TEIKEN.PROD_DATE           AS PROD_DATE    " & vbNewLine _
                                           & "FROM                                          " & vbNewLine _
                                           & "$LM_TRN$..I_HON_TEIKEN     TEIKEN             " & vbNewLine _
                                           & "WHERE  TEIKEN.NRS_BR_CD    = @NRS_BR_CD       " & vbNewLine _
                                           & "  AND  TEIKEN.SERIAL_NO    = @SERIAL_NO       " & vbNewLine _
                                           & "  AND  TEIKEN.SYS_DEL_FLG  = '0'              " & vbNewLine

#End Region

#Region "排他チェック"

    Private Const SQL_HAITA_DATA As String = "SELECT                                        " & vbNewLine _
                                           & "   TEIKEN.NRS_BR_CD           AS NRS_BR_CD    " & vbNewLine _
                                           & "  ,TEIKEN.SERIAL_NO           AS SERIAL_NO    " & vbNewLine _
                                           & "FROM                                          " & vbNewLine _
                                           & "$LM_TRN$..I_HON_TEIKEN     TEIKEN             " & vbNewLine _
                                           & "WHERE  TEIKEN.NRS_BR_CD    = @NRS_BR_CD       " & vbNewLine _
                                           & "  AND  TEIKEN.SERIAL_NO    = @SERIAL_NO       " & vbNewLine _
                                           & "  AND  TEIKEN.SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                           & "  AND  TEIKEN.SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "保存（新規登録）"

    Private Const SQL_INSERT_HON_TEIKEN As String = "INSERT INTO $LM_TRN$..I_HON_TEIKEN " & vbNewLine _
                                                  & "(                                  " & vbNewLine _
                                                  & "   NRS_BR_CD                       " & vbNewLine _
                                                  & "  ,SERIAL_NO                       " & vbNewLine _
                                                  & "  ,SIZE                            " & vbNewLine _
                                                  & "  ,PROD_DATE                       " & vbNewLine _
                                                  & "  ,LAST_TEST_DATE                  " & vbNewLine _
                                                  & "  ,NEXT_TEST_DATE                  " & vbNewLine _
                                                  & "  ,SYS_ENT_DATE                    " & vbNewLine _
                                                  & "  ,SYS_ENT_TIME                    " & vbNewLine _
                                                  & "  ,SYS_ENT_PGID                    " & vbNewLine _
                                                  & "  ,SYS_ENT_USER                    " & vbNewLine _
                                                  & "  ,SYS_UPD_DATE                    " & vbNewLine _
                                                  & "  ,SYS_UPD_TIME                    " & vbNewLine _
                                                  & "  ,SYS_UPD_PGID                    " & vbNewLine _
                                                  & "  ,SYS_UPD_USER                    " & vbNewLine _
                                                  & "  ,SYS_DEL_FLG                     " & vbNewLine _
                                                  & " )VALUES(                          " & vbNewLine _
                                                  & "   @NRS_BR_CD                      " & vbNewLine _
                                                  & "  ,@SERIAL_NO                      " & vbNewLine _
                                                  & "  ,@SIZE                           " & vbNewLine _
                                                  & "  ,@PROD_DATE                      " & vbNewLine _
                                                  & "  ,@LAST_TEST_DATE                 " & vbNewLine _
                                                  & "  ,@NEXT_TEST_DATE                 " & vbNewLine _
                                                  & "  ,@SYS_ENT_DATE                   " & vbNewLine _
                                                  & "  ,@SYS_ENT_TIME                   " & vbNewLine _
                                                  & "  ,@SYS_ENT_PGID                   " & vbNewLine _
                                                  & "  ,@SYS_ENT_USER                   " & vbNewLine _
                                                  & "  ,@SYS_UPD_DATE                   " & vbNewLine _
                                                  & "  ,@SYS_UPD_TIME                   " & vbNewLine _
                                                  & "  ,@SYS_UPD_PGID                   " & vbNewLine _
                                                  & "  ,@SYS_UPD_USER                   " & vbNewLine _
                                                  & "  ,@SYS_DEL_FLG                    " & vbNewLine _
                                                  & ")                                  " & vbNewLine


#End Region

#Region "保存（更新）"

    Private Const SQL_UPDATE_HON_TEIKEN As String = "UPDATE $LM_TRN$..I_HON_TEIKEN SET       " & vbNewLine _
                                                  & "   SIZE             = @SIZE             " & vbNewLine _
                                                  & "  ,PROD_DATE        = @PROD_DATE        " & vbNewLine _
                                                  & "  ,LAST_TEST_DATE   = @LAST_TEST_DATE   " & vbNewLine _
                                                  & "  ,NEXT_TEST_DATE   = @NEXT_TEST_DATE   " & vbNewLine _
                                                  & "  ,SYS_UPD_DATE     = @SYS_UPD_DATE     " & vbNewLine _
                                                  & "  ,SYS_UPD_TIME     = @SYS_UPD_TIME     " & vbNewLine _
                                                  & "  ,SYS_UPD_PGID     = @SYS_UPD_PGID     " & vbNewLine _
                                                  & "  ,SYS_UPD_USER     = @SYS_UPD_USER     " & vbNewLine _
                                                  & "  ,SYS_DEL_FLG      = '0'               " & vbNewLine _
                                                  & "WHERE  NRS_BR_CD    = @NRS_BR_CD        " & vbNewLine _
                                                  & "  AND  SERIAL_NO    = @SERIAL_NO        " & vbNewLine _
                                                  & "  AND  SYS_UPD_DATE = @GUI_SYS_UPD_DATE " & vbNewLine _
                                                  & "  AND  SYS_UPD_TIME = @GUI_SYS_UPD_TIME " & vbNewLine _
                                                  & "  AND  SYS_DEL_FLG  = '0'               " & vbNewLine


#End Region

#Region "削除・復活"

    Private Const SQL_UPDATE_DEL_FLG As String = "UPDATE $LM_TRN$..I_HON_TEIKEN SET       " & vbNewLine _
                                               & "   SYS_UPD_DATE     = @SYS_UPD_DATE     " & vbNewLine _
                                               & "  ,SYS_UPD_TIME     = @SYS_UPD_TIME     " & vbNewLine _
                                               & "  ,SYS_UPD_PGID     = @SYS_UPD_PGID     " & vbNewLine _
                                               & "  ,SYS_UPD_USER     = @SYS_UPD_USER     " & vbNewLine _
                                               & "  ,SYS_DEL_FLG      = @SYS_DEL_FLG      " & vbNewLine _
                                               & "WHERE  NRS_BR_CD    = @NRS_BR_CD        " & vbNewLine _
                                               & "  AND  SERIAL_NO    = @SERIAL_NO        " & vbNewLine _
                                               & "  AND  SYS_UPD_DATE = @GUI_SYS_UPD_DATE " & vbNewLine _
                                               & "  AND  SYS_UPD_TIME = @GUI_SYS_UPD_TIME " & vbNewLine _
                                               & "  AND  SYS_DEL_FLG  = @GUI_SYS_DEL_FLG  " & vbNewLine

#End Region

#Region "保存（更新）"

    Private Const SQL_UPDATE_IMPORT As String = "UPDATE $LM_TRN$..I_HON_TEIKEN SET       " & vbNewLine _
                                                  & "   LAST_TEST_DATE   = @LAST_TEST_DATE   " & vbNewLine _
                                                  & "  ,NEXT_TEST_DATE   = @NEXT_TEST_DATE   " & vbNewLine _
                                                  & "  ,SYS_UPD_DATE     = @SYS_UPD_DATE     " & vbNewLine _
                                                  & "  ,SYS_UPD_TIME     = @SYS_UPD_TIME     " & vbNewLine _
                                                  & "  ,SYS_UPD_PGID     = @SYS_UPD_PGID     " & vbNewLine _
                                                  & "  ,SYS_UPD_USER     = @SYS_UPD_USER     " & vbNewLine _
                                                  & "  ,SYS_DEL_FLG      = '0'               " & vbNewLine _
                                                  & "WHERE  NRS_BR_CD    = @NRS_BR_CD        " & vbNewLine _
                                                  & "  AND  SERIAL_NO    = @SERIAL_NO        " & vbNewLine _
                                                  & "  AND  SYS_DEL_FLG  = '0'               " & vbNewLine


#End Region

#End Region 'Const

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

    ' ********** 検索処理 **********

#Region "検索データ取得処理"

    ''' <summary>
    ''' 検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_SELECT_DATA)       'SQL構築(Select句)
        Call Me.SetSQLWhere(inTbl.Rows(0))                 'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append("ORDER BY TEIKEN.SERIAL_NO ")    'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI220DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI220OUTMap()
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_OUT)

        reader.Close()

        Return ds

    End Function

#End Region

#Region "データ存在チェック"

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsDataExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_EXIST_DATA)       'SQL構築(Select句)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(0).Item("SERIAL_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI220DAC", "IsDataExistChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()
        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("PROD_DATE", "PROD_DATE")       'ADD 2019/10/30 006785
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_OUT)

        reader.Close()

        Return ds

    End Function

#End Region

#Region "排他チェック"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_HAITA_DATA)       'SQL構築(Select句)

        'パラメータ設定
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(0).Item("SERIAL_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI220DAC", "HaitaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_OUT)

        reader.Close()

        If ds.Tables(TABLE_NM_OUT).Rows.Count = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

    ' ********** 保存処理 **********

#Region "保存（更新）"


    ''' <summary>
    ''' 保存（更新）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_UPDATE_HON_TEIKEN)       'SQL構築　

        'パラメータ設定
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSysdataTimeParameter(Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(0).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE", inTbl.Rows(0).Item("SIZE").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROD_DATE", inTbl.Rows(0).Item("PROD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_TEST_DATE", inTbl.Rows(0).Item("LAST_TEST_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEXT_TEST_DATE", inTbl.Rows(0).Item("NEXT_TEST_DATE").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI220DAC", "UpdateData", cmd)

        'SQLの発行
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "保存（新規登録）"

    ''' <summary>
    ''' 保存（新規登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_INSERT_HON_TEIKEN)       'SQL構築　

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(0).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE", inTbl.Rows(0).Item("SIZE").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROD_DATE", inTbl.Rows(0).Item("PROD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_TEST_DATE", inTbl.Rows(0).Item("LAST_TEST_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEXT_TEST_DATE", inTbl.Rows(0).Item("NEXT_TEST_DATE").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI220DAC", "InsertData", cmd)

        'SQLの発行
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "削除"

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_UPDATE_DEL_FLG)       'SQL構築　

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        For i As Integer = 0 To inTbl.Rows.Count - 1

            'SQLパラメータ初期化
            cmd.Parameters.Clear()
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetSysDateTime(inTbl.Rows(i), Me._SqlPrmList)
            Call Me.SetSysdataTimeParameter(Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(i).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(i).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI220DAC", "DeleteData", cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "復活"

    ''' <summary>
    ''' 復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ReviveData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI220DAC.SQL_UPDATE_DEL_FLG)       'SQL構築　

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        For i As Integer = 0 To inTbl.Rows.Count - 1

            'SQLパラメータ初期化
            cmd.Parameters.Clear()
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetSysDateTime(inTbl.Rows(i), Me._SqlPrmList)
            Call Me.SetSysdataTimeParameter(Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(i).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inTbl.Rows(i).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI220DAC", "ReviveData", cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "取込更新"


    ''' <summary>
    ''' 取込更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ImportData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        For Each inRow As DataRow In inTbl.Rows

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL作成
            Me._StrSql.Append(LMI220DAC.SQL_UPDATE_IMPORT)       'SQL構築　

            'パラメータ設定
            Call Me.SetSysDateTime(inRow, Me._SqlPrmList)
            Call Me.SetSysdataTimeParameter(Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inRow.Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_TEST_DATE", inRow.Item("LAST_TEST_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEXT_TEST_DATE", inRow.Item("NEXT_TEST_DATE").ToString(), DBDataType.CHAR))

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inRow.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI220DAC", "ImportData", cmd)

            'SQLの発行
            Call Me.UpdateResultChk(cmd)

        Next

        Return ds

    End Function

#End Region


#End Region 'SQLメイン処理

#Region "SQL条件設定"

    ' ********** 検索処理 **********

#Region "SQL条件設定 検索データ取得処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhere(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            '必須パラメータ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '状態
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.SYS_DEL_FLG = @SYS_DEL_FLG ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            'シリアル番号
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.SERIAL_NO = @SERIAL_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", whereStr, DBDataType.NVARCHAR))
            End If

            'サイズ
            whereStr = .Item("SIZE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.SIZE = @SIZE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE", whereStr, DBDataType.NVARCHAR))
            End If

            '点検開始日
            whereStr = .Item("PROD_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.PROD_DATE = @PROD_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROD_DATE", whereStr, DBDataType.CHAR))
            End If

            '前回点検日
            whereStr = .Item("LAST_TEST_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.LAST_TEST_DATE = @LAST_TEST_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_TEST_DATE", whereStr, DBDataType.CHAR))
            End If

            '次回点検日
            whereStr = .Item("NEXT_TEST_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TEIKEN.NEXT_TEST_DATE = @NEXT_TEST_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEXT_TEST_DATE", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

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

#End Region 'SQL条件設定

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索結果項目マッピング
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLMI220OUTMap() As Hashtable

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("SIZE_NM", "SIZE_NM")
        map.Add("PROD_DATE", "PROD_DATE")
        map.Add("LAST_TEST_DATE", "LAST_TEST_DATE")
        map.Add("NEXT_TEST_DATE", "NEXT_TEST_DATE")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("SIZE", "SIZE")

        Return map

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

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemUserID As String = MyBase.GetUserID()
        Dim systemPGID As String = MyBase.GetPGID()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))

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

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetSysdataTimeParameter(prmList)

    End Sub

#End Region

#End Region

End Class
