' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ330DAC : UNマスタ照会
'  作  成  者       :  asatsuma
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ330DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ330DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用(UNマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_UN As String = " SELECT COUNT(UNPG.UN_NO)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' COM_DB..M_IMDGデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UN As String = " SELECT                                  " & vbNewLine _
                                        & "      UNPG.SORT_KEY         AS SORT_KEY        " & vbNewLine _
                                        & "     ,UNPG.UN_NO            AS UN_NO           " & vbNewLine _
                                        & "     ,UNPG.PG_KBN           AS PG_KBN          " & vbNewLine _
                                        & "     ,UNPG.IMDG_CLASS       AS IMDG_CLASS      " & vbNewLine _
                                        & "     ,UNPG.IMDG_CLASS1      AS IMDG_CLASS1     " & vbNewLine _
                                        & "     ,UNPG.IMDG_CLASS2      AS IMDG_CLASS2     " & vbNewLine _
                                        & "     ,UNPG.MP_FLG           AS MP_FLG          " & vbNewLine _
                                        & "     ,UNPG.MP_FLG_NM        AS MP_FLG_NM       " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' COM_DB..M_IMDG
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_UN As String = " FROM                               " & vbNewLine _
                                              & " (SELECT                                       " & vbNewLine _
                                              & "       2                   AS SORT_KEY         " & vbNewLine _
                                              & "      ,UN.UN_NO            AS UN_NO            " & vbNewLine _
                                              & "      ,UN.PG_KBN           AS PG_KBN           " & vbNewLine _
                                              & "      ,UN.IMDG_CLASS       AS IMDG_CLASS       " & vbNewLine _
                                              & "      ,UN.IMDG_CLASS1      AS IMDG_CLASS1      " & vbNewLine _
                                              & "      ,UN.IMDG_CLASS2      AS IMDG_CLASS2      " & vbNewLine _
                                              & "      ,UN.MP_FLG           AS MP_FLG           " & vbNewLine _
                                              & "      ,KBN.KBN_NM1         AS MP_FLG_NM        " & vbNewLine _
                                              & "    FROM                                       " & vbNewLine _
                                              & "       COM_DB..M_IMDG  AS UN                   " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "        LM_MST..Z_KBN AS KBN                   " & vbNewLine _
                                              & "     ON UN.MP_FLG     = KBN.KBN_NM2            " & vbNewLine _
                                              & "    AND KBN_GROUP_CD  = 'G013'                 " & vbNewLine _
                                              & "  WHERE                                        " & vbNewLine _
                                              & "       UN.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                              & "  UNION ALL                                    " & vbNewLine _
                                              & "  SELECT                                       " & vbNewLine _
                                              & "       1                   AS SORT_KEY         " & vbNewLine _
                                              & "      ,'-'                 AS UN_NO            " & vbNewLine _
                                              & "      ,'-'                 AS PG_KBN           " & vbNewLine _
                                              & "      ,'非該当'            AS IMDG_CLASS       " & vbNewLine _
                                              & "      ,''                  AS IMDG_CLASS1      " & vbNewLine _
                                              & "      ,''                  AS IMDG_CLASS2      " & vbNewLine _
                                              & "      ,''                  AS MP_FLG           " & vbNewLine _
                                              & "      ,''                  AS MP_FLG_NM        " & vbNewLine _
                                              & " ) UNPG                                        " & vbNewLine _
                                              & "  WHERE                                        " & vbNewLine _
                                              & "       1 = 1                                   " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(COM_DB..M_IMDG)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UN As String = " ORDER BY                                       " & vbNewLine _
                                         & "     UNPG.SORT_KEY                                 " & vbNewLine _
                                         & "    ,UNPG.UN_NO                                    " & vbNewLine _
                                         & "    ,UNPG.PG_KBN                                   " & vbNewLine

#End Region

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
    ''' 検索件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ330DAC.SQL_SELECT_COUNT_UN)    'SQL構築(カウント用Select句:割増ヘッダ)
        Me._StrSql.Append(LMZ330DAC.SQL_FROM_DATA_UN)       'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                     '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ330DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        sql = Me.SelectExtc()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ330DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("UN_NO", "UN_NO")
        map.Add("PG_KBN", "PG_KBN")
        map.Add("IMDG_CLASS", "IMDG_CLASS")
        map.Add("IMDG_CLASS1", "IMDG_CLASS1")
        map.Add("IMDG_CLASS2", "IMDG_CLASS2")
        map.Add("MP_FLG", "MP_FLG")
        map.Add("MP_FLG_NM", "MP_FLG_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ330OUT")

        Return ds

    End Function

    ''' <summary>
    ''' UNマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectExtc() As String

        'SQL作成
        Me._StrSql.Append(LMZ330DAC.SQL_SELECT_DATA_UN)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ330DAC.SQL_FROM_DATA_UN)             'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ330DAC.SQL_ORDER_BY_UN)         'SQL構築(データ抽出用ORDER BY句)

        Return Me._StrSql.ToString()

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
        With Me._Row

            whereStr = .Item("UN_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND  ", "UNPG.UN_NO = @UN_NO"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UN_NO", whereStr, DBDataType.NCHAR))
            End If

            whereStr = .Item("PG_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND  ", "UNPG.PG_KBN = @PG_KBN"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PG_KBN", whereStr, DBDataType.NCHAR))
            End If

            whereStr = .Item("IMDG_CLASS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND ", "UNPG.IMDG_CLASS LIKE @IMDG_CLASS"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IMDG_CLASS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("IMDG_CLASS1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND ", "UNPG.IMDG_CLASS1 LIKE @IMDG_CLASS1"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IMDG_CLASS1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("IMDG_CLASS2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND ", "UNPG.IMDG_CLASS2 LIKE @IMDG_CLASS2"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IMDG_CLASS2", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("MP_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat(" AND  ", "UNPG.MP_FLG = @MP_FLG"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MP_FLG", whereStr, DBDataType.NCHAR))
            End If

        End With

    End Sub

#End Region

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

#End Region

End Class
