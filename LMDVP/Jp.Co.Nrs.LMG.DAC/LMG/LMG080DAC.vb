' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG080DAC : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG080DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "Count用"

    ''' <summary>
    ''' SQLCountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT As String = "SELECT COUNT(HEAD.NRS_BR_CD) AS SELECT_CNT                                                                  " & vbNewLine _
                                           & "FROM $LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                                               " & vbNewLine _
                                           & " LEFT OUTER JOIN $LM_MST$..S_USER AS SUSER                                                             " & vbNewLine _
                                           & "  ON SUSER.USER_CD = HEAD.OPE_USER_CD                                                                  " & vbNewLine _
                                           & " LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST                                                              " & vbNewLine _
                                           & "  ON  CUST.NRS_BR_CD = HEAD.NRS_BR_CD                                                                  " & vbNewLine _
                                           & "  AND CUST.CUST_CD_L = HEAD.CUST_CD_L                                                                  " & vbNewLine _
                                           & "  AND CUST.CUST_CD_M = HEAD.CUST_CD_M                                                                  " & vbNewLine _
                                           & "  AND CUST.CUST_CD_S = HEAD.CUST_CD_S                                                                  " & vbNewLine _
                                           & "  AND CUST.CUST_CD_SS = HEAD.CUST_CD_SS                                                                " & vbNewLine _
                                           & "WHERE HEAD.SYS_DEL_FLG = '0'                                                                           " & vbNewLine

#End Region

