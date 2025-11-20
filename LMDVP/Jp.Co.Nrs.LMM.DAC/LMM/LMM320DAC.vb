' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM320DAC : 請求項目マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM320DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM320DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SEIQKMK.GROUP_KB)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' SEIQKMK_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
                                        & " SEIQKMK.PRINT_SORT                                           AS PRINT_SORT" & vbNewLine _
                                        & " ,SEIQKMK.GROUP_KB                                              AS GROUP_KB" & vbNewLine _
                                        & " ,KBN1.KBN_NM1                                               AS GROUP_KB_NM" & vbNewLine _
                                        & " ,SEIQKMK.SEIQKMK_CD                                          AS SEIQKMK_CD" & vbNewLine _
                                        & " ,SEIQKMK.SEIQKMK_CD_S                                      AS SEIQKMK_CD_S" & vbNewLine _
                                        & " ,SEIQKMK.SEIQKMK_NM                                          AS SEIQKMK_NM" & vbNewLine _
                                        & " ,SEIQKMK.TAX_KB                                                  AS TAX_KB" & vbNewLine _
                                        & " ,KBN2.KBN_NM1                                                 AS TAX_KB_NM" & vbNewLine _
                                        & " ,SEIQKMK.KEIRI_KB                                              AS KEIRI_KB" & vbNewLine _
                                        & " ,KBN3.KBN_NM1                                               AS KEIRI_KB_NM" & vbNewLine _
                                        & " ,SEIQKMK.REMARK                                                  AS REMARK" & vbNewLine _
                                        & " ,SEIQKMK.SYS_ENT_DATE                                      AS SYS_ENT_DATE" & vbNewLine _
                                        & " ,USER1.USER_NM                                          AS SYS_ENT_USER_NM" & vbNewLine _
                                        & " ,SEIQKMK.SYS_UPD_DATE                                      AS SYS_UPD_DATE" & vbNewLine _
                                        & " ,SEIQKMK.SYS_UPD_TIME                                      AS SYS_UPD_TIME" & vbNewLine _
                                        & " ,USER2.USER_NM                                          AS SYS_UPD_USER_NM" & vbNewLine _
                                        & " ,SEIQKMK.SYS_DEL_FLG                                        AS SYS_DEL_FLG" & vbNewLine _
                                        & " ,KBN4.KBN_NM1                                                AS SYS_DEL_NM" & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                               " & vbNewLine _
                                            & "$LM_MST$..M_SEIQKMK AS SEIQKMK                                   " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                      " & vbNewLine _
                                            & "ON SEIQKMK.GROUP_KB = KBN1.KBN_CD                                " & vbNewLine _
                                            & "AND KBN1.KBN_GROUP_CD='S024'                                     " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                      " & vbNewLine _
                                            & "ON SEIQKMK.TAX_KB = KBN2.KBN_CD                                  " & vbNewLine _
                                            & "AND KBN2.KBN_GROUP_CD='Z001'                                     " & vbNewLine _
                                            & "AND KBN2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3                      " & vbNewLine _
                                            & "ON SEIQKMK.KEIRI_KB = KBN3.KBN_CD                                " & vbNewLine _
                                            & "AND KBN3.KBN_GROUP_CD='K016'                                     " & vbNewLine _
                                            & "AND KBN3.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                     " & vbNewLine _
                                            & "ON SEIQKMK.SYS_ENT_USER = USER1.USER_CD                          " & vbNewLine _
                                            & "AND USER1.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                     " & vbNewLine _
                                            & "ON SEIQKMK.SYS_UPD_USER = USER2.USER_CD                          " & vbNewLine _
                                            & "AND USER2.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4                      " & vbNewLine _
                                            & "ON SEIQKMK.SYS_DEL_FLG = KBN4.KBN_CD                             " & vbNewLine _
                                            & "AND KBN4.KBN_GROUP_CD='S051'                                     " & vbNewLine _
                                            & "AND KBN4.SYS_DEL_FLG = '0'                                       " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     SEIQKMK.GROUP_KB                                  " & vbNewLine _
                                         & ",    SEIQKMK.SEIQKMK_CD                                " & vbNewLine _
                                         & ",    SEIQKMK.SEIQKMK_CD_S                              " & vbNewLine
