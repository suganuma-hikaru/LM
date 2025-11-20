' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI680DAC : 保管・荷役明細書(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI680DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI680DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	 SEKY_MEISAI_PRT.NRS_BR_CD                              AS NRS_BR_CD  " & vbNewLine _
                                            & ", 'B9'                                                   AS PTN_ID     " & vbNewLine _
                                            & ", CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	  	  ELSE MR2.PTN_CD END                               AS PTN_CD     " & vbNewLine _
                                            & ", CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                            & "	 	  ELSE MR2.RPT_ID END                               AS RPT_ID     " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                                              " & vbNewLine _
                                            & "      CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                              " & vbNewLine _
                                            & "      ELSE MR2.RPT_ID END                                        AS RPT_ID                                          " & vbNewLine _
                                            & "    , SEKY_MEISAI_PRT.NRS_BR_CD                                  AS NRS_BR_CD                                       " & vbNewLine _
                                            & "    , SEKY_MEISAI_PRT.INV_DATE_FROM                              AS INV_DATE_FROM                                   " & vbNewLine _
                                            & "    , SEKY_MEISAI_PRT.INV_DATE_TO                                AS INV_DATE_TO                                     " & vbNewLine _
                                            & "    , M_CUST.CUST_CD_L                                           AS CUST_CD_L                                       " & vbNewLine _
                                            & "    , M_CUST.CUST_CD_M                                           AS CUST_CD_M                                       " & vbNewLine _
                                            & "    , M_CUST.CUST_CD_S                                           AS CUST_CD_S                                       " & vbNewLine _
                                            & "    , M_CUST.CUST_NM_L                                           AS CUST_NM_L                                       " & vbNewLine _
                                            & "    , ISNULL(GOODS_DTL1.SET_NAIYO,'')                            AS MEIGARA              -- 商品名                  " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB1)                           AS KISYZAN_NB1          -- 一期首残                " & vbNewLine _
                                            & "    , SEKY_MEISAI_PRT.HANDLING_IN1                               AS HANDLING_IN1         -- 一期入庫荷役料単価      " & vbNewLine _
                                            & "    , CASE WHEN TEMP.KEISAN_TLGK IS NOT NULL  AND GOODS_DTL2.SET_NAIYO = '1'    --UPD 2019/05/07                                       " & vbNewLine _
                                            & "           THEN  0                                                                                                  " & vbNewLine _
                                            & "           ELSE  SEKY_MEISAI_PRT.STORAGE1         END            AS STORAGE1             -- 一期保管料単価          " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB2)                           AS KISYZAN_NB2          -- 二期首残                " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB3)                           AS KISYZAN_NB3          -- 三期首残                " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.MATUZAN_NB)                            AS MATUZAN_NB           -- 末残                    " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.INKO_NB_TTL1)                          AS INKO_NB_TTL1         -- 当月入庫高(総数)        " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.INKO_NB_TTL2)                          AS INKO_NB_TTL2         -- 当月入庫高(荷役料なし)  " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.HANDLING_IN_AMO_TTL)                   AS HANDLING_IN_AMO_TTL  -- 入庫荷役料計算額        " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.OUTKO_NB_TTL1)                         AS OUTKO_NB_TTL1        -- 当月出庫高(総数)        " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.OUTKO_NB_TTL2)                         AS OUTKO_NB_TTL2        -- 当月出庫高(荷役料なし)  " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.HANDLING_OUT_AMO_TTL)                  AS HANDLING_OUT_AMO_TTL -- 出庫荷役料計算額        " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.SEKI_ARI_NB1)                                                                             " & vbNewLine _
                                            & "    + SUM(SEKY_MEISAI_PRT.SEKI_ARI_NB2)                                                                             " & vbNewLine _
                                            & "    + SUM(SEKY_MEISAI_PRT.SEKI_ARI_NB3)                          AS SEKISU               -- 積数                    " & vbNewLine _
                                             & "    , CASE WHEN TEMP.KEISAN_TLGK IS NOT NULL  AND GOODS_DTL2.SET_NAIYO = '1'    --UPD 2019/05/07                                       " & vbNewLine _
                                            & "           THEN  0                                                                                                  " & vbNewLine _
                                            & "           ELSE  SUM(SEKY_MEISAI_PRT.STORAGE_AMO_TTL)   END      AS STORAGE_AMO_TTL      -- 保管料合計金額          " & vbNewLine _
                                            & "    , ISNULL(GOODS_DTL2.SET_NAIYO,'0')                           AS ROW_COUNT            -- 行番号                  " & vbNewLine _
                                            & "    , NRS_BR.NRS_BR_NM                                           AS NRS_BR_NM            -- 営業所名                " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB1 * SEKY_MEISAI_PRT.IRIME)   AS KISYZAN_QT1          -- 一期首残数量            " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB2 * SEKY_MEISAI_PRT.IRIME)   AS KISYZAN_QT2          -- 二期首残数量            " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.KISYZAN_NB3 * SEKY_MEISAI_PRT.IRIME)   AS KISYZAN_QT3          -- 三期首残数量            " & vbNewLine _
                                            & "    , SUM(SEKY_MEISAI_PRT.MATUZAN_NB * SEKY_MEISAI_PRT.IRIME)    AS MATUZAN_QT           -- 末残数量                " & vbNewLine _
                                            & "    , SUM((SEKY_MEISAI_PRT.INKO_NB_TTL1 - SEKY_MEISAI_PRT.INKO_NB_TTL2)                                             " & vbNewLine _
                                            & "    * SEKY_MEISAI_PRT.IRIME)                                     AS INKO_NB_TTL_QT       -- 当月入庫高(総数量)      " & vbNewLine _
                                            & "    , SUM((SEKY_MEISAI_PRT.OUTKO_NB_TTL1 - SEKY_MEISAI_PRT.OUTKO_NB_TTL2)                                           " & vbNewLine _
                                            & "    * SEKY_MEISAI_PRT.IRIME)                                     AS OUTKO_NB_TTL_QT      -- 当月出庫高(総数量)      " & vbNewLine _
                                            & "    ,MAX(ISNULL(TEMP.KEISAN_TLGK,0))                             AS STORAGE_AMO_TOU      --テンプレート保管料　ADD 2019/03/28 依頼番号 : 004883" & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                                                           " & vbNewLine _
                                            & "          $LM_TRN$..G_SEKY_MEISAI_PRT SEKY_MEISAI_PRT                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                            " & vbNewLine _
                                            & "       ON SEKY_MEISAI_PRT.NRS_BR_CD = M_GOODS.NRS_BR_CD                                        " & vbNewLine _
                                            & "      AND SEKY_MEISAI_PRT.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST M_CUST                                                              " & vbNewLine _
                                            & "       ON M_GOODS.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
                                            & "      AND M_GOODS.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
                                            & "      AND M_GOODS.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
                                            & "      AND M_GOODS.CUST_CD_S = M_CUST.CUST_CD_S                                                 " & vbNewLine _
                                            & "      AND M_GOODS.CUST_CD_SS = M_CUST.CUST_CD_SS                                               " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODS_DTL1  --商品名                                       " & vbNewLine _
                                            & "       ON SEKY_MEISAI_PRT.NRS_BR_CD = GOODS_DTL1.NRS_BR_CD                                     " & vbNewLine _
                                            & "      AND SEKY_MEISAI_PRT.GOODS_CD_NRS = GOODS_DTL1.GOODS_CD_NRS                               " & vbNewLine _
                                            & "      AND GOODS_DTL1.SUB_KB = '26'                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODS_DTL2  --行番号                                       " & vbNewLine _
                                            & "       ON SEKY_MEISAI_PRT.NRS_BR_CD = GOODS_DTL2.NRS_BR_CD                                     " & vbNewLine _
                                            & "      AND SEKY_MEISAI_PRT.GOODS_CD_NRS = GOODS_DTL2.GOODS_CD_NRS                               " & vbNewLine _
                                            & "      AND GOODS_DTL2.SUB_KB = '27'                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR NRS_BR                                                            " & vbNewLine _
                                            & "       ON SEKY_MEISAI_PRT.NRS_BR_CD = NRS_BR.NRS_BR_CD                                         " & vbNewLine _
                                            & "LEFT JOIN                                                                                      " & vbNewLine _
                                            & "  $LM_MST$..M_CUST_RPT MCR1                                                                    " & vbNewLine _
                                            & "  ON M_GOODS.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                            & " AND M_GOODS.CUST_CD_L = MCR1.CUST_CD_L                                                        " & vbNewLine _
                                            & " AND M_GOODS.CUST_CD_M = MCR1.CUST_CD_M                                                        " & vbNewLine _
                                            & " AND MCR1.PTN_ID    = 'B9'                                                                     " & vbNewLine _
                                            & "LEFT JOIN                                                                                      " & vbNewLine _
                                            & "   $LM_MST$..M_RPT MR1                                                                         " & vbNewLine _
                                            & "  ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                            " & vbNewLine _
                                            & " AND MR1.PTN_ID = MCR1.PTN_ID                                                                  " & vbNewLine _
                                            & " AND MR1.PTN_CD = MCR1.PTN_CD                                                                  " & vbNewLine _
                                            & " AND MR1.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                            & "--帳票マスタから取得                                                                           " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                  " & vbNewLine _
                                            & "  ON MR2.NRS_BR_CD = SEKY_MEISAI_PRT.NRS_BR_CD                                                 " & vbNewLine _
                                            & " AND MR2.PTN_ID = 'B9'                                                                         " & vbNewLine _
                                            & " AND MR2.STANDARD_FLAG = '01'                                                                  " & vbNewLine _
                                            & " AND MR2.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                            & "--請求テンプレートマスタから取得 ADD 2019/03/28 依頼番号 : 004883                              " & vbNewLine _
                                            & "LEFT JOIN LM_MST..M_SEIQ_TEMPLATE TEMP                                                         " & vbNewLine _
                                            & "     ON TEMP.NRS_BR_CD   = @NRS_BR_CD                                                          " & vbNewLine _
                                            & "  AND TEMP.SEIQTO_CD   = @SEIQTO_CD                                                            " & vbNewLine _
                                            & "  AND TEMP.SEIQKMK_CD  = '01'  --保管料                                                        " & vbNewLine _
                                            & "  AND TEMP.SYS_DEL_FLG = '0'                                                                   " & vbNewLine