#Region "SELECT文"

    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = "SELECT                                                                                                 " & vbNewLine _
                                       & "  HEAD.EXEC_TIMING_KB AS EXEC_TIMING_KB,                                                               " & vbNewLine _
                                       & "  KBN.#KBN# AS EXEC_TIMING_NM,                                                                         " & vbNewLine _
                                       & "  HEAD.NRS_BR_CD AS NRS_BR_CD,                                                                         " & vbNewLine _
                                       & "  NRS.NRS_BR_NM AS NRS_BR_NM,                                                                          " & vbNewLine _
                                       & "  HEAD.OPE_USER_CD AS OPE_USER_CD,                                                                     " & vbNewLine _
                                       & "  SUSER.USER_NM AS USER_NM,                                                                            " & vbNewLine _
                                       & "  CUST.OYA_SEIQTO_CD AS SEIQTO_CD,                                                                     " & vbNewLine _
                                       & "  HEAD.SEKY_DATE,                                                                                      " & vbNewLine _
                                       & "  HEAD.CUST_CD_L + '-' + HEAD.CUST_CD_M + '-' + HEAD.CUST_CD_S + '-' + HEAD.CUST_CD_SS AS CUST_CD,     " & vbNewLine _
                                       & "  HEAD.CUST_CD_L AS CUST_CD_L,                                                                         " & vbNewLine _
                                       & "  HEAD.CUST_CD_M AS CUST_CD_M,                                                                         " & vbNewLine _
                                       & "  HEAD.CUST_CD_S AS CUST_CD_S,                                                                         " & vbNewLine _
                                       & "  HEAD.CUST_CD_SS AS CUST_CD_SS,                                                                       " & vbNewLine _
                                       & "  CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM,     " & vbNewLine _
                                       & "  HEAD.JOB_NO AS JOB_NO,                                                                               " & vbNewLine _
                                       & "  HEAD.SEKY_FLG AS SEKY_FLG,                                                                           " & vbNewLine _
                                       & "  KBN2.#KBN# AS SEKY_FLG_NM,                                                                           " & vbNewLine _
                                       & "  HEAD.SYS_ENT_DATE AS JIKKO_DATE,                                                                     " & vbNewLine _
                                       & "  HEAD.SYS_ENT_TIME AS JIKKO_TIME,                                                                     " & vbNewLine _
                                       & "  HEAD.EXEC_START_DATE AS SHORI_DATE,                                                                  " & vbNewLine _
                                       & "  HEAD.EXEC_START_TIME AS SHORI_TIME,                                                                  " & vbNewLine _
                                       & "  HEAD.EXEC_END_DATE AS SYURYO_DATE,                                                                   " & vbNewLine _
                                       & "  HEAD.EXEC_END_TIME AS SYURYO_TIME,                                                                   " & vbNewLine _
                                       & "  KBN1.#KBN# AS EXEC_STATE_NM,                                                                         " & vbNewLine _
                                       & "  HEAD.EXEC_STATE_KB AS EXEC_STATE_KB,                                                                 " & vbNewLine _
                                       & "  MESSAG.#MESSAGE_STRING#  AS MESSAGE_STRING,                                                          " & vbNewLine _
                                       & "  HEAD.BATCH_NO AS BATCH_NO,                                                                           " & vbNewLine _
                                       & "  HEAD.REC_NO AS REC_NO,                                                                               " & vbNewLine _
                                       & "  HEAD.INV_DATE_TO AS INV_DATE_TO,                                                                     " & vbNewLine _
                                       & "  HEAD.SYS_UPD_DATE AS SYS_UPD_DATE,                                                                   " & vbNewLine _
                                       & "  HEAD.SYS_UPD_TIME AS SYS_UPD_TIME                                                                    " & vbNewLine _
                                       & "FROM $LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                                               " & vbNewLine _
                                       & " LEFT OUTER JOIN $LM_MST$..M_NRS_BR AS NRS                                                             " & vbNewLine _
                                       & "  ON NRS.NRS_BR_CD = HEAD.NRS_BR_CD                                                                    " & vbNewLine _
                                       & " INNER JOIN $LM_MST$..Z_KBN AS KBN                                                                     " & vbNewLine _
                                       & "  ON KBN.KBN_GROUP_CD = 'S070'                                                                         " & vbNewLine _
                                       & "  AND KBN.KBN_CD = HEAD.EXEC_TIMING_KB                                                                 " & vbNewLine _
                                       & " LEFT OUTER JOIN $LM_MST$..S_USER AS SUSER                                                             " & vbNewLine _
                                       & "  ON SUSER.USER_CD = HEAD.OPE_USER_CD                                                                  " & vbNewLine _
                                       & " LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST                                                              " & vbNewLine _
                                       & "  ON  CUST.NRS_BR_CD = HEAD.NRS_BR_CD                                                                  " & vbNewLine _
                                       & "  AND CUST.CUST_CD_L = HEAD.CUST_CD_L                                                                  " & vbNewLine _
                                       & "  AND CUST.CUST_CD_M = HEAD.CUST_CD_M                                                                  " & vbNewLine _
                                       & "  AND CUST.CUST_CD_S = HEAD.CUST_CD_S                                                                  " & vbNewLine _
                                       & "  AND CUST.CUST_CD_SS = HEAD.CUST_CD_SS                                                                " & vbNewLine _
                                       & " INNER JOIN $LM_MST$..Z_KBN AS KBN1                                                                    " & vbNewLine _
                                       & "  ON KBN1.KBN_GROUP_CD = 'S068'                                                                        " & vbNewLine _
                                       & "  AND KBN1.KBN_CD = HEAD.EXEC_STATE_KB                                                                 " & vbNewLine _
                                       & " LEFT OUTER JOIN $LM_MST$..S_MESSAGE AS MESSAG                                                         " & vbNewLine _
                                       & "  ON HEAD.MESSAGE_ID = MESSAG.MESSAGE_ID                                                               " & vbNewLine _
                                       & " INNER JOIN $LM_MST$..Z_KBN AS KBN2                                                                    " & vbNewLine _
                                       & "  ON KBN2.KBN_GROUP_CD = 'H013'                                                                        " & vbNewLine _
                                       & "  AND KBN2.KBN_CD = HEAD.SEKY_FLG                                                                      " & vbNewLine _
                                       & "WHERE HEAD.SYS_DEL_FLG = '0'                                                                           " & vbNewLine

#End Region

#Region "ORDERBY"

    ''' <summary>
    ''' ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY HEAD.BATCH_NO,HEAD.CUST_CD_L,HEAD.CUST_CD_M,HEAD.CUST_CD_S,HEAD.CUST_CD_SS"

#End Region

#End Region

#Region "予約取消"

