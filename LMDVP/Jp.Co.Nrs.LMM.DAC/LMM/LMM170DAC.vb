' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM170DAC : 郵便番号マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM170DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM170DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' 都道府県名データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KEN As String = " SELECT                                                               " & vbNewLine _
                                           & "	         KEN_N                     AS KEN_N                         " & vbNewLine _
                                           & " FROM                                                                 " & vbNewLine _
                                           & "	(                                                                   " & vbNewLine _
                                           & "		SELECT MIN(JIS_CD) AS JIS_CD                                    " & vbNewLine _
                                           & "	     ,KEN_N                                                         " & vbNewLine _
                                           & "	     FROM $LM_MST$..M_ZIP                                             " & vbNewLine _
                                           & "		 WHERE SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "	     GROUP BY  KEN_N                                                " & vbNewLine _
                                           & "	)                      AS ZIP                                       " & vbNewLine _
                                           & " ORDER BY JIS_CD                                                      " & vbNewLine

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZIP.ZIP_NO)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' M_ZIPデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                               " & vbNewLine _
                                            & "	      ZIP.ZIP_NO                    AS ZIP_NO                        " & vbNewLine _
                                            & "	     ,ZIP.JIS_CD                    AS JIS_CD                        " & vbNewLine _
                                            & "	     ,ZIP.KEN_K                     AS KEN_K                         " & vbNewLine _
                                            & "	     ,ZIP.CITY_K                    AS CITY_K                        " & vbNewLine _
                                            & "	     ,ZIP.TOWN_K                    AS TOWN_K                        " & vbNewLine _
                                            & "	     ,ZIP.KEN_N                     AS KEN_N                         " & vbNewLine _
                                            & "	     ,ZIP.CITY_N                    AS CITY_N                        " & vbNewLine _
                                            & "	     ,ZIP.TOWN_N                    AS TOWN_N                        " & vbNewLine _
                                            & "	     ,ZIP.SYS_ENT_DATE              AS SYS_ENT_DATE                  " & vbNewLine _
                                            & "	     ,USER1.USER_NM                 AS SYS_ENT_USER_NM	             " & vbNewLine _
                                            & "	     ,ZIP.SYS_UPD_DATE              AS SYS_UPD_DATE                  " & vbNewLine _
                                            & "	     ,ZIP.SYS_UPD_TIME              AS SYS_UPD_TIME                  " & vbNewLine _
                                            & "	     ,USER2.USER_NM                 AS SYS_UPD_USER_NM               " & vbNewLine _
                                            & "	     ,ZIP.SYS_DEL_FLG               AS SYS_DEL_FLG                   " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                  AS SYS_DEL_NM                    " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_ZIP AS ZIP                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER1          " & vbNewLine _
                                          & "        ON ZIP.SYS_ENT_USER                 = USER1.USER_CD  " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG                = '0'            " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER2          " & vbNewLine _
                                          & "        ON ZIP.SYS_UPD_USER                 = USER2.USER_CD  " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG                = '0'            " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN      AS KBN1           " & vbNewLine _
                                          & "        ON ZIP.SYS_DEL_FLG                  = KBN1.KBN_CD    " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                = 'S051'         " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                 = '0'            " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                   " & vbNewLine _
                                         & "    ZIP.ZIP_NO                                             " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 郵便番号コード必須チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HISSU_ZIP As String = "SELECT                                 " & vbNewLine _
                                            & "   COUNT(ZIP_NO)   AS REC_CNT        " & vbNewLine _
                                            & " FROM $LM_MST$..M_ZIP                " & vbNewLine _
                                            & " WHERE ZIP_NO       = @ZIP_NO        " & vbNewLine
                                            


