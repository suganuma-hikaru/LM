' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM350DAC : 初期出荷元マスタ
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM350DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM350DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(MAIN.JIS_CD)		   AS SELECT_CNT   " & vbNewLine _
                                             & " FROM (SELECT COUNT(JIS.JIS_CD)    AS JIS_CD       " & vbNewLine

    ''' <summary>
    ''' JIS_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
                                            & "	     JIS.JIS_CD                               AS JIS_CD       " & vbNewLine _
                                            & "	    ,JIS.KEN + JIS.SHI                        AS JIS_NM       " & vbNewLine _
                                            & "	    ,DSK.WH_CD                                AS WH_CD        " & vbNewLine _
                                            & "	    ,SOKO.WH_NM                               AS WH_NM        " & vbNewLine _
                                            & "	    ,JIS.KEN                                  AS KEN          " & vbNewLine _
                                            & "	    ,JIS.SHI                                  AS SHI          " & vbNewLine _
                                            & "	    ,CASE  WHEN DSK.WH_CD IS NULL THEN '0'                    " & vbNewLine _
                                            & "	           ELSE                        '1'                    " & vbNewLine _
                                            & "      END                                      AS UPD_FLG      " & vbNewLine _
                                            & "	    ,DSK.SYS_UPD_DATE                         AS SYS_UPD_DATE " & vbNewLine _
                                            & "	    ,DSK.SYS_UPD_TIME                         AS SYS_UPD_TIME " & vbNewLine

    Private Const SQL_GROUP_BY As String = "GROUP BY                                                   " & vbNewLine _
                                         & "     JIS.JIS_CD                                            " & vbNewLine _
                                         & "    ,DSK.WH_CD                                             " & vbNewLine _
                                         & "    ,SOKO.WH_NM                                            " & vbNewLine _
                                         & "    ,JIS.KEN                                               " & vbNewLine _
                                         & "    ,JIS.SHI                                               " & vbNewLine _
                                         & "    ,DSK.SYS_UPD_DATE                                      " & vbNewLine _
                                         & "    ,DSK.SYS_UPD_TIME                                      " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     DSK.WH_CD                                         " & vbNewLine _
                                         & "    ,JIS.JIS_CD                                        " & vbNewLine

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "(                    " & vbNewLine _
                                       & "      SCM_CUST_CD    " & vbNewLine _
                                       & "     ,JIS_CD         " & vbNewLine _
                                       & "     ,WH_CD          " & vbNewLine _
                                       & "     ,SYS_ENT_DATE   " & vbNewLine _
                                       & "     ,SYS_ENT_TIME   " & vbNewLine _
                                       & "     ,SYS_ENT_PGID   " & vbNewLine _
                                       & "     ,SYS_ENT_USER   " & vbNewLine _
                                       & "     ,SYS_UPD_DATE   " & vbNewLine _
                                       & "     ,SYS_UPD_TIME   " & vbNewLine _
                                       & "     ,SYS_UPD_PGID   " & vbNewLine _
                                       & "     ,SYS_UPD_USER   " & vbNewLine _
                                       & "     ,SYS_DEL_FLG    " & vbNewLine _
                                       & ") VALUES (           " & vbNewLine _
                                       & "      @SCM_CUST_CD   " & vbNewLine _
                                       & "     ,@JIS_CD        " & vbNewLine _
                                       & "     ,@WH_CD         " & vbNewLine _
                                       & "     ,@SYS_ENT_DATE  " & vbNewLine _
                                       & "     ,@SYS_ENT_TIME  " & vbNewLine _
                                       & "     ,@SYS_ENT_PGID  " & vbNewLine _
                                       & "     ,@SYS_ENT_USER  " & vbNewLine _
                                       & "     ,@SYS_UPD_DATE  " & vbNewLine _
                                       & "     ,@SYS_UPD_TIME  " & vbNewLine _
                                       & "     ,@SYS_UPD_PGID  " & vbNewLine _
                                       & "     ,@SYS_UPD_USER  " & vbNewLine _
                                       & "     ,@SYS_DEL_FLG   " & vbNewLine _
                                       & ")                    " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "          WH_CD        = @WH_CD           " & vbNewLine _
                                       & "         ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                       & "         ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                       & "         ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                       & "         ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                       & " WHERE                                    " & vbNewLine _
                                       & "         SCM_CUST_CD   = @SCM_CUST_CD     " & vbNewLine _
                                       & " AND     JIS_CD        = @JIS_CD          " & vbNewLine