#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_SEIQKMK As String = "SELECT                           " & vbNewLine _
                                        & "   COUNT(SEIQKMK_CD)  AS REC_CNT    " & vbNewLine _
                                        & "FROM $LM_MST$..M_SEIQKMK          " & vbNewLine _
                                        & "WHERE GROUP_KB     = @GROUP_KB    " & vbNewLine _
                                        & "  AND SEIQKMK_CD   = @SEIQKMK_CD  " & vbNewLine _
                                        & "  AND SEIQKMK_CD_S = @SEIQKMK_CD_S" & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SEIQKMK   " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      GROUP_KB                   	" & vbNewLine _
                                       & "      ,SEIQKMK_CD         		" & vbNewLine _
                                       & "      ,SEIQKMK_CD_S         		" & vbNewLine _
                                       & "      ,SEIQKMK_NM        		    " & vbNewLine _
                                       & "      ,TAX_KB                  	" & vbNewLine _
                                       & "      ,KEIRI_KB         		    " & vbNewLine _
                                       & "      ,REMARK         		    " & vbNewLine _
                                       & "      ,PRINT_SORT    			    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,SYS_ENT_PGID         		" & vbNewLine _
                                       & "      ,SYS_ENT_USER         		" & vbNewLine _
                                       & "      ,SYS_UPD_DATE         		" & vbNewLine _
                                       & "      ,SYS_UPD_TIME         		" & vbNewLine _
                                       & "      ,SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,SYS_DEL_FLG         		" & vbNewLine _
                                       & "      ) VALUES (                  " & vbNewLine _
                                       & "      @GROUP_KB                   " & vbNewLine _
                                       & "      ,@SEIQKMK_CD         		" & vbNewLine _
                                       & "      ,@SEIQKMK_CD_S         		" & vbNewLine _
                                       & "      ,@SEIQKMK_NM         		" & vbNewLine _
                                       & "      ,@TAX_KB                 	" & vbNewLine _
                                       & "      ,@KEIRI_KB         		    " & vbNewLine _
                                       & "      ,@REMARK         		    " & vbNewLine _
                                       & "      ,@PRINT_SORT   			    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         		" & vbNewLine _
                                       & "      ,@SYS_ENT_USER         		" & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         		" & vbNewLine _
                                       & "      ,@SYS_UPD_TIME         		" & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,@SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         		" & vbNewLine _
                                       & ")                                 " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SEIQKMK SET                         " & vbNewLine _
                                       & "        SEIQKMK_NM            = @SEIQKMK_NM           " & vbNewLine _
                                       & "       ,TAX_KB                = @TAX_KB               " & vbNewLine _
                                       & "       ,KEIRI_KB              = @KEIRI_KB             " & vbNewLine _
                                       & "       ,REMARK                = @REMARK               " & vbNewLine _
                                       & "       ,PRINT_SORT            = @PRINT_SORT           " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         GROUP_KB             = @GROUP_KB             " & vbNewLine _
                                       & " AND     SEIQKMK_CD           = @SEIQKMK_CD           " & vbNewLine _
                                       & " AND     SEIQKMK_CD_S         = @SEIQKMK_CD_S         " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SEIQKMK SET                        " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        GROUP_KB              = @GROUP_KB             " & vbNewLine _
                                       & " AND    SEIQKMK_CD            = @SEIQKMK_CD           " & vbNewLine _
                                       & " AND    SEIQKMK_CD_S          = @SEIQKMK_CD_S         " & vbNewLine
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
    ''' 請求項目マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM320DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM320DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 請求項目マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM320DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM320DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定(データ抽出用WHERE句)
        Me._StrSql.Append(LMM320DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("GROUP_KB_NM", "GROUP_KB_NM")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_CD_S", "SEIQKMK_CD_S")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("KEIRI_KB_NM", "KEIRI_KB_NM")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM320OUT")

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

            whereStr = .Item("PRINT_SORT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQKMK.PRINT_SORT LIKE @PRINT_SORT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", String.Concat(whereStr, "%")))
            End If

            whereStr = .Item("GROUP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQKMK.GROUP_KB = @GROUP_KB OR SEIQKMK.GROUP_KB IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQKMK_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQKMK.SEIQKMK_CD LIKE @SEIQKMK_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", String.Concat(whereStr, "%")))
            End If

            whereStr = .Item("SEIQKMK_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQKMK.SEIQKMK_CD_S = @SEIQKMK_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", whereStr))
            End If

            whereStr = .Item("SEIQKMK_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQKMK.SEIQKMK_NM LIKE @SEIQKMK_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_NM", String.Concat("%", whereStr, "%")))
            End If

            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQKMK.TAX_KB = @TAX_KB OR SEIQKMK.TAX_KB IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("KEIRI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQKMK.KEIRI_KB = @KEIRI_KB OR SEIQKMK.KEIRI_KB IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEIRI_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQKMK.REMARK LIKE @REMARK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%")))
            End If

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQKMK.SYS_DEL_FLG = @SYS_DEL_FLG OR SEIQKMK.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
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
    ''' 請求項目マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM320DAC.SQL_EXIT_SEIQKMK)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "SelectSeikyukoumokuM", cmd)

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
    ''' 請求項目マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM320DAC.SQL_EXIT_SEIQKMK,
                                                                       Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "CheckExistSeikyukoumokuM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader("REC_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If
        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' 請求項目マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM320DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "InsertSeikyukoumokuM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 請求項目マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM320DAC.SQL_UPDATE _
                                                                                     , LMM320DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "UpdateSeikyukoumokuM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 請求項目マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求項目マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM320DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM320DAC", "DeleteSeikyukoumokuM", cmd)

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
    ''' パラメータ設定モジュール(請求項目マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", .Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", .Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_NM", .Item("SEIQKMK_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEIRI_KB", .Item("KEIRI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", Me._Row.Item("GROUP_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", Me._Row.Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", Me._Row.Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))
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
