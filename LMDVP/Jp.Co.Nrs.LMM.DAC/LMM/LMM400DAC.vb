' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM400DAC : 西濃着点マスタメンテ
'  作  成  者       :  adachi
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM400DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM400DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用 前
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_START As String = " SELECT COUNT(*)      AS SELECT_CNT   " & vbNewLine _
                                                & "      FROM(            " & vbNewLine

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_END As String = " ) CNT   " & vbNewLine _
                                                & "                  " & vbNewLine

    ''' <summary>
    ''' カウント用 select count + from 
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(*)      AS SELECT_CNT   " & vbNewLine _
                                             & "      FROM            " & vbNewLine _
                                             & "    $LM_MST$..M_SEINO MS " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST MC " & vbNewLine _
                                             & "   ON MC.NRS_BR_CD = MS.NRS_BR_CD " & vbNewLine _
                                             & "   AND MC.CUST_CD_L = MS.CUST_CD_L " & vbNewLine _
                                             & "   AND MC.CUST_CD_M = MS.CUST_CD_M " & vbNewLine _
                                             & "   AND MC.SYS_DEL_FLG = '0' " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..Z_KBN DF " & vbNewLine _
                                             & "   ON DF.KBN_CD = MS.SYS_DEL_FLG " & vbNewLine _
                                             & "   AND DF.KBN_GROUP_CD = 'S051' " & vbNewLine _
                                             & "   AND DF.SYS_DEL_FLG = '0' " & vbNewLine



    ''' <summary>
    ''' 西濃マスタデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                 " & vbNewLine _
                                             & "       MS.SEINO_KEY AS SEINO_KEY            " & vbNewLine _
                                             & "       ,MS.NRS_BR_CD AS NRS_BR_CD             " & vbNewLine _
                                             & "       ,MN.NRS_BR_NM AS NRS_BR_NM             " & vbNewLine _
                                             & "       ,MS.CUST_CD_L AS CUST_CD_L             " & vbNewLine _
                                             & "       ,MS.CUST_CD_M AS CUST_CD_M            " & vbNewLine _
                                             & " 	  ,MC.CUST_NM_L AS CUST_NM_L            " & vbNewLine _
                                             & " 	  ,MC.CUST_NM_M AS CUST_NM_M            " & vbNewLine _
                                             & "       ,MS.ZIP_NO AS ZIP_NO            " & vbNewLine _
                                             & "       ,MS.SHIWAKE_CD AS SHIWAKE_CD            " & vbNewLine _
                                             & "       ,MS.CHAKU_CD AS CHAKU_CD                       " & vbNewLine _
                                             & "       ,MS.CHAKU_NM AS CHAKU_NM            " & vbNewLine _
                                             & "       ,MS.KEN_K AS KEN_K            " & vbNewLine _
                                             & "       ,MS.CITY_K AS CITY_K            " & vbNewLine _
                                             & "       ,MS.SYS_ENT_DATE AS SYS_ENT_DATE            " & vbNewLine _
                                             & "       ,MS.SYS_ENT_TIME AS SYS_ENT_TIME            " & vbNewLine _
                                             & "       ,MS.SYS_ENT_PGID AS SYS_ENT_PGID            " & vbNewLine _
                                             & "       ,USER1.USER_NM   AS SYS_ENT_USER_NM          " & vbNewLine _
                                             & "       ,MS.SYS_UPD_DATE AS SYS_UPD_DATE            " & vbNewLine _
                                             & "       ,MS.SYS_UPD_TIME AS SYS_UPD_TIME            " & vbNewLine _
                                             & "       ,MS.SYS_UPD_PGID AS SYS_UPD_PGID            " & vbNewLine _
                                             & "       ,USER2.USER_NM   AS SYS_UPD_USER_NM          " & vbNewLine _
                                             & "       ,MS.SYS_DEL_FLG AS SYS_DEL_FLG            " & vbNewLine _
                                             & "       ,DF.KBN_NM1 AS SYS_DEL_NM            " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                 " & vbNewLine _
                                          & "    $LM_MST$..M_SEINO MS " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_NRS_BR MN " & vbNewLine _
                                             & "   ON MN.NRS_BR_CD = MS.NRS_BR_CD " & vbNewLine _
                                             & "   AND MN.SYS_DEL_FLG = '0' " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST MC " & vbNewLine _
                                             & "   ON MC.NRS_BR_CD = MS.NRS_BR_CD " & vbNewLine _
                                             & "   AND MC.CUST_CD_L = MS.CUST_CD_L " & vbNewLine _
                                             & "   AND MC.CUST_CD_M = MS.CUST_CD_M " & vbNewLine _
                                             & "   AND MC.SYS_DEL_FLG = '0' " & vbNewLine _
                                             & "   LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                   " & vbNewLine _
                                             & "     ON MS.SYS_ENT_USER = USER1.USER_CD                         " & vbNewLine _
                                             & "    AND USER1.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                             & "   LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                   " & vbNewLine _
                                             & "    ON  MS.SYS_UPD_USER  = USER2.USER_CD                        " & vbNewLine _
                                             & "    AND USER2.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..Z_KBN DF " & vbNewLine _
                                             & "   ON DF.KBN_CD = MS.SYS_DEL_FLG " & vbNewLine _
                                             & "   AND DF.KBN_GROUP_CD = 'S051' " & vbNewLine _
                                             & "   AND DF.SYS_DEL_FLG = '0' " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                            " & vbNewLine _
                                         & "     MS.NRS_BR_CD , MS.CUST_CD_L,MS.CUST_CD_M,MS.ZIP_NO                                    " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

     ''' <summary>
     ''' 排他チェック用 20150317
     ''' </summary>
     ''' <remarks></remarks>
    Private Const SQL_EXIST_SEINO_HAITA As String = "SELECT                                       " & vbNewLine _
                                            & "      COUNT(MS.SEINO_KEY)  AS REC_CNT        " & vbNewLine _
                                            & " FROM $LM_MST$..M_SEINO AS MS             " & vbNewLine _
                                            & "WHERE MS.SEINO_KEY    = @SEINO_KEY                    " & vbNewLine

    ''' <summary>
    ''' 登録済みチェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_SEINO_TOROKUZUMI As String = "SELECT                                       " & vbNewLine _
                                            & "      COUNT(MS.SEINO_KEY)  AS REC_CNT        " & vbNewLine _
                                            & " FROM $LM_MST$..M_SEINO AS MS             " & vbNewLine _
                                            & "WHERE MS.NRS_BR_CD    = @NRS_BR_CD                    " & vbNewLine _
                                            & "AND MS.CUST_CD_L    = @CUST_CD_L                    " & vbNewLine _
                                            & "AND MS.CUST_CD_M    = @CUST_CD_M                    " & vbNewLine _
                                            & "AND MS.ZIP_NO    = @ZIP_NO                    " & vbNewLine _
                                            & "AND MS.SEINO_KEY   <> @SEINO_KEY                    " & vbNewLine _
                                            & "AND MS.SYS_DEL_FLG    = '0'                    " & vbNewLine