#End Region

#End Region

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM350DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Call Me.SQLSelectFrom()                           'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM350DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(") MAIN")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM350DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM350DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Call Me.SQLSelectFrom()                           'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM350DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMM350DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM350DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JIS_CD", "JIS_CD")
        map.Add("JIS_NM", "JIS_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("KEN", "KEN")
        map.Add("SHI", "SHI")
        'map.Add("KU", "KU")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM350OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQLFROM句作成
    ''' </summary>
    ''' <remarks>初期出荷マスタ検索用SQLの構築</remarks>
    Private Sub SQLSelectFrom()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._StrSql.Append("FROM                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_JIS           JIS")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO  DSK")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  DSK.JIS_CD       = JIS.JIS_CD       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND DSK.SYS_DEL_FLG  = '0'              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_SOKO          SOKO")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  SOKO.WH_CD       = DSK.WH_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SOKO.SYS_DEL_FLG = '0'              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_ZIP           ZIP")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  ZIP.JIS_CD      = JIS.JIS_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZIP.SYS_DEL_FLG = '0'               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    JIS.SYS_DEL_FLG  = '0'              ")

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("SCM_CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (DSK.SCM_CUST_CD = @SCM_CUST_CD OR DSK.SCM_CUST_CD IS NULL)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ZIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZIP.ZIP_NO LIKE @ZIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND JIS.JIS_CD LIKE @JIS_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("JIS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND JIS.KEN + JIS.SHI LIKE @JIS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DSK.WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UN_LINK_FLG").ToString()
            Select Case whereStr
                Case LMConst.FLG.ON
                    Me._StrSql.Append(" AND DSK.SCM_CUST_CD IS NULL")
                    Me._StrSql.Append(vbNewLine)
            End Select

        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 初期出荷マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN_UPDATE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLSelectExit()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'Select結果保持用
        Dim selectCnt As Integer = 0

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamExistChk()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM350DAC", "CheckExistShokiShukkaM", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            selectCnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If selectCnt > 0 Then
                MyBase.SetResultCount(selectCnt)
                Exit For
            End If

        Next

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN_UPDATE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLSelectExit()
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE ")
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'Select結果保持用
        Dim selectCnt As Integer = 0

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamHaitaChk()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM350DAC", "SelectShokiShukkaM", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            selectCnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If selectCnt = 0 Then
                MyBase.SetResultCount(selectCnt)
                Exit For
            End If

        Next

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN_UPDATE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLInsert()
        Me._StrSql.Append(LMM350DAC.SQL_INSERT)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsert()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM350DAC", "InsertShokiShukkaM", cmd)

            'SQLの発行
            insCnt = insCnt + MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(insCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期出荷マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM350IN_UPDATE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLUpdate()
        Me._StrSql.Append(LMM350DAC.SQL_UPDATE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdate()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM350DAC", "UpdateShokiShukkaM", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#Region "SQL"

    ''' <summary>
    '''  初期出荷マスタ存在チェック用SQL作成
    ''' </summary>
    ''' <remarks>初期出荷マスタ検索用SQLの構築</remarks>
    Private Sub SQLSelectExit()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("   COUNT(JIS_CD)  AS REC_CNT   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("   SCM_CUST_CD  = @SCM_CUST_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND JIS_CD      = @JIS_CD      ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  初期出荷マスタ新規登録用SQL作成
    ''' </summary>
    ''' <remarks>初期出荷マスタ新規登録用SQLの構築</remarks>
    Private Sub SQLInsert()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._StrSql.Append("INSERT INTO                         ")
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  初期出荷マスタ更新処理用SQL作成
    ''' </summary>
    ''' <remarks>初期出荷マスタ更新処理用SQLの構築</remarks>
    Private Sub SQLUpdate()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._StrSql.Append("UPDATE ")
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO SET")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm()

        Dim brCd As String = Me._Row.Item("BR_CD").ToString()

        Me._MstSchemaNm = MyBase.GetDatabaseName(brCd, DBKbn.MST)

        If String.IsNullOrEmpty(Me._MstSchemaNm) = False Then
            Me._MstSchemaNm = String.Concat(Me._MstSchemaNm, "..")
        End If

    End Sub

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemIns()
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
