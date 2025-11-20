' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC721    : ANA改定
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC721DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC721DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                       " & vbNewLine _
                                            & "        OUTKA_L.NRS_BR_CD                                AS NRS_BR_CD  " & vbNewLine _
                                            & "      , 'AQ'                                             AS PTN_ID     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
                                            & "        END                                              AS PTN_CD     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
                                            & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                          " & vbNewLine _
                                            & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID         " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID         " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID                                          " & vbNewLine _
                                            & "        END                         AS RPT_ID                    " & vbNewLine _
                                            & "      , OUTKA_L.OUTKA_NO_L          AS OUTKA_NO_L                " & vbNewLine _
                                            & "      , OUTKA_L.OUTKA_PLAN_DATE     AS OUTKA_PLAN_DATE           " & vbNewLine _
                                            & "      , OUTKA_L.CUST_CD_L           AS CUST_CD_L                 " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_L            AS CUST_NM_L                 " & vbNewLine _
                                            & "      , M_CUST.AD_1                 AS CUST_AD_1                 " & vbNewLine _
                                            & "      , M_CUST.AD_2                 AS CUST_AD_2                 " & vbNewLine _
                                            & "      , M_CUST.AD_3                 AS CUST_AD_3                 " & vbNewLine _
                                            & "      , OUTKA_L.DEST_CD             AS DEST_CD                   " & vbNewLine _
                                            & "      , CASE WHEN OUTKA_L.DEST_KB = '01' THEN OUTKA_L.DEST_NM    " & vbNewLine _
                                            & "             ELSE M_DEST.DEST_NM                                 " & vbNewLine _
                                            & "        END                         AS DEST_NM                   " & vbNewLine _
                                            & "      , CASE WHEN OUTKA_L.DEST_KB = '01' THEN OUTKA_L.DEST_AD_1  " & vbNewLine _
                                            & "             ELSE M_DEST.AD_1                                    " & vbNewLine _
                                            & "        END                         AS DEST_AD_1                 " & vbNewLine _
                                            & "      , CASE WHEN OUTKA_L.DEST_KB = '01' THEN OUTKA_L.DEST_AD_2  " & vbNewLine _
                                            & "             ELSE M_DEST.AD_2                                    " & vbNewLine _
                                            & "        END                         AS DEST_AD_2                 " & vbNewLine _
                                            & "      , OUTKA_L.DEST_AD_3           AS DEST_AD_3                 " & vbNewLine _
                                            & "      , OUTKA_L.DEST_TEL            AS DEST_TEL                  " & vbNewLine _
                                            & "      , OUTKA_M.ALCTD_NB            AS ALCTD_NB                  " & vbNewLine _
                                            & "      , OUTKA_M.GOODS_CD_NRS        AS GOODS_CD_NRS              " & vbNewLine _
                                            & "      , M_GOODS.GOODS_CD_CUST       AS GOODS_CD_CUST             " & vbNewLine _
                                            & "      , M_GOODS.GOODS_NM_1          AS GOODS_NM_1                " & vbNewLine _
                                            & "      , M_GOODS.GOODS_NM_2          AS GOODS_NM_2                " & vbNewLine _
                                            & "      , M_GOODS.GOODS_NM_3          AS GOODS_NM_3   --INTO DATA  " & vbNewLine _
                                            & "      , M_GOODS.STD_WT_KGS          AS STD_WT_KGS                " & vbNewLine _
                                            & "      , SUM_OUTKA_M.OUTKA_TTL_NB    AS OUTKA_TTL_NB              " & vbNewLine



    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " --出荷(大)                                                        " & vbNewLine _
                                     & " FROM $LM_TRN$..C_OUTKA_L OUTKA_L                                  " & vbNewLine _
                                     & "      --出荷(中)                                                   " & vbNewLine _
                                     & "      INNER JOIN $LM_TRN$..C_OUTKA_M OUTKA_M                       " & vbNewLine _
                                     & "              ON OUTKA_M.NRS_BR_CD  = OUTKA_L.NRS_BR_CD            " & vbNewLine _
                                     & "             AND OUTKA_M.OUTKA_NO_L = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
                                     & "             AND OUTKA_M.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                     & "       --届先Ｍ                                                    " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_DEST M_DEST                             " & vbNewLine _
                                     & "              ON M_DEST.NRS_BR_CD = OUTKA_L.NRS_BR_CD              " & vbNewLine _
                                     & "             AND M_DEST.DEST_CD   = OUTKA_L.DEST_CD                " & vbNewLine _
                                     & "             AND M_DEST.CUST_CD_L = OUTKA_L.CUST_CD_L              " & vbNewLine _
                                     & "--             AND M_DEST.AD_3 Like '%ANA%'                          " & vbNewLine _
                                     & "--       LEFT JOIN LM_MST..M_DEST DEST1                              " & vbNewLine _
                                     & "--              ON DEST1.DEST_CD = OUTKA_L.DEST_CD                   " & vbNewLine _
                                     & "       --荷主Ｍ                                                    " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_CUST M_CUST                             " & vbNewLine _
                                     & "              ON M_CUST.NRS_BR_CD  = OUTKA_L.NRS_BR_CD             " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_L  = OUTKA_L.CUST_CD_L             " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_M  = '00'                          " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_S  = '00'                          " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_SS = '00'                          " & vbNewLine _
                                     & "       --商品Ｍ                                                    " & vbNewLine _
                                     & "      INNER JOIN LM_MST..M_GOODS M_GOODS                           " & vbNewLine _
                                     & "              ON M_GOODS.NRS_BR_CD    = OUTKA_M.NRS_BR_CD          " & vbNewLine _
                                     & "             AND M_GOODS.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS       " & vbNewLine _
                                     & "       --荷主での荷主帳票パターン取得                              " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_CUST_RPT MCR1                           " & vbNewLine _
                                     & "              ON MCR1.NRS_BR_CD = OUTKA_L.NRS_BR_CD                " & vbNewLine _
                                     & "             AND MCR1.CUST_CD_L = OUTKA_L.CUST_CD_L                " & vbNewLine _
                                     & "             AND MCR1.CUST_CD_M = OUTKA_L.CUST_CD_M                " & vbNewLine _
                                     & "             AND MCR1.CUST_CD_S = '00'                             " & vbNewLine _
                                     & "             AND MCR1.PTN_ID    = 'AQ'                             " & vbNewLine _
                                     & "       --帳票パターン取得                                          " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_RPT MR1                                 " & vbNewLine _
                                     & "              ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                    " & vbNewLine _
                                     & "             AND MR1.PTN_ID = MCR1.PTN_ID                          " & vbNewLine _
                                     & "             AND MR1.PTN_CD = MCR1.PTN_CD                          " & vbNewLine _
                                     & "             AND MR1.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                     & "       --商品Mの荷主での荷主帳票パターン取得                       " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_CUST_RPT MCR2                           " & vbNewLine _
                                     & "              ON MCR2.NRS_BR_CD = M_GOODS.NRS_BR_CD                " & vbNewLine _
                                     & "             AND MCR2.CUST_CD_L = M_GOODS.CUST_CD_L                " & vbNewLine _
                                     & "             AND MCR2.CUST_CD_M = M_GOODS.CUST_CD_M                " & vbNewLine _
                                     & "             AND MCR2.CUST_CD_S = M_GOODS.CUST_CD_S                " & vbNewLine _
                                     & "             AND MCR2.PTN_ID = 'AQ'                                " & vbNewLine _
                                     & "       --帳票パターン取得                                          " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_RPT MR2                                 " & vbNewLine _
                                     & "              ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                    " & vbNewLine _
                                     & "             AND MR2.PTN_ID = MCR2.PTN_ID                          " & vbNewLine _
                                     & "             AND MR2.PTN_CD = MCR2.PTN_CD                          " & vbNewLine _
                                     & "             AND MR2.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                     & "       --存在しない場合の帳票パターン取得                          " & vbNewLine _
                                     & "       LEFT JOIN LM_MST..M_RPT MR3                                 " & vbNewLine _
                                     & "              ON MR3.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
                                     & "             AND MR3.PTN_ID = 'AQ'                                 " & vbNewLine _
                                     & "             AND MR3.STANDARD_FLAG = '01'                          " & vbNewLine _
                                     & "             AND MR3.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                     & "       LEFT JOIN                                                   " & vbNewLine _
                                     & "            (SELECT                                                " & vbNewLine _
                                     & "               C_OUTKA_M.NRS_BR_CD                                 " & vbNewLine _
                                     & "               ,C_OUTKA_M.OUTKA_NO_L                               " & vbNewLine _
                                     & "               ,MIN(C_OUTKA_M.OUTKA_NO_M) AS OUTKA_NO_M            " & vbNewLine _
                                     & "               ,SUM(C_OUTKA_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB        " & vbNewLine _
                                     & "               ,SUM(C_OUTKA_M.BACKLOG_NB) AS BACKLOG_NB            " & vbNewLine _
                                     & "               ,SUM(C_OUTKA_M.BACKLOG_QT) AS BACKLOG_QT            " & vbNewLine _
                                     & "               ,MIN(C_OUTKA_M.ALCTD_NB) AS MIN_ALCTD_NB            " & vbNewLine _
                                     & "               ,MIN(C_OUTKA_M.ALCTD_QT) AS MIN_ALCTD_QT            " & vbNewLine _
                                     & "               ,MIN(C_OUTKA_M.LOT_NO) AS LOT_NO                    " & vbNewLine _
                                     & "               ,MIN(C_OUTKA_M.SERIAL_NO) AS SERIAL_NO              " & vbNewLine _
                                     & "               ,C_OUTKA_M.SYS_DEL_FLG                              " & vbNewLine _
                                     & "               FROM                                                " & vbNewLine _
                                     & "            $LM_TRN$..C_OUTKA_M     C_OUTKA_M   -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                     & "            WHERE                                                  " & vbNewLine _
                                     & "               C_OUTKA_M.NRS_BR_CD = @NRS_BR_CD  --'10' UPD 2020/06/05 012672 " & vbNewLine _
                                     & "            AND                                                    " & vbNewLine _
                                     & "               C_OUTKA_M.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                     & "            GROUP BY                                               " & vbNewLine _
                                     & "               C_OUTKA_M.NRS_BR_CD                                 " & vbNewLine _
                                     & "               ,C_OUTKA_M.OUTKA_NO_L                               " & vbNewLine _
                                     & "               ,C_OUTKA_M.SYS_DEL_FLG                              " & vbNewLine _
                                     & "            )       SUM_OUTKA_M                                    " & vbNewLine _
                                     & "       ON                                                          " & vbNewLine _
                                     & "        OUTKA_L.NRS_BR_CD =SUM_OUTKA_M.NRS_BR_CD                   " & vbNewLine _
                                     & "       AND                                                         " & vbNewLine _
                                     & "        OUTKA_L.OUTKA_NO_L =SUM_OUTKA_M.OUTKA_NO_L                 " & vbNewLine


    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、③印刷順番、④管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                     " & vbNewLine _
                                         & "      OUTKA_L.NRS_BR_CD      " & vbNewLine _
                                         & "    , OUTKA_L.OUTKA_NO_L     " & vbNewLine _
                                         & "    , OUTKA_M.PRINT_SORT     " & vbNewLine _
                                         & "    , OUTKA_M.OUTKA_NO_M     " & vbNewLine

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC721IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC721DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC721DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC721DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function


    ''' <summary>
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC721IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC721DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC721DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMC721DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC721DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_AD_1", "CUST_AD_1")
        map.Add("CUST_AD_2", "CUST_AD_2")
        map.Add("CUST_AD_3", "CUST_AD_3")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC721OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日
            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))
            End If

            '"ANA"を含むこと
            Me._StrSql.Append(" AND OUTKA_L.DEST_AD_3 LIKE '%ANA%' ")
            Me._StrSql.Append(vbNewLine)

        End With

    End Sub

#End Region

#Region "設定処理"

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

#End Region

End Class