#End Region

#Region "WHERE句"

    ''' <summary>
    ''' 印刷データ抽出用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE As String = "WHERE                                                                                         " & vbNewLine _
                                             & "         SEKY_MEISAI_PRT.INV_DATE_FROM = @F_DATE                                              " & vbNewLine _
                                             & "     AND SEKY_MEISAI_PRT.INV_DATE_TO   = @T_DATE                                              " & vbNewLine _
                                             & "     AND M_CUST.CUST_CD_L = @CUST_CD_L                                                        " & vbNewLine _
                                             & "     AND M_CUST.CUST_CD_S = @CUST_CD_S                                                        " & vbNewLine

#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 印刷データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                                " & vbNewLine _
                                         & "      CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                         & "      ELSE MR2.RPT_ID END                               " & vbNewLine _
                                         & "    , SEKY_MEISAI_PRT.NRS_BR_CD                         " & vbNewLine _
                                         & "    , SEKY_MEISAI_PRT.INV_DATE_FROM                     " & vbNewLine _
                                         & "    , SEKY_MEISAI_PRT.INV_DATE_TO                       " & vbNewLine _
                                         & "    , M_CUST.CUST_CD_L                                  " & vbNewLine _
                                         & "    , M_CUST.CUST_CD_M                                  " & vbNewLine _
                                         & "    , M_CUST.CUST_CD_S                                  " & vbNewLine _
                                         & "    , M_CUST.CUST_NM_L                                  " & vbNewLine _
                                         & "    , SEKY_MEISAI_PRT.HANDLING_IN1                      " & vbNewLine _
                                         & "    , SEKY_MEISAI_PRT.STORAGE1                          " & vbNewLine _
                                         & "    , GOODS_DTL1.SET_NAIYO                              " & vbNewLine _
                                         & "    , GOODS_DTL2.SET_NAIYO                              " & vbNewLine _
                                         & "    , NRS_BR.NRS_BR_NM                                  " & vbNewLine _
                                         & "    , TEMP.KEISAN_TLGK                                  " & vbNewLine

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                  " & vbNewLine _
                                         & "    GOODS_DTL2.SET_NAIYO  " & vbNewLine

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI680IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_MPRT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用WHERE句)
        Call Me.SetSQLSelectWhere()                       'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI680DAC", "SelectMPRT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        '処理件数の設定
        If ds.Tables("M_RPT").Rows.Count < 1 Then
            MyBase.SetMessage("G021")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI680IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI680DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用WHERE句)
        Me._StrSql.Append(LMI680DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMI680DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)
        Call Me.SetSQLSelectWhere()                       'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI680DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INV_DATE_FROM", "INV_DATE_FROM")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("MEIGARA", "MEIGARA")
        map.Add("KISYZAN_NB1", "KISYZAN_NB1")
        map.Add("HANDLING_IN1", "HANDLING_IN1")
        map.Add("STORAGE1", "STORAGE1")
        map.Add("KISYZAN_NB2", "KISYZAN_NB2")
        map.Add("KISYZAN_NB3", "KISYZAN_NB3")
        map.Add("MATUZAN_NB", "MATUZAN_NB")
        map.Add("INKO_NB_TTL1", "INKO_NB_TTL1")
        map.Add("INKO_NB_TTL2", "INKO_NB_TTL2")
        map.Add("HANDLING_IN_AMO_TTL", "HANDLING_IN_AMO_TTL")
        map.Add("OUTKO_NB_TTL1", "OUTKO_NB_TTL1")
        map.Add("OUTKO_NB_TTL2", "OUTKO_NB_TTL2")
        map.Add("HANDLING_OUT_AMO_TTL", "HANDLING_OUT_AMO_TTL")
        map.Add("SEKISU", "SEKISU")
        map.Add("STORAGE_AMO_TTL", "STORAGE_AMO_TTL")
        map.Add("ROW_COUNT", "ROW_COUNT")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("KISYZAN_QT1", "KISYZAN_QT1")
        map.Add("KISYZAN_QT2", "KISYZAN_QT2")
        map.Add("KISYZAN_QT3", "KISYZAN_QT3")
        map.Add("MATUZAN_QT", "MATUZAN_QT")
        map.Add("INKO_NB_TTL_QT", "INKO_NB_TTL_QT")
        map.Add("OUTKO_NB_TTL_QT", "OUTKO_NB_TTL_QT")
        map.Add("STORAGE_AMO_TOU", "STORAGE_AMO_TOU")   'ADD 2019/03/28　依頼番号 : 004883 

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI680OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectWhere()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", Me._Row.Item("CUST_CD_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))

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

#End Region

End Class
