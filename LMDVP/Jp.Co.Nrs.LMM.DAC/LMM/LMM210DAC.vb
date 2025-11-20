' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM050DAC : 乗務員マスタ
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM210DACクラス
''' </summary>
Public Class LMM210DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(DRIVER.DRIVER_CD)		   AS SELECT_CNT   " & vbNewLine


    ''' <summary>
    ''' DRIVER_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                             " & vbNewLine _
                                            & "	      DRIVER.YUSO_BR_CD                               AS YUSO_BR_CD                " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                                 AS YUSO_BR_NM                " & vbNewLine _
                                            & "	     ,DRIVER.DRIVER_CD                                 AS DRIVER_CD                  " & vbNewLine _
                                            & "	     ,DRIVER.DRIVER_NM                                 AS DRIVER_NM                  " & vbNewLine _
                                            & "	     ,DRIVER.AVAL_YN                                  AS AVAL_YN                   " & vbNewLine _
                                            & "	     ,DRIVER.LCAR_LICENSE_YN                          AS LCAR_LICENSE_YN           " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM2                                    AS LCAR_LICENSE_YN_NM        " & vbNewLine _
                                            & "	     ,DRIVER.TRAILER_LICENSE_YN                       AS TRAILER_LICENSE_YN        " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM2                                    AS TRAILER_LICENSE_YN_NM     " & vbNewLine _
                                            & "	     ,DRIVER.OTSU1_YN                                 AS OTSU1_YN                  " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM2                                    AS OTSU1_YN_NM               " & vbNewLine _
                                            & "	     ,DRIVER.OTSU2_YN                                 AS OTSU2_YN                  " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM2                                    AS OTSU2_YN_NM               " & vbNewLine _
                                            & "	     ,DRIVER.OTSU3_YN                                 AS OTSU3_YN                  " & vbNewLine _
                                            & "	     ,KBN5.KBN_NM2                                    AS OTSU3_YN_NM               " & vbNewLine _
                                            & " 	 ,DRIVER.OTSU4_YN                                 AS OTSU4_YN                  " & vbNewLine _
                                            & "	     ,KBN6.KBN_NM2                                    AS OTSU4_YN_NM               " & vbNewLine _
                                            & "	     ,DRIVER.OTSU5_YN                                 AS OTSU5_YN                  " & vbNewLine _
                                            & "	     ,KBN7.KBN_NM2                                    AS OTSU5_YN_NM               " & vbNewLine _
                                            & "	     ,DRIVER.OTSU6_YN                                 AS OTSU6_YN                  " & vbNewLine _
                                            & "	     ,KBN8.KBN_NM2                                    AS OTSU6_YN_NM               " & vbNewLine _
                                            & "	     ,DRIVER.HICOMPGAS_YN                             AS HICOMPGAS_YN              " & vbNewLine _
                                            & "	     ,KBN9.KBN_NM2                                    AS HICOMPGAS_YN_NM           " & vbNewLine _
                                            & "	     ,DRIVER.SYS_ENT_DATE                             AS SYS_ENT_DATE              " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                   AS SYS_ENT_USER_NM           " & vbNewLine _
                                            & "	     ,DRIVER.SYS_UPD_DATE                             AS SYS_UPD_DATE              " & vbNewLine _
                                            & "	     ,DRIVER.SYS_UPD_TIME                             AS SYS_UPD_TIME              " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                   AS SYS_UPD_USER_NM           " & vbNewLine _
                                            & "	     ,DRIVER.SYS_DEL_FLG                              AS SYS_DEL_FLG               " & vbNewLine _
                                            & "	     ,KBN10.KBN_NM2                                   AS SYS_DEL_NM                " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                       " & vbNewLine _
                                          & "      M_DRIVER AS DRIVER                                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                                   " & vbNewLine _
                                          & "        ON DRIVER.YUSO_BR_CD         = NRSBR.NRS_BR_CD                     " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG         = '0'                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                                    " & vbNewLine _
                                          & "        ON DRIVER.LCAR_LICENSE_YN    = KBN1.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD         = 'U009'                              " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG          = '0'                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                                    " & vbNewLine _
                                          & "        ON DRIVER.TRAILER_LICENSE_YN = KBN2.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD         = 'U009'                              " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG          = '0'                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU1_YN            = KBN3.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD         = 'U009'                              " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG          = '0'                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU2_YN           = KBN4.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD        = 'U009'                               " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN5                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU3_YN           = KBN5.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD        = 'U009'                               " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN6                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU4_YN           = KBN6.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN6.KBN_GROUP_CD        = 'U009'                               " & vbNewLine _
                                          & "       AND KBN6.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN7                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU5_YN           = KBN7.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD        = 'U009'                               " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN8                                    " & vbNewLine _
                                          & "        ON DRIVER.OTSU6_YN           = KBN8.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN8.KBN_GROUP_CD        = 'U009'                               " & vbNewLine _
                                          & "       AND KBN8.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN9                                    " & vbNewLine _
                                          & "        ON DRIVER. HICOMPGAS_YN      = KBN9.KBN_CD                         " & vbNewLine _
                                          & "       AND KBN9.KBN_GROUP_CD        ='U009'                                " & vbNewLine _
                                          & "       AND KBN9.SYS_DEL_FLG         = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                                   " & vbNewLine _
                                          & "        ON DRIVER.SYS_ENT_USER       = USER1.USER_CD                       " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG        = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                                   " & vbNewLine _
                                          & "        ON DRIVER.SYS_UPD_USER       = USER2.USER_CD                       " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG        = '0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN10                                   " & vbNewLine _
                                          & "        ON DRIVER.SYS_DEL_FLG        = KBN10.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN10.KBN_GROUP_CD       = 'S051'                               " & vbNewLine _
                                          & "       AND KBN10.SYS_DEL_FLG        = '0'                                  " & vbNewLine



#End Region


#Region "入力チェック"

    'START YANAI 要望番号808
    '''' <summary>
    '''' 乗務員コード存在チェック用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_EXIT_DRIVER As String = "SELECT                            " & vbNewLine _
    '                                        & "   COUNT(DRIVER_CD)  AS REC_CNT    " & vbNewLine _
    '                                        & "FROM $LM_MST$..M_DRIVER           " & vbNewLine _
    '                                        & "WHERE YUSO_BR_CD    = @YUSO_BR_CD " & vbNewLine _
    '                                        & "  AND DRIVER_CD    = @DRIVER_CD     " & vbNewLine
    ''' <summary>
    ''' 乗務員コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_DRIVER As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(DRIVER_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM $LM_MST$..M_DRIVER           " & vbNewLine _
                                            & "WHERE DRIVER_CD    = @DRIVER_CD     " & vbNewLine
    'END YANAI 要望番号808

#End Region

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_DRIVER    " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      YUSO_BR_CD               	" & vbNewLine _
                                       & "      ,DRIVER_CD         		    " & vbNewLine _
                                       & "      ,DRIVER_NM         		    " & vbNewLine _
                                       & "      ,AVAL_YN         	        " & vbNewLine _
                                       & "      ,LCAR_LICENSE_YN         	" & vbNewLine _
                                       & "      ,TRAILER_LICENSE_YN         " & vbNewLine _
                                       & "      ,OTSU1_YN         			" & vbNewLine _
                                       & "      ,OTSU2_YN        		 	" & vbNewLine _
                                       & "      ,OTSU3_YN         			" & vbNewLine _
                                       & "      ,OTSU4_YN         			" & vbNewLine _
                                       & "      ,OTSU5_YN         		    " & vbNewLine _
                                       & "      ,OTSU6_YN         			" & vbNewLine _
                                       & "      ,HICOMPGAS_YN         		" & vbNewLine _
                                       & "      ,SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,SYS_ENT_PGID       		" & vbNewLine _
                                       & "      ,SYS_ENT_USER        		" & vbNewLine _
                                       & "      ,SYS_UPD_DATE         		" & vbNewLine _
                                       & "      ,SYS_UPD_TIME         		" & vbNewLine _
                                       & "      ,SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,SYS_DEL_FLG         	    " & vbNewLine _
                                       & "      ) VALUES (                  " & vbNewLine _
                                       & "      @YUSO_BR_CD                 " & vbNewLine _
                                       & "      ,@DRIVER_CD         		    " & vbNewLine _
                                       & "      ,@DRIVER_NM         		    " & vbNewLine _
                                       & "      ,@AVAL_YN         	        " & vbNewLine _
                                       & "      ,@LCAR_LICENSE_YN         	" & vbNewLine _
                                       & "      ,@TRAILER_LICENSE_YN        " & vbNewLine _
                                       & "      ,@OTSU1_YN         			" & vbNewLine _
                                       & "      ,@OTSU2_YN        		 	" & vbNewLine _
                                       & "      ,@OTSU3_YN         			" & vbNewLine _
                                       & "      ,@OTSU4_YN     			    " & vbNewLine _
                                       & "      ,@OTSU5_YN        		    " & vbNewLine _
                                       & "      ,@OTSU6_YN    			    " & vbNewLine _
                                       & "      ,@HICOMPGAS_YN       		" & vbNewLine _
                                       & "      ,@SYS_ENT_DATE     		    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME    		    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID       		" & vbNewLine _
                                       & "      ,@SYS_ENT_USER        		" & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_DATE     		" & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_TIME     		" & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER         	    " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	    " & vbNewLine _
                                       & ")                                 " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_DRIVER SET                              " & vbNewLine _
                                       & "        YUSO_BR_CD                = @YUSO_BR_CD            " & vbNewLine _
                                       & "       ,DRIVER_NM                  = @DRIVER_NM              " & vbNewLine _
                                       & "       ,AVAL_YN                   = @AVAL_YN               " & vbNewLine _
                                       & "       ,LCAR_LICENSE_YN           = @LCAR_LICENSE_YN       " & vbNewLine _
                                       & "       ,TRAILER_LICENSE_YN        = @TRAILER_LICENSE_YN    " & vbNewLine _
                                       & "       ,OTSU1_YN                  = @OTSU1_YN              " & vbNewLine _
                                       & "       ,OTSU2_YN                  = @OTSU2_YN              " & vbNewLine _
                                       & "       ,OTSU3_YN                  = @OTSU3_YN              " & vbNewLine _
                                       & "       ,OTSU4_YN                  = @OTSU4_YN              " & vbNewLine _
                                       & "       ,OTSU5_YN                  = @OTSU5_YN              " & vbNewLine _
                                       & "       ,OTSU6_YN                  = @OTSU6_YN              " & vbNewLine _
                                       & "       ,HICOMPGAS_YN              = @HICOMPGAS_YN          " & vbNewLine _
                                       & "       ,SYS_UPD_DATE              = @DAC_SYS_UPD_DATE      " & vbNewLine _
                                       & "       ,SYS_UPD_TIME              = @DAC_SYS_UPD_TIME      " & vbNewLine _
                                       & "       ,SYS_UPD_PGID              = @SYS_UPD_PGID          " & vbNewLine _
                                       & "       ,SYS_UPD_USER              = @SYS_UPD_USER          " & vbNewLine _
                                       & " WHERE                                                     " & vbNewLine _
                                       & "        DRIVER_CD                  = @DRIVER_CD              " & vbNewLine _
                                       & " AND    SYS_UPD_DATE              = @SYS_UPD_DATE          " & vbNewLine _
                                       & " AND    SYS_UPD_TIME              = @SYS_UPD_TIME          " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_DRIVER SET                         " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @DAC_SYS_UPD_DATE     " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME     " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        DRIVER_CD              = @DRIVER_CD             " & vbNewLine _
                                       & " AND    SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & " AND    SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine

#Region "ORDER BY"


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     DRIVER.DRIVER_CD                                         " & vbNewLine


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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 乗務員マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM210DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM210DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定


        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function
    ''' <summary>
    ''' 請求先マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM210DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM210DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM210DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("YUSO_BR_NM", "YUSO_BR_NM")
        map.Add("DRIVER_CD", "DRIVER_CD")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("AVAL_YN", "AVAL_YN")
        map.Add("LCAR_LICENSE_YN", "LCAR_LICENSE_YN")
        map.Add("LCAR_LICENSE_YN_NM", "LCAR_LICENSE_YN_NM")
        map.Add("TRAILER_LICENSE_YN", "TRAILER_LICENSE_YN")
        map.Add("TRAILER_LICENSE_YN_NM", "TRAILER_LICENSE_YN_NM")
        map.Add("OTSU1_YN", "OTSU1_YN")
        map.Add("OTSU1_YN_NM", "OTSU1_YN_NM")
        map.Add("OTSU2_YN", "OTSU2_YN")
        map.Add("OTSU2_YN_NM", "OTSU2_YN_NM")
        map.Add("OTSU3_YN", "OTSU3_YN")
        map.Add("OTSU3_YN_NM", "OTSU3_YN_NM")
        map.Add("OTSU4_YN", "OTSU4_YN")
        map.Add("OTSU4_YN_NM", "OTSU4_YN_NM")
        map.Add("OTSU5_YN", "OTSU5_YN")
        map.Add("OTSU5_YN_NM", "OTSU5_YN_NM")
        map.Add("OTSU6_YN", "OTSU6_YN")
        map.Add("OTSU6_YN_NM", "OTSU6_YN_NM")
        map.Add("HICOMPGAS_YN", "HICOMPGAS_YN")
        map.Add("HICOMPGAS_YN_NM", "HICOMPGAS_YN_NM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM210OUT")

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
            '下記項目に条件式

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (DRIVER.SYS_DEL_FLG = @SYS_DEL_FLG  OR DRIVER.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("YUSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (DRIVER.YUSO_BR_CD = @YUSO_BR_CD OR DRIVER.YUSO_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DRIVER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DRIVER.DRIVER_CD LIKE @DRIVER_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DRIVER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DRIVER.DRIVER_NM LIKE @DRIVER_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("LCAR_LICENSE_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LCAR_LICENSE_YN = @LCAR_LICENSE_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LCAR_LICENSE_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TRAILER_LICENSE_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TRAILER_LICENSE_YN = @TRAILER_LICENSE_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRAILER_LICENSE_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OTSU1_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU1_YN = @OTSU1_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU1_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OTSU2_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU2_YN = @OTSU2_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU2_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OTSU3_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU3_YN = @OTSU3_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU3_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OTSU4_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU4_YN = @OTSU4_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU4_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OTSU5_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU5_YN = @OTSU5_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU5_YN", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("OTSU6_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OTSU6_YN  = @OTSU6_YN ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU6_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HICOMPGAS_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HICOMPGAS_YN = @HICOMPGAS_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HICOMPGAS_YN", whereStr, DBDataType.CHAR))
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
    ''' 乗務員マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectDriverM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM210DAC.SQL_EXIT_DRIVER)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME ")

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

        MyBase.Logger.WriteSQLLog("LMM210DAC", "SelectDriverM", cmd)

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
    ''' 乗務員マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistDriverM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM210DAC.SQL_EXIT_DRIVER _
                                                                        , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "CheckExistDriverM", cmd)

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
    ''' 乗務員マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertDriverM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM210DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "InsertDriverM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 乗務員マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateDriverM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM210DAC.SQL_UPDATE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "UpdateDriverM", cmd)

        '編集出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function
    ''' <summary>
    ''' 乗務員マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteDriverM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM210DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM210DAC", "DeleteDriverM", cmd)

        '削除出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

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

#Region "設定処理"

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(乗務員マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            'START YANAI 要望番号808
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号808
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", .Item("DRIVER_CD").ToString(), DBDataType.CHAR))

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", .Item("DRIVER_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", .Item("DRIVER_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", .Item("AVAL_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LCAR_LICENSE_YN", .Item("LCAR_LICENSE_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRAILER_LICENSE_YN", .Item("TRAILER_LICENSE_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU1_YN", .Item("OTSU1_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU2_YN", .Item("OTSU2_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU3_YN", .Item("OTSU3_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU4_YN", .Item("OTSU4_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU5_YN", .Item("OTSU5_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OTSU6_YN", .Item("OTSU6_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HICOMPGAS_YN", .Item("HICOMPGAS_YN").ToString(), DBDataType.CHAR))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", Me._Row.Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", Me._Row.Item("DRIVER_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))


    End Sub


#End Region

#End Region

#End Region

End Class
