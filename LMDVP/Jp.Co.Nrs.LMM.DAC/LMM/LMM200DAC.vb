' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM200DAC : 車輌マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM200DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM200DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(VCLE.CAR_KEY)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' M_JISデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                               " & vbNewLine _
                                            & "	      VCLE.NRS_BR_CD                               AS NRS_BR_CD                        " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
                                            & "	     ,VCLE.UNSOCO_CD                               AS UNSOCO_CD                        " & vbNewLine _
                                            & "	     ,VCLE.UNSOCO_BR_CD                            AS UNSOCO_BR_CD                     " & vbNewLine _
                                            & "	     ,UNSOCO.UNSOCO_NM                             AS UNSOCO_NM                        " & vbNewLine _
                                            & "	     ,UNSOCO.UNSOCO_BR_NM                          AS UNSOCO_BR_NM                     " & vbNewLine _
                                            & "	     ,VCLE.CAR_NO                                  AS CAR_NO                           " & vbNewLine _
                                            & "	     ,VCLE.INSPC_DATE_TRUCK                        AS INSPC_DATE_TRUCK                 " & vbNewLine _
                                            & "	     ,VCLE.AVAL_YN                                 AS AVAL_YN                          " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                 AS AVAL_YN_NM                       " & vbNewLine _
                                            & "	     ,VCLE.VCLE_KB                                 AS VCLE_KB                          " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                 AS VCLE_KB_NM                       " & vbNewLine _
                                            & "	     ,VCLE.CAR_KEY                                 AS CAR_KEY                          " & vbNewLine _
                                            & "	     ,VCLE.TRAILER_NO                              AS TRAILER_NO                       " & vbNewLine _
                                            & "	     ,VCLE.JSHA_KB                                 AS JSHA_KB                          " & vbNewLine _
                                            & "	     ,VCLE.CAR_TP_KB                               AS CAR_TP_KB                        " & vbNewLine _
                                            & "	     ,VCLE.LOAD_WT                                 AS LOAD_WT                          " & vbNewLine _
                                            & "	     ,VCLE.TEMP_YN                                 AS TEMP_YN                          " & vbNewLine _
                                            & "	     ,VCLE.ONDO_MM                                 AS ONDO_MM                          " & vbNewLine _
                                            & "	     ,VCLE.ONDO_MX                                 AS ONDO_MX                          " & vbNewLine _
                                            & "	     ,VCLE.FUKUSU_ONDO_YN                          AS FUKUSU_ONDO_YN                   " & vbNewLine _
                                            & "	     ,VCLE.INSPC_DATE_TRAILER                      AS INSPC_DATE_TRAILER               " & vbNewLine _
                                            & "	     ,VCLE.DAY_UNCHIN                              AS DAY_UNCHIN                       --ADD 2022.09.06 " & vbNewLine _
                                            & "	     ,VCLE.SYS_ENT_DATE                            AS SYS_ENT_DATE                     " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                AS SYS_ENT_USER_NM	               " & vbNewLine _
                                            & "	     ,VCLE.SYS_UPD_DATE                            AS SYS_UPD_DATE                     " & vbNewLine _
                                            & "	     ,VCLE.SYS_UPD_TIME                            AS SYS_UPD_TIME                     " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                AS SYS_UPD_USER_NM                  " & vbNewLine _
                                            & "	     ,VCLE.SYS_DEL_FLG                             AS SYS_DEL_FLG                      " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                                 AS SYS_DEL_NM                       " & vbNewLine
#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                                                             " & vbNewLine _
                                          & "                      $LM_MST$..M_VCLE AS VCLE                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR   AS NRSBR                 " & vbNewLine _
                                          & "        ON VCLE.NRS_BR_CD                   = NRSBR.NRS_BR_CD       " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG                = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO   AS UNSOCO                " & vbNewLine _
                                          & "        ON VCLE.NRS_BR_CD                   = UNSOCO.NRS_BR_CD      " & vbNewLine _
                                          & "       AND VCLE.UNSOCO_CD                   = UNSOCO.UNSOCO_CD      " & vbNewLine _
                                          & "       AND VCLE.UNSOCO_BR_CD                = UNSOCO.UNSOCO_BR_CD   " & vbNewLine _
                                          & "       AND UNSOCO.SYS_DEL_FLG               = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN      AS KBN1                  " & vbNewLine _
                                          & "        ON VCLE.AVAL_YN                     = KBN1.KBN_CD           " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                = 'K017'                " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                 = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN      AS KBN2                  " & vbNewLine _
                                          & "        ON VCLE.VCLE_KB                     = KBN2.KBN_CD           " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD                = 'S012'                " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG                 = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER1                 " & vbNewLine _
                                          & "        ON VCLE.SYS_ENT_USER                = USER1.USER_CD         " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG                = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER     AS USER2                 " & vbNewLine _
                                          & "        ON VCLE.SYS_UPD_USER                = USER2.USER_CD         " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG                = '0'                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN      AS KBN3                  " & vbNewLine _
                                          & "        ON VCLE.SYS_DEL_FLG                 = KBN3.KBN_CD           " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD                = 'S051'                " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG                 = '0'                   " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                             " & vbNewLine _
                                         & "       VCLE.CAR_NO                                                   " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 車輌コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_SYARYO As String = "SELECT                                                        " & vbNewLine _
                                            & "   COUNT(UNSOCO_CD)            AS REC_CNT                      " & vbNewLine _
                                            & "  FROM $LM_MST$..M_VCLE                                        " & vbNewLine _
                                            & "  WHERE CAR_KEY              =    @CAR_KEY                     " & vbNewLine
                                           