#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_ZIP       " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "       ZIP_NO                     " & vbNewLine _
                                       & "      ,JIS_CD                     " & vbNewLine _
                                       & "      ,KEN_K                      " & vbNewLine _
                                       & "      ,CITY_K                     " & vbNewLine _
                                       & "      ,TOWN_K                     " & vbNewLine _
                                       & "      ,KEN_N 		                " & vbNewLine _
                                       & "      ,CITY_N		                " & vbNewLine _
                                       & "      ,TOWN_N	                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE               " & vbNewLine _
                                       & "      ,SYS_ENT_TIME               " & vbNewLine _
                                       & "      ,SYS_ENT_PGID               " & vbNewLine _
                                       & "      ,SYS_ENT_USER               " & vbNewLine _
                                       & "      ,SYS_UPD_DATE         	    " & vbNewLine _
                                       & "      ,SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,SYS_UPD_PGID               " & vbNewLine _
                                       & "      ,SYS_UPD_USER               " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                " & vbNewLine _
                                       & "      ) VALUES (                  " & vbNewLine _
                                       & "       @ZIP_NO                    " & vbNewLine _
                                       & "      ,@JIS_CD                    " & vbNewLine _
                                       & "      ,@KEN_K                     " & vbNewLine _
                                       & "      ,@CITY_K       	            " & vbNewLine _
                                       & "      ,@TOWN_K                    " & vbNewLine _
                                       & "      ,@KEN_N                     " & vbNewLine _
                                       & "      ,@CITY_N                    " & vbNewLine _
                                       & "      ,@TOWN_N                    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID        	    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER              " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME              " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER              " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	    " & vbNewLine _
                                       & ")                                 " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>     
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_ZIP SET                                    " & vbNewLine _
                                       & "        JIS_CD                = @JIS_CD                       " & vbNewLine _
                                       & "       ,KEN_K                 = @KEN_K                        " & vbNewLine _
                                       & "       ,CITY_K                = @CITY_K                       " & vbNewLine _
                                       & "       ,TOWN_K                = @TOWN_K                       " & vbNewLine _
                                       & "       ,KEN_N                 = @KEN_N                        " & vbNewLine _
                                       & "       ,CITY_N                = @CITY_N                       " & vbNewLine _
                                       & "       ,TOWN_N                = @TOWN_N                       " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE                 " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME                 " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID                 " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER                 " & vbNewLine _
                                       & " WHERE                                                        " & vbNewLine _
                                       & "        ZIP_NO                = @ZIP_NO                       " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_ZIP SET                    " & vbNewLine _
                                       & "        SYS_UPD_DATE  = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME  = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID  = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER  = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG   = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                        " & vbNewLine _
                                       & "        ZIP_NO        = @ZIP_NO               " & vbNewLine

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

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 郵便番号マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM170DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM170DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>都道府県名データ検索取得SQLの構築・発行</remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM170DAC.SQL_SELECT_KEN)      'SQL構築(都道府県名抽出用Select句)
        'Me._StrSql.Append(LMM170DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        'Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        ''パラメータの反映
        'For Each obj As Object In Me._SqlPrmList
        '    cmd.Parameters.Add(obj)
        'Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "SelectKenData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("KEN_N", "KEN_N")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM170KEN")

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM170DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM170DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM170DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("ZIP_NO", "ZIP_NO")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("KEN_K", "KEN_K")
        map.Add("CITY_K", "CITY_K")
        map.Add("TOWN_K", "TOWN_K")
        map.Add("KEN_N", "KEN_N")
        map.Add("CITY_N", "CITY_N")
        map.Add("TOWN_N", "TOWN_N")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM170OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (ZIP.SYS_DEL_FLG = @SYS_DEL_FLG  OR ZIP.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ZIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZIP.ZIP_NO LIKE @ZIP_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZIP.JIS_CD LIKE @JIS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            whereStr = .Item("KEN_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (ZIP.KEN_N = @KEN_N OR ZIP.KEN_N IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_N", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CITY_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZIP.CITY_N LIKE @CITY_N")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_N", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("TOWN_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZIP.TOWN_N LIKE @TOWN_N")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOWN_N", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 郵便番号マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistYubinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM170DAC.SQL_HISSU_ZIP, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "CheckExistYubinM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectYubinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM170DAC.SQL_HISSU_ZIP)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "SelectYubinM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertYubinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM170DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "InsertYubinM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateYubinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM170DAC.SQL_UPDATE _
                                                                                     , LMM170DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "UpdateYubinM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteYubinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM170IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM170DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM170DAC", "DeleteYubinM", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#Region "SQL"

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(郵便番号マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール（郵便番号最大値チェック）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCount()

        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        'With Me._Row
        '    'パラメータ設定
        '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOTAI_CD", .Item("JOTAI_CD").ToString(), DBDataType.CHAR))
        'End With

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

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()


    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_K", .Item("KEN_K").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_K", .Item("CITY_K").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOWN_K", .Item("TOWN_K").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_N", .Item("KEN_N").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_N", .Item("CITY_N").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOWN_N", .Item("TOWN_N").ToString(), DBDataType.NVARCHAR))

        End With

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", Me._Row.Item("ZIP_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