#Region "更新"


    ''' <summary>
    ''' UPDATE文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UPDATE_SQL As String = "	UPDATE $LM_MST$..G_HN_CALC_WK_HEAD 	            " & vbNewLine _
                                        & "	SET	                                            " & vbNewLine _
                                        & "	 EXEC_STATE_KB = '03',	                        " & vbNewLine _
                                        & "	 SYS_UPD_DATE = @SYS_UPD_DATE,	                " & vbNewLine _
                                        & "	 SYS_UPD_TIME = @SYS_UPD_TIME,	                " & vbNewLine _
                                        & "	 SYS_UPD_PGID = @SYS_UPD_PGID,	                " & vbNewLine _
                                        & "	 SYS_UPD_USER = @SYS_UPD_USER	                " & vbNewLine _
                                        & "	WHERE SEKY_FLG = @SEKY_FLG	                    " & vbNewLine _
                                        & "	  AND BATCH_NO = @BATCH_NO	                    " & vbNewLine _
                                        & "	  AND NRS_BR_CD = @NRS_BR_CD	                " & vbNewLine _
                                        & "	  AND OPE_USER_CD = @OPE_USER_CD	            " & vbNewLine _
                                        & "	  AND REC_NO = @REC_NO	                        " & vbNewLine _
                                        & "	  AND SYS_UPD_DATE = @UPD_DATE	                " & vbNewLine _
                                        & "	  AND SYS_UPD_TIME = @UPD_TIME	                " & vbNewLine _
                                        & "	  AND SYS_DEL_FLG = '0'	                        " & vbNewLine

#End Region

#Region "排他SQL"


    ''' <summary>
    ''' 予約取消排他処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA As String = "SELECT COUNT(HEAD.SEKY_FLG) SELECT_CNT                                                   " & vbNewLine _
                                       & "FROM $LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                                " & vbNewLine _
                                       & "WHERE HEAD.SEKY_FLG = @SEKY_FLG                                                         " & vbNewLine _
                                       & "  AND HEAD.BATCH_NO = @BATCH_NO                                                         " & vbNewLine _
                                       & "  AND HEAD.NRS_BR_CD = @NRS_BR_CD                                                       " & vbNewLine _
                                       & "  AND HEAD.OPE_USER_CD = @OPE_USER_CD                                                   " & vbNewLine _
                                       & "  AND HEAD.REC_NO = @REC_NO                                                             " & vbNewLine _
                                       & "  AND HEAD.SYS_UPD_DATE = @UPD_DATE                                                     " & vbNewLine _
                                       & "  AND HEAD.SYS_UPD_TIME = @UPD_TIME                                                     " & vbNewLine _
                                       & "  AND HEAD.SYS_DEL_FLG = '0'                                                            " & vbNewLine

#End Region

#End Region

#Region "処理結果詳細"

    ''' <summary>
    ''' 処理結果詳細ＳＱＬ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_RESULT As String = "SELECT                                                        " & vbNewLine _
                                       & " DETL.JOB_NO AS JOB_NO,                                       " & vbNewLine _
                                       & " DETL.MESSAGE_ID AS MESSAGE_ID,                               " & vbNewLine _
                                       & " DETL.CUST_CD_L + '-' + DETL.CUST_CD_M + '-' + DETL.CUST_CD_S" _
                                       & " + '-' + DETL.CUST_CD_SS AS CUST_CD,                          " & vbNewLine _
                                       & " MESSAG.MESSAGE_STRING AS MESSAGE_STRING,                     " & vbNewLine _
                                       & " 'ＪＯＢ番号' AS KEY_TITLE,                                   " & vbNewLine _
                                       & " DETL.STRING1 AS STRING1,                                     " & vbNewLine _
                                       & " DETL.STRING2 AS STRING2,                                     " & vbNewLine _
                                       & " DETL.STRING3 AS STRING3,                                     " & vbNewLine _
                                       & " DETL.STRING4 AS STRING4,                                     " & vbNewLine _
                                       & " DETL.STRING5 AS STRING5,                                     " & vbNewLine _
                                       & " @ROW_NO AS ROW_NO,                                           " & vbNewLine _
                                       & " @SYS_PGID AS PAGE_ID                                         " & vbNewLine _
                                       & "FROM $LM_MST$..G_HN_CALC_WK_DETL AS DETL                      " & vbNewLine _
                                       & "INNER JOIN $LM_MST$..S_MESSAGE AS MESSAG                      " & vbNewLine _
                                       & " ON DETL.MESSAGE_ID = MESSAG.MESSAGE_ID                       " & vbNewLine _
                                       & "WHERE DETL.CSV_OUT_FLG = '1'                                  " & vbNewLine _
                                       & "  AND DETL.SEKY_FLG = @SEKY_FLG                               " & vbNewLine _
                                       & "  AND DETL.BATCH_NO = @BATCH_NO                               " & vbNewLine _
                                       & "  AND DETL.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                       & "  AND DETL.OPE_USER_CD = @OPE_USER_CD                         " & vbNewLine _
                                       & "  AND DETL.CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                       & "  AND DETL.CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                       & "  AND DETL.CUST_CD_S = @CUST_CD_S                             " & vbNewLine _
                                       & "  AND DETL.CUST_CD_SS = @CUST_CD_SS                           " & vbNewLine


