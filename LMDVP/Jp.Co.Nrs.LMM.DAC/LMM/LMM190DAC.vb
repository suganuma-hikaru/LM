' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM190DAC : 距離程マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM190DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM190DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(KYORI.KYORI_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' M_JISデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                 " & vbNewLine _
                                            & "	      KYORI.NRS_BR_CD                              AS NRS_BR_CD                        " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
                                            & "	     ,KYORI.KYORI_CD                               AS KYORI_CD                         " & vbNewLine _
                                            & "	     ,KYORI.ORIG_JIS_CD                            AS ORIG_JIS_CD                      " & vbNewLine _
                                            & "	     ,JIS1.SHI                                     AS ORIG_SHI                         " & vbNewLine _
                                            & "	     ,KYORI.DEST_JIS_CD                            AS DEST_JIS_CD                      " & vbNewLine _
                                            & "	     ,JIS2.SHI                                     AS DEST_SHI                         " & vbNewLine _
                                            & "	     ,KYORI.KYORI                                  AS KYORI                            " & vbNewLine _
                                            & "	     ,KYORI.KYORI_REM                              AS KYORI_REM                        " & vbNewLine _
                                            & "	     ,JIS1.KEN                                     AS ORIG_KEN                         " & vbNewLine _
                                            & "	     ,JIS2.KEN                                     AS DEST_KEN                         " & vbNewLine _
                                            & "	     ,KYORI.SYS_ENT_DATE                           AS SYS_ENT_DATE                     " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                AS SYS_ENT_USER_NM	               " & vbNewLine _
                                            & "	     ,KYORI.SYS_UPD_DATE                           AS SYS_UPD_DATE                     " & vbNewLine _
                                            & "	     ,KYORI.SYS_UPD_TIME                           AS SYS_UPD_TIME                     " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                AS SYS_UPD_USER_NM                  " & vbNewLine _
                                            & "	     ,KYORI.SYS_DEL_FLG                            AS SYS_DEL_FLG                      " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                 AS SYS_DEL_NM                       " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                             " & vbNewLine _
                                          & "                      $LM_MST$..M_KYORI AS KYORI                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR    AS NRSBR             " & vbNewLine _
                                          & "        ON KYORI.NRS_BR_CD                  = NRSBR.NRS_BR_CD    " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG                = '0'                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_JIS      AS JIS1               " & vbNewLine _
                                          & "        ON KYORI.ORIG_JIS_CD                = JIS1.JIS_CD        " & vbNewLine _
                                          & "       AND JIS1.SYS_DEL_FLG                 = '0'                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_JIS      AS JIS2               " & vbNewLine _
                                          & "        ON KYORI.DEST_JIS_CD                = JIS2.JIS_CD        " & vbNewLine _
                                          & "       AND JIS2.SYS_DEL_FLG                 = '0'                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER1              " & vbNewLine _
                                          & "        ON KYORI.SYS_ENT_USER               = USER1.USER_CD      " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG                = '0'                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER2              " & vbNewLine _
                                          & "        ON KYORI.SYS_UPD_USER               = USER2.USER_CD      " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG                = '0'                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN      AS KBN1               " & vbNewLine _
                                          & "        ON KYORI.SYS_DEL_FLG                  = KBN1.KBN_CD      " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                = 'S051'             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                 = '0'                " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                             " & vbNewLine _
                                         & "       KYORI.KYORI_CD                                                " & vbNewLine _
                                         & "      ,KYORI.ORIG_JIS_CD                                             " & vbNewLine _
                                         & "      ,KYORI.DEST_JIS_CD                                             " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 距離程コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_KYORI As String = "SELECT                                                        " & vbNewLine _
                                            & "   COUNT(KYORI_CD)            AS REC_CNT                      " & vbNewLine _
                                            & " FROM $LM_MST$..M_KYORI                                       " & vbNewLine _
                                            & " WHERE NRS_BR_CD              = @NRS_BR_CD                    " & vbNewLine _
                                            & "  AND   KYORI_CD              = @KYORI_CD                     " & vbNewLine _
                                            & "  AND   ORIG_JIS_CD           = @ORIG_JIS_CD                  " & vbNewLine _
                                            & "  AND   DEST_JIS_CD           = @DEST_JIS_CD                  " & vbNewLine