#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_VCLE       " & vbNewLine _
                                       & "(                                  " & vbNewLine _
                                       & "       NRS_BR_CD                   " & vbNewLine _
                                       & "      ,CAR_KEY                     " & vbNewLine _
                                       & "      ,AVAL_YN                     " & vbNewLine _
                                       & "      ,CAR_NO                      " & vbNewLine _
                                       & "      ,TRAILER_NO                  " & vbNewLine _
                                       & "      ,UNSOCO_CD                   " & vbNewLine _
                                       & "      ,UNSOCO_BR_CD                " & vbNewLine _
                                       & "      ,JSHA_KB                     " & vbNewLine _
                                       & "      ,CAR_TP_KB                   " & vbNewLine _
                                       & "      ,VCLE_KB                     " & vbNewLine _
                                       & "      ,LOAD_WT                     " & vbNewLine _
                                       & "      ,TEMP_YN                     " & vbNewLine _
                                       & "      ,ONDO_MM                     " & vbNewLine _
                                       & "      ,ONDO_MX                     " & vbNewLine _
                                       & "      ,FUKUSU_ONDO_YN              " & vbNewLine _
                                       & "      ,INSPC_DATE_TRUCK            " & vbNewLine _
                                       & "      ,INSPC_DATE_TRAILER          " & vbNewLine _
                                       & "      ,DAY_UNCHIN                  --ADD 2022.09.06 " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,SYS_ENT_USER                " & vbNewLine _
                                       & "      ,SYS_UPD_DATE         	     " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,SYS_UPD_USER                " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                 " & vbNewLine _
                                       & "      ) VALUES (                   " & vbNewLine _
                                       & "       @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@CAR_KEY                    " & vbNewLine _
                                       & "      ,@AVAL_YN                    " & vbNewLine _
                                       & "      ,@CAR_NO                     " & vbNewLine _
                                       & "      ,@TRAILER_NO                 " & vbNewLine _
                                       & "      ,@UNSOCO_CD                  " & vbNewLine _
                                       & "      ,@UNSOCO_BR_CD               " & vbNewLine _
                                       & "      ,@JSHA_KB                    " & vbNewLine _
                                       & "      ,@CAR_TP_KB                  " & vbNewLine _
                                       & "      ,@VCLE_KB                    " & vbNewLine _
                                       & "      ,@LOAD_WT                    " & vbNewLine _
                                       & "      ,@TEMP_YN                    " & vbNewLine _
                                       & "      ,@ONDO_MM                    " & vbNewLine _
                                       & "      ,@ONDO_MX                    " & vbNewLine _
                                       & "      ,@FUKUSU_ONDO_YN             " & vbNewLine _
                                       & "      ,@INSPC_DATE_TRUCK           " & vbNewLine _
                                       & "      ,@INSPC_DATE_TRAILER         " & vbNewLine _
                                       & "      ,@DAY_UNCHIN                 --ADD 2022.09.06 " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	     " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         	     " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID        	     " & vbNewLine _
                                       & "      ,@SYS_ENT_USER               " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         	     " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	     " & vbNewLine _
                                       & "      ,@SYS_UPD_USER               " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	     " & vbNewLine _
                                       & ")                                  " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>     
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_VCLE SET                                 " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD                  " & vbNewLine _
                                       & "       ,AVAL_YN               = @AVAL_YN                    " & vbNewLine _
                                       & "       ,CAR_NO                = @CAR_NO                     " & vbNewLine _
                                       & "       ,TRAILER_NO            = @TRAILER_NO                 " & vbNewLine _
                                       & "       ,UNSOCO_CD             = @UNSOCO_CD                  " & vbNewLine _
                                       & "       ,UNSOCO_BR_CD          = @UNSOCO_BR_CD               " & vbNewLine _
                                       & "       ,JSHA_KB               = @JSHA_KB                    " & vbNewLine _
                                       & "       ,CAR_TP_KB             = @CAR_TP_KB                  " & vbNewLine _
                                       & "       ,VCLE_KB               = @VCLE_KB                    " & vbNewLine _
                                       & "       ,LOAD_WT               = @LOAD_WT                    " & vbNewLine _
                                       & "       ,TEMP_YN               = @TEMP_YN                    " & vbNewLine _
                                       & "       ,ONDO_MM               = @ONDO_MM                    " & vbNewLine _
                                       & "       ,ONDO_MX               = @ONDO_MX                    " & vbNewLine _
                                       & "       ,FUKUSU_ONDO_YN        = @FUKUSU_ONDO_YN             " & vbNewLine _
                                       & "       ,INSPC_DATE_TRUCK      = @INSPC_DATE_TRUCK           " & vbNewLine _
                                       & "       ,INSPC_DATE_TRAILER    = @INSPC_DATE_TRAILER         " & vbNewLine _
                                       & "       ,DAY_UNCHIN            = @DAY_UNCHIN                 --ADD 2022.09.06 " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE               " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME               " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID               " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER               " & vbNewLine _
                                       & " WHERE                                                      " & vbNewLine _
                                       & "        CAR_KEY               = @CAR_KEY                    " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_VCLE SET                   " & vbNewLine _
                                       & "        SYS_UPD_DATE  = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME  = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID  = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER  = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG   = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                        " & vbNewLine _
                                       & "        CAR_KEY       = @CAR_KEY              " & vbNewLine

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
    ''' 車輌マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM200DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM200DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM200DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function


    ''' <summary>
    ''' 車輌マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM200DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM200DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM200DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM200DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("INSPC_DATE_TRUCK", "INSPC_DATE_TRUCK")
        map.Add("AVAL_YN", "AVAL_YN")
        map.Add("AVAL_YN_NM", "AVAL_YN_NM")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("VCLE_KB_NM", "VCLE_KB_NM")
        map.Add("CAR_KEY", "CAR_KEY")
        map.Add("TRAILER_NO", "TRAILER_NO")
        map.Add("JSHA_KB", "JSHA_KB")
        map.Add("CAR_TP_KB", "CAR_TP_KB")
        map.Add("LOAD_WT", "LOAD_WT")
        map.Add("TEMP_YN", "TEMP_YN")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("FUKUSU_ONDO_YN", "FUKUSU_ONDO_YN")
        map.Add("INSPC_DATE_TRAILER", "INSPC_DATE_TRAILER")
        '2022.09.06 追加START
        map.Add("DAY_UNCHIN", "DAY_UNCHIN")
        '2022.09.06 追加END
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM200OUT")

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
                andstr.Append(" (VCLE.SYS_DEL_FLG = @SYS_DEL_FLG  OR VCLE.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (VCLE.NRS_BR_CD = @NRS_BR_CD  OR VCLE.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" VCLE.UNSOCO_CD LIKE @UNSOCO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" VCLE.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_BR_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_BR_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CAR_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" VCLE.CAR_NO LIKE @CAR_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If
           
            whereStr = .Item("AVAL_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (VCLE.AVAL_YN = @AVAL_YN  OR VCLE.AVAL_YN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("VCLE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (VCLE.VCLE_KB = @VCLE_KB  OR VCLE.VCLE_KB IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VCLE_KB", whereStr, DBDataType.CHAR))
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
    ''' 車輌マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSyaryoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM200DAC.SQL_EXIST_SYARYO)
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

        MyBase.Logger.WriteSQLLog("LMM200DAC", "SelectSyaryoM", cmd)

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
    ''' 車輌マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSyaryoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM200DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM200DAC", "InsertSyaryoM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 車輌マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSyaryoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM200DAC.SQL_UPDATE _
                                                                                     , LMM200DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM200DAC", "UpdateSyaryoM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 車輌マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSyaryoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM200IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM200DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM200DAC", "DeleteSyaryoM", cmd)

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KEY", .Item("CAR_KEY").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_TP_KB", .Item("CAR_TP_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", .Item("CAR_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INSPC_DATE_TRUCK", .Item("INSPC_DATE_TRUCK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRAILER_NO", .Item("TRAILER_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INSPC_DATE_TRAILER", .Item("INSPC_DATE_TRAILER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", .Item("AVAL_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JSHA_KB", .Item("JSHA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_WT", .Item("LOAD_WT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEMP_YN", .Item("TEMP_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MM", .Item("ONDO_MM").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MX", .Item("ONDO_MX").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FUKUSU_ONDO_YN", .Item("FUKUSU_ONDO_YN").ToString(), DBDataType.CHAR))
            '2022.09.06 追加START
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DAY_UNCHIN", .Item("DAY_UNCHIN").ToString(), DBDataType.NUMERIC))
            '2022.09.06 追加END


        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活時用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KEY", Me._Row.Item("CAR_KEY").ToString(), DBDataType.CHAR))
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
    ''' パラメータ設定モジュール(車輌マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KEY", Me._Row.Item("CAR_KEY").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