#End Region

#Region "強制実行"

    ''' <summary>
    ''' 強制実行排他処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXECUTE As String = "SELECT                                                                      " & vbNewLine _
                                       & " CASE WHEN HEAD1.CNT1 = HEAD2.CNT1 THEN '1' ELSE '0' END AS CNT              " & vbNewLine _
                                       & "FROM                                                                         " & vbNewLine _
                                       & " (SELECT COUNT(HEAD.SEKY_FLG) AS CNT1                                        " & vbNewLine _
                                       & "  FROM $LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                   " & vbNewLine _
                                       & "  WHERE HEAD.SEKY_FLG = @IN_SEKY_FLG                                         " & vbNewLine _
                                       & "    AND HEAD.BATCH_NO = @IN_BATCH_NO                                         " & vbNewLine _
                                       & "    AND HEAD.NRS_BR_CD = @IN_NRS_BR_CD                                       " & vbNewLine _
                                       & "    AND HEAD.OPE_USER_CD = @IN_OPE_USER_CD                                   " & vbNewLine _
                                       & "    AND HEAD.SYS_DEL_FLG = '0')HEAD1,                                        " & vbNewLine _
                                       & " (SELECT COUNT(HEAD.SEKY_FLG) AS CNT1                                        " & vbNewLine _
                                       & "  FROM $LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                   " & vbNewLine _
                                       & "  WHERE HEAD.SEKY_FLG = @IN_SEKY_FLG                                         " & vbNewLine _
                                       & "    AND HEAD.BATCH_NO = @IN_BATCH_NO                                         " & vbNewLine _
                                       & "    AND HEAD.NRS_BR_CD = @IN_NRS_BR_CD                                       " & vbNewLine _
                                       & "    AND HEAD.OPE_USER_CD = @IN_OPE_USER_CD                                   " & vbNewLine _
                                       & "    AND HEAD.SYS_UPD_DATE = @SYS_UPD_DATE                                    " & vbNewLine _
                                       & "    AND HEAD.SYS_UPD_TIME = @SYS_UPD_TIME                                    " & vbNewLine _
                                       & "    AND HEAD.SYS_DEL_FLG = '0')HEAD2                                         " & vbNewLine


    Private Const ERR_SQL As String = "SELECT MESSAGE_STRING                                                           " & vbNewLine _
                                       & "FROM $LM_MST$..S_MESSAGE                                                     " & vbNewLine _
                                       & "WHERE MESSAGE_ID = 'E011'                                                    " & vbNewLine
#End Region