#End Region


#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                       " & vbNewLine _
                                             & "   MS.SEINO_KEY " & vbNewLine _
                                             & "       ,MS.NRS_BR_CD " & vbNewLine _
                                             & "       ,MN.NRS_BR_NM " & vbNewLine _
                                             & "       ,MS.CUST_CD_L " & vbNewLine _
                                             & "       ,MS.CUST_CD_M " & vbNewLine _
                                             & " 	  ,MC.CUST_NM_L  " & vbNewLine _
                                             & " 	  ,MC.CUST_NM_M  " & vbNewLine _
                                             & "       ,MS.ZIP_NO  " & vbNewLine _
                                             & "       ,MS.SHIWAKE_CD " & vbNewLine _
                                             & "       ,MS.CHAKU_CD  " & vbNewLine _
                                             & "       ,MS.CHAKU_NM  " & vbNewLine _
                                             & "       ,MS.KEN_K  " & vbNewLine _
                                              & "       ,MS.CITY_K " & vbNewLine _
                                             & "       ,MS.SYS_ENT_DATE " & vbNewLine _
                                             & "       ,MS.SYS_ENT_TIME " & vbNewLine _
                                             & "       ,MS.SYS_ENT_PGID " & vbNewLine _
                                             & "       ,USER1.USER_NM " & vbNewLine _
                                             & "       ,MS.SYS_UPD_DATE " & vbNewLine _
                                             & "       ,MS.SYS_UPD_TIME " & vbNewLine _
                                             & "       ,MS.SYS_UPD_PGID " & vbNewLine _
                                             & "       ,USER2.USER_NM " & vbNewLine _
                                             & "       ,MS.SYS_DEL_FLG  " & vbNewLine _
                                             & "       ,DF.KBN_NM1  " & vbNewLine




