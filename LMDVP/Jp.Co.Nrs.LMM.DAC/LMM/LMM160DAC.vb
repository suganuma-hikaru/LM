' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM160DAC : 届出商品マスタ
'  作  成  者       :  高道
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM160DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM160DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(DESTGOODS.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' M_DESTGOODSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                               " & vbNewLine _
                                            & "	      DESTGOODS.NRS_BR_CD                                   AS NRS_BR_CD             " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                                       AS NRS_BR_NM             " & vbNewLine _
                                            & "	     ,DESTGOODS.CUST_CD_L                                   AS CUST_CD_L             " & vbNewLine _
                                            & "	     ,DESTGOODS.CUST_CD_M                                   AS CUST_CD_M             " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_L                                        AS CUST_NM_L             " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_M                                        AS CUST_NM_M             " & vbNewLine _
                                            & "	     ,DESTGOODS.CD                                          AS CD                    " & vbNewLine _
                                            & "	     ,DEST.DEST_NM                                          AS DEST_NM               " & vbNewLine _
                                            & "	     ,DESTGOODS.GOODS_CD_NRS                                AS GOODS_CD_NRS          " & vbNewLine _
                                            & "	     ,DESTGOODS.GOODS_NM                                    AS GOODS_NM              " & vbNewLine _
                                            & "	     ,DESTGOODS.SAGYO_KB_1                                  AS SAGYO_KB_1            " & vbNewLine _
                                            & "	     ,DESTGOODS.SAGYO_KB_2                                  AS SAGYO_KB_2            " & vbNewLine _
                                            & "	     ,DESTGOODS.SAGYO_SEIQTO_CD                             AS SAGYO_SEIQTO_CD       " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_NM + ' ' + SEIQTO.SEIQTO_BUSYO_NM       AS SAGYO_SEIQTO_NM       " & vbNewLine _
                                            & "	     ,SAGYO_1.SAGYO_NM                                      AS SAGYO_NM_1            " & vbNewLine _
                                            & "	     ,SAGYO_2.SAGYO_NM                                      AS SAGYO_NM_2            " & vbNewLine _
                                            & "	     ,GOODS.GOODS_CD_CUST                                   AS GOODS_CD              " & vbNewLine _
                                            & "	     ,GOODS.GOODS_NM_1                                      AS GOODS_NM_1            " & vbNewLine _
                                            & "	     ,DESTGOODS.SYS_ENT_DATE                                AS SYS_ENT_DATE          " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                         AS SYS_ENT_USER_NM       " & vbNewLine _
                                            & "	     ,DESTGOODS.SYS_UPD_DATE                                AS SYS_UPD_DATE          " & vbNewLine _
                                            & "	     ,DESTGOODS.SYS_UPD_TIME                                AS SYS_UPD_TIME          " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                         AS SYS_UPD_USER_NM       " & vbNewLine _
                                            & "	     ,DESTGOODS.SYS_DEL_FLG                                 AS SYS_DEL_FLG           " & vbNewLine _
                                            & "	     ,KBN.KBN_NM1                                           AS SYS_DEL_NM            " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                    " & vbNewLine _
                                          & "                  $LM_MST$..M_DESTGOODS          AS DESTGOODS           " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_NRS_BR             AS NRSBR               " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = NRSBR.NRS_BR_CD           " & vbNewLine _
                                          & "              AND NRSBR.SYS_DEL_FLG         = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN                                                       " & vbNewLine _
                                          & "                      ( SELECT                                          " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                              " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                              " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                              " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M                              " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M                              " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S          " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS         " & vbNewLine _
                                          & "                            FROM      $LM_MST$..M_CUST     AS CUST      " & vbNewLine _
                                          & "                           WHERE      SYS_DEL_FLG = '0'                 " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                       " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                       " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                       " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_M                       " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_M                       " & vbNewLine _
                                          & "                                                    )      AS CUST      " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = CUST.NRS_BR_CD            " & vbNewLine _
                                          & "              AND DESTGOODS.CUST_CD_L       = CUST.CUST_CD_L            " & vbNewLine _
                                          & "              AND DESTGOODS.CUST_CD_M       = CUST.CUST_CD_M            " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_DEST               AS DEST                " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = DEST.NRS_BR_CD            " & vbNewLine _
                                          & "              AND DESTGOODS.CUST_CD_L       = DEST.CUST_CD_L            " & vbNewLine _
                                          & "              AND DESTGOODS.CD              = DEST.DEST_CD              " & vbNewLine _
                                          & "              AND DEST.SYS_DEL_FLG          = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_SEIQTO             AS SEIQTO              " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = SEIQTO.NRS_BR_CD          " & vbNewLine _
                                          & "              AND DESTGOODS.SAGYO_SEIQTO_CD = SEIQTO.SEIQTO_CD          " & vbNewLine _
                                          & "              AND SEIQTO.SYS_DEL_FLG        = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_SAGYO              AS SAGYO_1             " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = SAGYO_1.NRS_BR_CD         " & vbNewLine _
                                          & "              AND DESTGOODS.SAGYO_KB_1      = SAGYO_1.SAGYO_CD          " & vbNewLine _
                                          & "              AND SAGYO_1.SYS_DEL_FLG       = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_SAGYO              AS SAGYO_2             " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = SAGYO_2.NRS_BR_CD         " & vbNewLine _
                                          & "              AND DESTGOODS.SAGYO_KB_2      = SAGYO_2.SAGYO_CD          " & vbNewLine _
                                          & "              AND SAGYO_2.SYS_DEL_FLG       = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..M_GOODS              AS GOODS               " & vbNewLine _
                                          & "               ON DESTGOODS.NRS_BR_CD       = GOODS.NRS_BR_CD           " & vbNewLine _
                                          & "              AND DESTGOODS.GOODS_CD_NRS    = GOODS.GOODS_CD_NRS        " & vbNewLine _
                                          & "              AND GOODS.SYS_DEL_FLG         = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..S_USER               AS USER1               " & vbNewLine _
                                          & "              ON DESTGOODS.SYS_ENT_USER     = USER1.USER_CD             " & vbNewLine _
                                          & "              AND USER1.SYS_DEL_FLG         = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..S_USER               AS USER2               " & vbNewLine _
                                          & "              ON DESTGOODS.SYS_UPD_USER     = USER2.USER_CD             " & vbNewLine _
                                          & "              AND USER2.SYS_DEL_FLG         = '0'                       " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN                 " & vbNewLine _
                                          & "              ON DESTGOODS.SYS_DEL_FLG      = KBN.KBN_CD                " & vbNewLine _
                                          & "              AND KBN.KBN_GROUP_CD          = 'S051'                    " & vbNewLine _
                                          & "              AND KBN.SYS_DEL_FLG           = '0'                       " & vbNewLine


#End Region

#Region "ORDER BY"


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     DESTGOODS.CUST_CD_L                               " & vbNewLine _
                                         & "     ,DESTGOODS.CUST_CD_M                              " & vbNewLine _
                                         & "     ,DESTGOODS.GOODS_CD_NRS                           " & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 届先商品レコード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_DESTGOODS As String = "SELECT                                                " & vbNewLine _
                                            & "   COUNT(CUST_CD_L)  AS REC_CNT                          " & vbNewLine _
                                            & "FROM $LM_MST$..M_DESTGOODS                               " & vbNewLine _
                                            & "WHERE NRS_BR_CD        = @NRS_BR_CD                      " & vbNewLine _
                                            & "  AND CUST_CD_L        = @CUST_CD_L                      " & vbNewLine _
                                            & "  AND CUST_CD_M        = @CUST_CD_M                      " & vbNewLine _
                                            & "  AND CD               = @CD                             " & vbNewLine _
                                            & "  AND GOODS_CD_NRS     = @GOODS_CD_NRS                   " & vbNewLine


#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_DESTGOODS " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      NRS_BR_CD               	" & vbNewLine _
                                       & "      ,CUST_CD_L         		    " & vbNewLine _
                                       & "      ,CUST_CD_M         		    " & vbNewLine _
                                       & "      ,GOODS_CD_NRS         		" & vbNewLine _
                                       & "      ,CD         	            " & vbNewLine _
                                       & "      ,GOODS_NM         			" & vbNewLine _
                                       & "      ,SAGYO_KB_1        		 	" & vbNewLine _
                                       & "      ,SAGYO_KB_2         		" & vbNewLine _
                                       & "      ,SAGYO_SEIQTO_CD         	" & vbNewLine _
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
                                       & "      @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@CUST_CD_L         		" & vbNewLine _
                                       & "      ,@CUST_CD_M         		" & vbNewLine _
                                       & "      ,@GOODS_CD_NRS         		" & vbNewLine _
                                       & "      ,@CD         	            " & vbNewLine _
                                       & "      ,@GOODS_NM         			" & vbNewLine _
                                       & "      ,@SAGYO_KB_1        		" & vbNewLine _
                                       & "      ,@SAGYO_KB_2         		" & vbNewLine _
                                       & "      ,@SAGYO_SEIQTO_CD         	" & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         		" & vbNewLine _
                                       & "      ,@SYS_ENT_USER         		" & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_DATE     		" & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_TIME     		" & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,@SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         		" & vbNewLine _
                                       & ")                                 " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_DESTGOODS SET                      " & vbNewLine _
                                       & "        GOODS_NM     	        = @GOODS_NM             " & vbNewLine _
                                       & "       ,SAGYO_KB_1           	= @SAGYO_KB_1         	" & vbNewLine _
                                       & "       ,SAGYO_KB_2         	= @SAGYO_KB_2         	" & vbNewLine _
                                       & "       ,SAGYO_SEIQTO_CD       = @SAGYO_SEIQTO_CD      " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @DAC_SYS_UPD_DATE     " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME     " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     CUST_CD_L            = @CUST_CD_L            " & vbNewLine _
                                       & " AND     CUST_CD_M            = @CUST_CD_M            " & vbNewLine _
                                       & " AND     CD                   = @CD                   " & vbNewLine _
                                       & " AND     GOODS_CD_NRS         = @GOODS_CD_NRS         " & vbNewLine _
                                       & " AND     SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                       & " AND     SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine


    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_DESTGOODS SET                       " & vbNewLine _
                                        & "        SYS_UPD_DATE          = @DAC_SYS_UPD_DATE     " & vbNewLine _
                                        & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME     " & vbNewLine _
                                        & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                        & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                        & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                        & " WHERE                                                " & vbNewLine _
                                        & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                        & " AND     CUST_CD_L            = @CUST_CD_L            " & vbNewLine _
                                        & " AND     CUST_CD_M            = @CUST_CD_M            " & vbNewLine _
                                        & " AND     CD                   = @CD                   " & vbNewLine _
                                        & " AND     GOODS_CD_NRS         = @GOODS_CD_NRS         " & vbNewLine _
                                        & " AND     SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                        & " AND     SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine

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
    ''' 届先商品マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM160DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM160DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 届先商品マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM160DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM160DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM160DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()


        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CD", "CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SAGYO_KB_1", "SAGYO_KB_1")
        map.Add("SAGYO_KB_2", "SAGYO_KB_2")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_NM", "SAGYO_SEIQTO_NM")
        map.Add("SAGYO_NM_1", "SAGYO_NM_1")
        map.Add("SAGYO_NM_2", "SAGYO_NM_2")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM160OUT")

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
                andstr.Append(" (DESTGOODS.SYS_DEL_FLG = @SYS_DEL_FLG  OR DESTGOODS.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (DESTGOODS.NRS_BR_CD = @NRS_BR_CD OR DESTGOODS.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_M LIKE @CUST_NM_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.CD LIKE @CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_CD_CUST LIKE @GOODS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.GOODS_NM LIKE @GOODS_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SAGYO_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.SAGYO_KB_1 LIKE @SAGYO_KB_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB_1", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.SAGYO_KB_2 LIKE @SAGYO_KB_2")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB_2", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DESTGOODS.SAGYO_SEIQTO_CD LIKE @SAGYO_SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SEIQTO_CD", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
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
    ''' 届先商品マスタ排他チェック 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectDestgoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMM160DAC.SQL_EXIT_DESTGOODS)
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

        MyBase.Logger.WriteSQLLog("LMM160DAC", "SelectDestgoodsM", cmd)

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
    ''' 届先商品マスタ存在チェック 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistDestgoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM160DAC.SQL_EXIT_DESTGOODS _
                                                                        , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "CheckExistDestgoodsM", cmd)

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
    ''' 届先商品マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertDestgoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM160DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "InsertDestgoodsM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 届先商品マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateDestgoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM160DAC.SQL_UPDATE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "UpdateDestgoodsM", cmd)

        '更新出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先商品マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先商品マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteDestgoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM160DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM160DAC", "DeleteDestgoodsM", cmd)

        '削除出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If


        Return ds

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
    ''' パラメータ設定モジュール(届先商品マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD", .Item("CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD", .Item("CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB_1", .Item("SAGYO_KB_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB_2", .Item("SAGYO_KB_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SEIQTO_CD", .Item("SAGYO_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))        '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DAC_SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DAC_SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD", Me._Row.Item("CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