#Region "強制終了"

    ''' <summary>
    ''' 強制終了
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FORCE_DELETE_1 As String = "DELETE                                                          " & vbNewLine _
                                       & "FROM $LM_MST$..G_HN_CALC_WK_HEAD                                        " & vbNewLine _
                                       & "  WHERE SEKY_FLG = @IN_SEKY_FLG                                         " & vbNewLine _
                                       & "    AND BATCH_NO = @IN_BATCH_NO                                         " & vbNewLine _
                                       & "    AND NRS_BR_CD = @IN_NRS_BR_CD                                       " & vbNewLine _
                                       & "    AND OPE_USER_CD = @IN_OPE_USER_CD                                   " & vbNewLine _
                                       & "    AND SYS_UPD_DATE = @SYS_UPD_DATE                                    " & vbNewLine _
                                       & "    AND SYS_UPD_TIME = @SYS_UPD_TIME                                    " & vbNewLine _
                                       & "    AND EXEC_STATE_KB = '02'                                            " & vbNewLine _
                                       & "    AND SYS_DEL_FLG = '0'                                               " & vbNewLine

    ''' <summary>
    ''' 強制終了2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FORCE_DELETE_2 As String = "DELETE                                                          " & vbNewLine _
                                       & "FROM $LM_MST$..G_HN_CALC_WK_HEAD                                        " & vbNewLine _
                                       & "  WHERE SEKY_FLG = @IN_SEKY_FLG                                         " & vbNewLine _
                                       & "    AND BATCH_NO = @IN_BATCH_NO                                         " & vbNewLine _
                                       & "    AND NRS_BR_CD = @IN_NRS_BR_CD                                       " & vbNewLine _
                                       & "    AND OPE_USER_CD = @IN_OPE_USER_CD                                   " & vbNewLine _
                                       & "    AND EXEC_STATE_KB = '00'                                            " & vbNewLine _
                                       & "    AND SYS_DEL_FLG = '0'                                               " & vbNewLine

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
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_SELECT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG080DAC.SQL_COUNT)     'SQL構築(カウント用SelectCountStart句)
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '20210628 ベトナム対応Add
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20210628 ベトナム対応Add

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_SELECT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成

        'SQL構築(データ抽出用Select句)
        'SQL作成
        Me._StrSql.Append(LMG080DAC.SQL_SELECT)     'SQL構築(カウント用SelectCountStart句)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMG080DAC.SQL_ORDER_BY)   'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "SelectListData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 180

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EXEC_TIMING_KB", "EXEC_TIMING_KB")
        map.Add("EXEC_TIMING_NM", "EXEC_TIMING_NM")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("OPE_USER_CD", "OPE_USER_CD")
        map.Add("USER_NM", "USER_NM")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEKY_DATE", "SEKY_DATE")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("JIKKO_DATE", "JIKKO_DATE")
        map.Add("JIKKO_TIME", "JIKKO_TIME")
        map.Add("SHORI_DATE", "SHORI_DATE")
        map.Add("SHORI_TIME", "SHORI_TIME")
        map.Add("SYURYO_DATE", "SYURYO_DATE")
        map.Add("SYURYO_TIME", "SYURYO_TIME")
        map.Add("EXEC_STATE_NM", "EXEC_STATE_NM")
        map.Add("EXEC_STATE_KB", "EXEC_STATE_KB")
        map.Add("MESSAGE_STRING", "MESSAGE_STRING")
        map.Add("SEKY_FLG", "SEKY_FLG")
        map.Add("SEKY_FLG_NM", "SEKY_FLG_NM")
        map.Add("BATCH_NO", "BATCH_NO")
        map.Add("REC_NO", "REC_NO")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG080OUT")

        Return ds

    End Function

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CancelUpDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG080DAC.UPDATE_SQL)     'SQL構築(カウント用SelectCountStart句)
        Call Me.SetParamUpdate()
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "CancelUpDate", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG080DAC.SQL_HAITA)     'SQL構築(カウント用SelectCountStart句)
        Call Me.SetParamUpdate()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "CheckHaita", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) < 1 = True Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "処理結果詳細"

    ''' <summary>
    ''' 処理結果詳細
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessResults(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_RESULTS")
        For i As Integer = 0 To inTbl.Rows.Count - 1
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()
            'SQL作成

            'SQL構築(データ抽出用Select句)
            'SQL作成
            Me._StrSql.Append(LMG080DAC.SQL_RESULT)     'SQL構築(カウント用SelectCountStart句)
            Call Me.SetResultData()                     'パラメータ設定

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                            Me._StrSql.ToString() _
                                                            , Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG080DAC", "ProcessResults", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'DataReader→DataTableへの転記
            Dim map As Hashtable = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("JOB_NO", "JOB_NO")
            map.Add("CUST_CD", "CUST_CD")
            map.Add("MESSAGE_ID", "MESSAGE_ID")
            map.Add("MESSAGE_STRING", "MESSAGE_STRING")
            map.Add("KEY_TITLE", "KEY_TITLE")
            map.Add("PAGE_ID", "PAGE_ID")
            map.Add("ROW_NO", "ROW_NO")
            map.Add("STRING1", "STRING1")
            map.Add("STRING2", "STRING2")
            map.Add("STRING3", "STRING3")
            map.Add("STRING4", "STRING4")
            map.Add("STRING5", "STRING5")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG080_OUT_RESULTS")
            'INTableの条件rowの格納
            For j As Integer = 0 To ds.Tables("LMG080_OUT_RESULTS").Rows.Count - 1
                Me._Row = ds.Tables("LMG080_OUT_RESULTS").Rows(j)
                MyBase.SetMessageStore("01" _
                   , Me._Row.Item("MESSAGE_ID").ToString() _
                   , New String() {Me._Row.Item("STRING1").ToString() _
                                   , Me._Row.Item("STRING2").ToString() _
                                   , Me._Row.Item("STRING3").ToString() _
                                   , Me._Row.Item("STRING4").ToString() _
                                   , Me._Row.Item("STRING5").ToString()} _
                   , Me._Row.Item("ROW_NO").ToString() _
                   , "荷主コード/JOB番号" _
                   , String.Concat(Me._Row.Item("CUST_CD").ToString(), " ", Me._Row.Item("JOB_NO").ToString()))

            Next
        Next
        Return ds

    End Function

#End Region

#Region "強制実行"

    ''' <summary>
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function CheckExecute(ByVal ds As DataSet) As DataSet

        Dim numList As ArrayList = New ArrayList

        Dim dts As DataSet = ds.Copy

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_RESULTS")

        Dim max As Integer = inTbl.Rows.Count

        For i As Integer = 0 To max - 1

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(LMG080DAC.SQL_EXECUTE)     'SQL構築(カウント用SelectCountStart句)
            Call Me.SetExecuteData()

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                           , Me._Row.Item("NRS_BR_CD").ToString()))
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG080DAC", "CheckExecute", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            '処理結果判定
            reader.Read()
            Dim haita As String = Convert.ToString(reader.Item("CNT"))
            reader.Close()

            If "1".Equals(haita) = True Then

                '保管・荷役料計算処理（LMG800）を呼出す。
                Call Me.CtlCalcHokanNiyaku()
            Else
                MyBase.SetMessageStore("00" _
                           , "E011" _
                           , Nothing _
                           , Me._Row.Item("ROW_NO").ToString() _
                           , "JOB番号" _
                           , Me._Row.Item("JOB_NO").ToString())

            End If

        Next
       
        Return ds

    End Function

    ''' <summary>
    ''' 強制終了1
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>強制終了SQLの構築・発行</remarks>
    Private Function ForceDelete_1(ByVal ds As DataSet) As DataSet

        Dim numList As ArrayList = New ArrayList

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_RESULTS")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG080DAC.SQL_FORCE_DELETE_1)     'SQL構築()
        Call Me.SetExecuteData()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "ForceDelete_1", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 強制終了2
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>強制終了SQLの構築・発行</remarks>
    Private Function ForceDelete_2(ByVal ds As DataSet) As DataSet

        Dim numList As ArrayList = New ArrayList

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG080IN_RESULTS")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG080DAC.SQL_FORCE_DELETE_2)     'SQL構築()
        Call Me.SetExecuteData()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "ForceDelete_2", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 保管料・荷役料計算コントロール呼出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtlCalcHokanNiyaku()

        Dim ErrCode As String = "9"

        Me._StrSql = New StringBuilder()

        Me._StrSql.Append("$LM_TRN$..CTL_CALC_HOKAN_NIYAKU")

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        cmd.CommandType = CommandType.StoredProcedure

        'インプット項目の設定
        '請求フラグ
        Dim SEKY_FLG As SqlParameter = cmd.Parameters.AddWithValue("@IN_SEKY_FLG", DBDataType.CHAR)
        SEKY_FLG.Direction = ParameterDirection.Input
        SEKY_FLG.Value = Me._Row.Item("SEKY_FLG")

        'バッチ番号
        Dim BATCH_NO As SqlParameter = cmd.Parameters.AddWithValue("@IN_BATCH_NO", DBDataType.CHAR)
        BATCH_NO.Direction = ParameterDirection.Input
        BATCH_NO.Value = Me._Row.Item("BATCH_NO")

        '営業所コード
        Dim NRS_BR_CD As SqlParameter = cmd.Parameters.AddWithValue("@IN_NRS_BR_CD", DBDataType.CHAR)
        NRS_BR_CD.Direction = ParameterDirection.Input
        NRS_BR_CD.Value = Me._Row.Item("NRS_BR_CD")

        '実行ユーザーコード
        Dim OPE_USER_CD As SqlParameter = cmd.Parameters.AddWithValue("@IN_OPE_USER_CD", DBDataType.CHAR)
        OPE_USER_CD.Direction = ParameterDirection.Input
        OPE_USER_CD.Value = Me._Row.Item("OPE_USER_CD")

        'パラメータ設定（出力（処理結果））
        Dim OT_RTN_CD As SqlParameter = cmd.Parameters.AddWithValue("@OT_RTN_CD", DBDataType.NVARCHAR)
        OT_RTN_CD.Direction = ParameterDirection.Output
        OT_RTN_CD.Value = String.Empty

        MyBase.Logger.WriteSQLLog("LMG080DAC", "CtlCalcHokanNiyaku", cmd)

        'ストアドプロシージャーの呼び出し
        Dim rtnCd As SqlDataReader = Me.GetSelectResult(cmd)

        '処理結果判定
        If ErrCode.Equals(GetOutPutParam(cmd, "@OT_RTN_CD")) = True Then
            'エラーの場合、メッセージを設定
            MyBase.SetMessage("S001", New String() {"取込"})
        End If

    End Sub

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'バッチ条件
            whereStr = .Item("BATCH_JOKEN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.EXEC_TIMING_KB = @BATCH_JOKEN")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BATCH_JOKEN", whereStr, DBDataType.CHAR))
            End If

            '実行モード（請求フラグ）
            whereStr = .Item("JIKKOU_MODE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.SEKY_FLG = @JIKKOU_MODE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIKKOU_MODE", whereStr, DBDataType.CHAR))
            End If

            '処理状況
            Dim commastr As StringBuilder = New StringBuilder()
            whereStr = .Item("SHORI_MI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                commastr.Append(" AND HEAD.EXEC_STATE_KB IN(@SHORI_MI")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_MI", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("SHORI_CHU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If commastr.Length <> 0 Then
                    commastr.Append(" ,@SHORI_CHU")
                Else
                    commastr.Append(" AND HEAD.EXEC_STATE_KB IN(@SHORI_CHU")
                End If
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_CHU", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("SHORI_ZUMI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If commastr.Length <> 0 Then
                    commastr.Append(" ,@SHORI_ZUMI")
                Else
                    commastr.Append(" AND HEAD.EXEC_STATE_KB IN(@SHORI_ZUMI")
                End If
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_ZUMI", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("SHORI_CANCEL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If commastr.Length <> 0 Then
                    commastr.Append(" ,@SHORI_CANCEL")
                Else
                    commastr.Append(" AND HEAD.EXEC_STATE_KB IN(@SHORI_CANCEL")
                End If
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_CANCEL", whereStr, DBDataType.CHAR))
            End If

            If commastr.Length <> 0 Then
                commastr.Append(")")
                commastr.Append(vbNewLine)
                Me._StrSql.Append(commastr)
            End If

            '指示日（FROM）
            whereStr = .Item("JIKKOU_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.SYS_ENT_DATE >= @JIKKOU_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIKKOU_FROM", whereStr, DBDataType.CHAR))
            End If

            '指示日（TO)
            whereStr = .Item("JIKKOU_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.SYS_ENT_DATE <= @JIKKOU_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIKKOU_TO", whereStr, DBDataType.CHAR))
            End If

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'ユーザー名
            whereStr = .Item("USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUSER.USER_NM LIKE @USER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主コード
            whereStr = .Item("CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.CUST_CD_L + HEAD.CUST_CD_M + HEAD.CUST_CD_S + HEAD.CUST_CD_SS LIKE @CUST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(Replace(whereStr, "-", ""), "%"), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_NM_L + CUST.CUST_NM_M + CUST.CUST_NM_S + CUST.CUST_NM_SS LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEAD.JOB_NO LIKE @JOB_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（更新時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", .Item("SEKY_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BATCH_NO", .Item("BATCH_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OPE_USER_CD", .Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（処理結果詳細）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetResultData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", .Item("SEKY_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BATCH_NO", .Item("BATCH_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OPE_USER_CD", .Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", .Item("ROW_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_PGID", Me.GetPGID(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（強制実行）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExecuteData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_SEKY_FLG", .Item("SEKY_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_BATCH_NO", .Item("BATCH_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_OPE_USER_CD", .Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

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

#Region "言語取得"
    '20210628 ベトナム対応 add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMG080IN_SELECT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG080DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function

    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        If Trim(kbnNm) = "KBN_NM1" Then
            sql = sql.Replace("#MESSAGE_STRING#", "MESSAGE_STRING")
        Else
            sql = sql.Replace("#MESSAGE_STRING#", "MESSAGE_STRING_EN")
        End If

        Return sql

    End Function
    '20210628 ベトナム対応 add End

#End Region

#End Region

End Class