#End Region

#End Region

#Region "設定処理 SQL"


    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SEINO                 " & vbNewLine _
                                       & "(                                            " & vbNewLine _
                                       & "       NRS_BR_CD                             " & vbNewLine _
                                       & "      ,CUST_CD_L                                 " & vbNewLine _
                                       & "      ,CUST_CD_M                                 " & vbNewLine _
                                       & "      ,ZIP_NO                                   " & vbNewLine _
                                       & "      ,SHIWAKE_CD                                  " & vbNewLine _
                                       & "      ,CHAKU_CD                                  " & vbNewLine _
                                       & "      ,CHAKU_NM                                  " & vbNewLine _
                                       & "      ,KEN_K                                 " & vbNewLine _
                                       & "      ,CITY_K                                   " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                          " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                          " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                          " & vbNewLine _
                                       & "      ,SYS_ENT_USER                          " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                          " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                          " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                          " & vbNewLine _
                                       & "      ,SYS_UPD_USER                          " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                           " & vbNewLine _
                                       & "      ) VALUES (                             " & vbNewLine _
                                       & "       @NRS_BR_CD                            " & vbNewLine _
                                       & "      ,@CUST_CD_L                                " & vbNewLine _
                                       & "      ,@CUST_CD_M                                " & vbNewLine _
                                       & "      ,@ZIP_NO                                  " & vbNewLine _
                                       & "      ,@SHIWAKE_CD                                 " & vbNewLine _
                                       & "      ,@CHAKU_CD                                 " & vbNewLine _
                                       & "      ,@CHAKU_NM                                 " & vbNewLine _
                                       & "      ,@KEN_K                                " & vbNewLine _
                                       & "      ,@CITY_K                                  " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                         " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                         " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                         " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                         " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                         " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                         " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                         " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                         " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                          " & vbNewLine _
                                       & ")                                            " & vbNewLine



    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SEINO SET                                 " & vbNewLine _
                                       & "        NRS_BR_CD              = @NRS_BR_CD                 " & vbNewLine _
                                       & "       ,CUST_CD_L              = @CUST_CD_L                 " & vbNewLine _
                                       & "       ,CUST_CD_M              = @CUST_CD_M                 " & vbNewLine _
                                       & "       ,ZIP_NO                 = @ZIP_NO                 " & vbNewLine _
                                       & "       ,SHIWAKE_CD             = @SHIWAKE_CD                " & vbNewLine _
                                       & "       ,CHAKU_CD               = @CHAKU_CD                  " & vbNewLine _
                                       & "       ,CHAKU_NM               = @CHAKU_NM                  " & vbNewLine _
                                       & "       ,KEN_K                  = @KEN_K                      " & vbNewLine _
                                       & "       ,CITY_K                 = @CITY_K                      " & vbNewLine _
                                       & "       ,SYS_UPD_DATE           = @SYS_UPD_DATE              " & vbNewLine _
                                       & "       ,SYS_UPD_TIME           = @SYS_UPD_TIME              " & vbNewLine _
                                       & "       ,SYS_UPD_PGID           = @SYS_UPD_PGID              " & vbNewLine _
                                       & "       ,SYS_UPD_USER           = @SYS_UPD_USER              " & vbNewLine _
                                       & " WHERE                                                      " & vbNewLine _
                                       & "         SEINO_KEY                 = @SEINO_KEY                     " & vbNewLine



    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SEINO SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & "WHERE                SEINO_KEY    = @SEINO_KEY                " & vbNewLine

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
    ''' 西濃着点マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>西濃着点マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM400DAC.SQL_SELECT_COUNT) 'SQL構築(カウント用Select + from 句)
        Call Me.SetConditionMasterSQL()                   '条件設定



        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>西濃マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM400DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM400DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM400DAC.SQL_GROUP_BY)         'GROUP BY
        Me._StrSql.Append(LMM400DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEINO_KEY", "SEINO_KEY")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("ZIP_NO", "ZIP_NO")
        map.Add("SHIWAKE_CD", "SHIWAKE_CD")
        map.Add("CHAKU_CD", "CHAKU_CD")
        map.Add("CHAKU_NM", "CHAKU_NM")
        map.Add("KEN_K", "KEN_K")
        map.Add("CITY_K", "CITY_K")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM400OUT")

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
                andstr.Append(" MS.SYS_DEL_FLG = @SYS_DEL_FLG  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.NRS_BR_CD = @NRS_BR_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.CUST_CD_L = @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.CUST_CD_M = @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MC.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MC.CUST_NM_M LIKE @CUST_NM_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("ZIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.ZIP_NO LIKE @ZIP_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SHIWAKE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.SHIWAKE_CD LIKE @SHIWAKE_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIWAKE_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CHAKU_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.CHAKU_CD LIKE @CHAKU_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHAKU_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CHAKU_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.CHAKU_NM LIKE @CHAKU_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHAKU_NM", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("KEN_K").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.KEN_K LIKE @KEN_K")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_K", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CITY_K").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MS.CITY_K LIKE @CITY_K")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_K", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
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
    ''' 西濃マスタ排他チェック 
    '''
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSeinoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM400DAC.SQL_EXIST_SEINO_HAITA _
                                                                                     , LMM400DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                 , Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "CheckHaitaSeinoM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        '1件ＨＩＴしなかったらエラー
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ存在チェック　拠点コード・CUST_CD・郵便番号
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSeinoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化

        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '条件設定
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM400DAC.SQL_EXIST_SEINO_TOROKUZUMI _
                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "CheckExs", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)



        cmd.Parameters.Clear()

        '処理件数の設定
        '削除の場合は存在チェック必要なし
        '復活の場合は必要
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()


        If Me._Row.Item("SYS_DEL_FLG").ToString() Is "0" Then
            MyBase.SetResultCount(0)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>西濃マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSeinoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM400DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))


        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "InsertSeinoM", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>西濃マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSeinoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM400DAC.SQL_UPDATE _
                                                                                     , LMM400DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "UpdateSeinoM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>西濃マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSeinoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM400DAC.SQL_DELETE _
                                                                                     , LMM400DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM400DAC", "DeleteSeinoM", cmd)

        '更新&排他チェック
        Call Me.UpdateResultChk(cmd)

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
    ''' パラメータ設定モジュール(西濃マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEINO_KEY", Me.FormatNumValue(.Item("SEINO_KEY").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 必須項目チェック(郵便番号マスタ存在チェック)
    ''' 2015/3/17 現在未使用　検討中
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHissuItem()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'ハイフン入力があれば除去
            If IsNumeric(.Item("ZIP_NO").ToString()) = False Then
                Dim prmZip As String = String.Empty
                prmZip = System.Text.RegularExpressions.Regex.Replace(.Item("ZIP_NO").ToString(), "[^0-9]", "")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", prmZip, DBDataType.NVARCHAR))
                Exit Sub
            End If

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)20150317
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()


        'パラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEINO_KEY", .Item("SEINO_KEY").ToString(), DBDataType.NUMERIC))
            '画面で取得している更新日時項目
            Call Me.SetSysDateTime()
        End With
    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

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

        '更新項目
        Call Me.SetParamCommonSystemDel()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIWAKE_CD", .Item("SHIWAKE_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHAKU_CD", .Item("CHAKU_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHAKU_NM", .Item("CHAKU_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_K", .Item("KEN_K").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_K", .Item("CITY_K").ToString(), DBDataType.NVARCHAR))
            
        End With
    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        With Me._Row
            '新規は西濃キーがないのでチェック用にダミーをAdd
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEINO_KEY", "0", DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        End With
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEINO_KEY", Me._Row.Item("SEINO_KEY").ToString(), DBDataType.NUMERIC))
    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEINO_KEY", Me._Row.Item("SEINO_KEY").ToString(), DBDataType.NUMERIC))

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

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function


#End Region



#End Region

#End Region

End Class