#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_KYORI     " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "       NRS_BR_CD                  " & vbNewLine _
                                       & "      ,KYORI_CD                   " & vbNewLine _
                                       & "      ,ORIG_JIS_CD                " & vbNewLine _
                                       & "      ,DEST_JIS_CD                " & vbNewLine _
                                       & "      ,KYORI_REM                  " & vbNewLine _
                                       & "      ,KYORI                      " & vbNewLine _
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
                                       & "       @NRS_BR_CD                 " & vbNewLine _
                                       & "      ,@KYORI_CD                  " & vbNewLine _
                                       & "      ,@ORIG_JIS_CD               " & vbNewLine _
                                       & "      ,@DEST_JIS_CD               " & vbNewLine _
                                       & "      ,@KYORI_REM                 " & vbNewLine _
                                       & "      ,@KYORI                     " & vbNewLine _
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
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_KYORI SET                                  " & vbNewLine _
                                       & "        KYORI                 = @KYORI                        " & vbNewLine _
                                       & "       ,KYORI_REM             = @KYORI_REM                    " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE                 " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME                 " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID                 " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER                 " & vbNewLine _
                                       & " WHERE                                                        " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD                    " & vbNewLine _
                                       & "  AND   KYORI_CD              = @KYORI_CD                     " & vbNewLine _
                                       & "  AND   ORIG_JIS_CD           = @ORIG_JIS_CD                  " & vbNewLine _
                                       & "  AND   DEST_JIS_CD           = @DEST_JIS_CD                  " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_KYORI SET                  " & vbNewLine _
                                       & "        SYS_UPD_DATE  = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME  = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID  = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER  = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG   = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                        " & vbNewLine _
                                       & "        NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                                       & "  AND   KYORI_CD      = @KYORI_CD             " & vbNewLine _
                                       & "  AND   ORIG_JIS_CD   = @ORIG_JIS_CD          " & vbNewLine _
                                       & "  AND   DEST_JIS_CD   = @DEST_JIS_CD          " & vbNewLine
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
    ''' 距離程マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM190DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM190DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 距離程マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM190DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM190DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM190DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("KYORI_CD", "KYORI_CD")
        map.Add("ORIG_JIS_CD", "ORIG_JIS_CD")
        map.Add("ORIG_KEN", "ORIG_KEN")
        map.Add("ORIG_SHI", "ORIG_SHI")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_KEN", "DEST_KEN")
        map.Add("DEST_SHI", "DEST_SHI")
        map.Add("KYORI", "KYORI")
        map.Add("KYORI_REM", "KYORI_REM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM190OUT")

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
                andstr.Append(" (KYORI.SYS_DEL_FLG = @SYS_DEL_FLG  OR KYORI.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (KYORI.NRS_BR_CD = @NRS_BR_CD  OR KYORI.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("KYORI_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" KYORI.KYORI_CD LIKE @KYORI_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("ORIG_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" KYORI.ORIG_JIS_CD LIKE @ORIG_JIS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_JIS_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'START YANAI 要望番号1288 距離呈マスタの検索でアベンド
            'whereStr = .Item("ORIG_SHI").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    If andstr.Length <> 0 Then
            '        andstr.Append("AND")
            '    End If
            '    andstr.Append(" JIS1.SHI LIKE @SHI")
            '    andstr.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            'End If
            whereStr = .Item("ORIG_SHI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" JIS1.SHI LIKE @ORIG_SHI")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_SHI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            'END YANAI 要望番号1288 距離呈マスタの検索でアベンド

            whereStr = .Item("DEST_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" KYORI.DEST_JIS_CD LIKE @DEST_JIS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'START YANAI 要望番号1288 距離呈マスタの検索でアベンド
            'whereStr = .Item("DEST_SHI").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    If andstr.Length <> 0 Then
            '        andstr.Append("AND")
            '    End If
            '    andstr.Append(" JIS2.SHI LIKE @SHI")
            '    andstr.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            'End If
            whereStr = .Item("DEST_SHI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" JIS2.SHI LIKE @DEST_SHI")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SHI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            'END YANAI 要望番号1288 距離呈マスタの検索でアベンド

            whereStr = .Item("KYORI_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" KYORI.KYORI_REM LIKE @KYORI_REM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
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
    ''' 距離程マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistKyoriM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM190DAC.SQL_EXIST_KYORI, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "CheckExistKyoriM", cmd)

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
    ''' 距離程マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectKyoriM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM190DAC.SQL_EXIST_KYORI)
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

        MyBase.Logger.WriteSQLLog("LMM190DAC", "SelectKyoriM", cmd)

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
    ''' 距離程マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertKyoriM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM190DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "InsertKyoriM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 距離程マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateKyoriM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM190DAC.SQL_UPDATE _
                                                                                     , LMM190DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "UpdateKyoriM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 距離程マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteKyoriM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM190IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM190DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM190DAC", "DeleteKyoriM", cmd)

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
    ''' パラメータ設定モジュール(更新登録)
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
    ''' パラメータ設定モジュール(削除・復活)
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
    ''' パラメータ設定モジュール(新規登録用)
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
    ''' パラメータ設定モジュール(登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", .Item("KYORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_JIS_CD", .Item("ORIG_JIS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("DEST_JIS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI", .Item("KYORI").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_REM", .Item("KYORI_REM").ToString(), DBDataType.NVARCHAR))


        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活時用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", Me._Row.Item("KYORI_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_JIS_CD", Me._Row.Item("ORIG_JIS_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me._Row.Item("DEST_JIS_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

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
    ''' パラメータ設定モジュール(距離程マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", Me._Row.Item("KYORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_JIS_CD", Me._Row.Item("ORIG_JIS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me._Row.Item("DEST_JIS_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
