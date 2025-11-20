' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMH060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "検索対象データの検索"

#Region "検索対象データの検索 SQL SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(OUTEDIL.NRS_BR_CD)		            AS SELECT_CNT          " & vbNewLine

    ''' <summary>
    ''' 検索対象データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENDATA As String = " SELECT                                                                  " & vbNewLine _
                                               & " OUTEDIL.NRS_BR_CD                     AS NRS_BR_CD                      " & vbNewLine _
                                               & ",SUBSTRING(OUTEDIL.FREE_C19,1,5)       AS CUST_CD_L                      " & vbNewLine _
                                               & ",SUBSTRING(OUTEDIL.FREE_C19,6,2)       AS CUST_CD_M                      " & vbNewLine _
                                               & ",ISNULL(CUST.CUST_NM_L,'荷主不明')     AS CUST_NM_L                      " & vbNewLine _
                                               & ",OUTEDIL.EDI_CTL_NO                    AS EDI_CTL_NO                     " & vbNewLine _
                                               & ",OUTEDIL.OUTKA_PLAN_DATE               AS OUTKA_PLAN_DATE                " & vbNewLine _
                                               & ",OUTEDIL.DEST_CD                       AS DEST_CD                        " & vbNewLine _
                                               & ",OUTEDIL.DEST_NM                       AS DEST_NM                        " & vbNewLine _
                                               & ",OUTEDIL.FREE_C02                      AS ZBUKACD                        " & vbNewLine _
                                               & ",OUTEDIL.FREE_C20                      AS CUST_CD_UPD                    " & vbNewLine _
                                               & ",ISNULL(EDICUST.RCV_NM_HED,'')         AS RCV_NM_HED                     " & vbNewLine _
                                               & ",OUTEDIL.SYS_UPD_DATE                  AS SYS_UPD_DATE                   " & vbNewLine _
                                               & ",OUTEDIL.SYS_UPD_TIME                  AS SYS_UPD_TIME                   " & vbNewLine

#End Region

#Region "検索対象データの検索 SQL FROM句"

    ''' <summary>
    ''' 検索対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENDATA As String = "FROM                                                               " & vbNewLine _
                                                    & "$LM_TRN$..H_OUTKAEDI_L OUTEDIL                                     " & vbNewLine _
                                                    & "LEFT JOIN                                                          " & vbNewLine _
                                                    & "$LM_MST$..M_CUST CUST                                              " & vbNewLine _
                                                    & "ON                                                                 " & vbNewLine _
                                                    & "CUST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                 " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "CUST.CUST_CD_L = SUBSTRING(OUTEDIL.FREE_C19,1,5)                   " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "CUST.CUST_CD_M = SUBSTRING(OUTEDIL.FREE_C19,6,2)                   " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "CUST.CUST_CD_S = '00'                                              " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "CUST.CUST_CD_SS = '00'                                             " & vbNewLine _
                                                    & "LEFT JOIN                                                          " & vbNewLine _
                                                    & "$LM_MST$..M_EDI_CUST EDICUST                                       " & vbNewLine _
                                                    & "ON                                                                 " & vbNewLine _
                                                    & "EDICUST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                              " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "EDICUST.CUST_CD_L = SUBSTRING(OUTEDIL.FREE_C19,1,5)                " & vbNewLine _
                                                    & "AND                                                                " & vbNewLine _
                                                    & "EDICUST.CUST_CD_M = SUBSTRING(OUTEDIL.FREE_C19,6,2)                " & vbNewLine


#End Region

#Region "検索対象データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 検索対象データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENDATA As String = "ORDER BY                                                          " & vbNewLine _
                                                     & " OUTEDIL.FREE_C19                                                 " & vbNewLine _
                                                     & ",OUTEDIL.EDI_CTL_NO                                               " & vbNewLine

#End Region

#End Region

#Region "EDI出荷データの更新(荷主セット処理)"

    ''' <summary>
    ''' EDI出荷データの更新(荷主セット処理) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_NINUSHISET As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET                              " & vbNewLine _
                                                  & "                  FREE_C19             = @CUST_CD              " & vbNewLine _
                                                  & "                 ,UPD_USER             = @SYS_UPD_USER         " & vbNewLine _
                                                  & "                 ,UPD_DATE             = @SYS_UPD_DATE         " & vbNewLine _
                                                  & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                                  & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                                  & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                                  & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                                  & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                                  & "            WHERE                                              " & vbNewLine _
                                                  & "                  NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                                  & "              AND EDI_CTL_NO           = @EDI_CTL_NO           " & vbNewLine _
                                                  & "              AND CUST_CD_L            = @CUST_CD_LX           " & vbNewLine _
                                                  & "              AND CUST_CD_M            = @CUST_CD_MX           " & vbNewLine _
                                                  & "              AND FREE_C20             = @CUST_CD_L_UPD        " & vbNewLine _
                                                  & "              AND SYS_UPD_DATE         = @GUI_UPD_DATE         " & vbNewLine _
                                                  & "              AND SYS_UPD_TIME         = @GUI_UPD_TIME         " & vbNewLine _
                                                  & "              AND DEL_KB               = '0'                   " & vbNewLine

#End Region

#Region "EDI出荷データの更新(キャンセル処理)"

    ''' <summary>
    ''' EDI出荷データの更新(キャンセル処理) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CANCEL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET                              " & vbNewLine _
                                              & "                  FREE_C19             = @CUST_CD              " & vbNewLine _
                                              & "                 ,UPD_USER             = @SYS_UPD_USER         " & vbNewLine _
                                              & "                 ,UPD_DATE             = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                              & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                              & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                              & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                              & "            WHERE                                              " & vbNewLine _
                                              & "                  NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                              & "              AND EDI_CTL_NO           = @EDI_CTL_NO           " & vbNewLine _
                                              & "              AND CUST_CD_L            = @CUST_CD_LX           " & vbNewLine _
                                              & "              AND CUST_CD_M            = @CUST_CD_MX           " & vbNewLine _
                                              & "              AND FREE_C20             = @CUST_CD_L_UPD        " & vbNewLine _
                                              & "              AND SYS_UPD_DATE         = @GUI_UPD_DATE         " & vbNewLine _
                                              & "              AND SYS_UPD_TIME         = @GUI_UPD_TIME         " & vbNewLine _
                                              & "              AND DEL_KB               = '0'                   " & vbNewLine

#End Region

#Region "DIC出荷EDI受信データヘッダ更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データヘッダ更新(登録処理) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DICHED As String = "UPDATE $LM_TRN$..H_OUTKAEDI_HED_DIC SET                        " & vbNewLine _
                                              & "                  CUST_CD_L            = @CUST_CD_L_EDIL       " & vbNewLine _
                                              & "                 ,CUST_CD_M            = @CUST_CD_M_EDIL       " & vbNewLine _
                                              & "                 ,UPD_USER             = @SYS_UPD_USER         " & vbNewLine _
                                              & "                 ,UPD_DATE             = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                              & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                              & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                              & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                              & "            WHERE                                              " & vbNewLine _
                                              & "                  NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                              & "              AND EDI_CTL_NO           = @EDI_CTL_NO           " & vbNewLine _
                                              & "              AND DEL_KB               = '0'                   " & vbNewLine

#End Region

#Region "DIC出荷EDI受信データ明細更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データ明細更新(登録処理) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DICDTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_DIC SET                        " & vbNewLine _
                                              & "                  CUST_CD_L            = @CUST_CD_L_EDIL       " & vbNewLine _
                                              & "                 ,CUST_CD_M            = @CUST_CD_M_EDIL       " & vbNewLine _
                                              & "                 ,UPD_USER             = @SYS_UPD_USER         " & vbNewLine _
                                              & "                 ,UPD_DATE             = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                              & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                              & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                              & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                              & "            WHERE                                              " & vbNewLine _
                                              & "                  NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                              & "              AND EDI_CTL_NO           = @EDI_CTL_NO           " & vbNewLine _
                                              & "              AND DEL_KB               = '0'                   " & vbNewLine

#End Region

#Region "EDI出荷データの更新(登録処理)"

    ''' <summary>
    ''' EDI出荷データの更新(キャンセル処理) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HOZONDIC As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET                              " & vbNewLine _
                                                & "                  CUST_CD_L            = @CUST_CD_L_EDIL       " & vbNewLine _
                                                & "                 ,CUST_CD_M            = @CUST_CD_M_EDIL       " & vbNewLine _
                                                & "                 ,UPD_USER             = @SYS_UPD_USER         " & vbNewLine _
                                                & "                 ,UPD_DATE             = @SYS_UPD_DATE         " & vbNewLine _
                                                & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                                & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                                & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                                & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                                & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                                & "            WHERE                                              " & vbNewLine _
                                                & "                  NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                                & "              AND EDI_CTL_NO           = @EDI_CTL_NO           " & vbNewLine _
                                                & "              AND SYS_UPD_DATE         = @GUI_UPD_DATE         " & vbNewLine _
                                                & "              AND SYS_UPD_TIME         = @GUI_UPD_TIME         " & vbNewLine _
                                                & "              AND DEL_KB               = '0'                   " & vbNewLine

#End Region

#Region "DIC日陸荷主テーブルの新規追加(登録処理)"

    ''' <summary>
    ''' DIC日陸荷主テーブルの新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DICCUST As String = "INSERT INTO $LM_TRN$..H_NRSCUST_DIC                  " & vbNewLine _
                                               & " ( 		                                           " & vbNewLine _
                                               & " DEL_KB,                                             " & vbNewLine _
                                               & " ZBUKACD,                                            " & vbNewLine _
                                               & " NRS_BR_CD,                                          " & vbNewLine _
                                               & " WH_CD,                                              " & vbNewLine _
                                               & " CUST_CD_L,                                          " & vbNewLine _
                                               & " CUST_CD_M,                                          " & vbNewLine _
                                               & " CRT_USER,                                           " & vbNewLine _
                                               & " CRT_DATE,                                           " & vbNewLine _
                                               & " CRT_TIME,                                           " & vbNewLine _
                                               & " UPD_USER,                                           " & vbNewLine _
                                               & " UPD_DATE,                                           " & vbNewLine _
                                               & " UPD_TIME,                                           " & vbNewLine _
                                               & " SYS_ENT_DATE,                                       " & vbNewLine _
                                               & " SYS_ENT_TIME,                                       " & vbNewLine _
                                               & " SYS_ENT_PGID,                                       " & vbNewLine _
                                               & " SYS_ENT_USER,                                       " & vbNewLine _
                                               & " SYS_UPD_DATE,                                       " & vbNewLine _
                                               & " SYS_UPD_TIME,                                       " & vbNewLine _
                                               & " SYS_UPD_PGID,                                       " & vbNewLine _
                                               & " SYS_UPD_USER,                                       " & vbNewLine _
                                               & " SYS_DEL_FLG                                         " & vbNewLine _
                                               & " )                                                   " & vbNewLine _
                                               & " SELECT                                              " & vbNewLine _
                                               & " OUTEDIL.DEL_KB         AS DEL_KB,                   " & vbNewLine _
                                               & " OUTEDIL.FREE_C02       AS ZBUKACD,                  " & vbNewLine _
                                               & " OUTEDIL.NRS_BR_CD      AS NRS_BR_CD,                " & vbNewLine _
                                               & " OUTEDIL.WH_CD          AS WH_CD,                    " & vbNewLine _
                                               & " OUTEDIL.CUST_CD_L      AS CUST_CD_L,                " & vbNewLine _
                                               & " OUTEDIL.CUST_CD_M      AS CUST_CD_M,                " & vbNewLine _
                                               & " @SYS_ENT_USER          AS CRT_USER,                 " & vbNewLine _
                                               & " @SYS_ENT_DATE          AS CRT_DATE,                 " & vbNewLine _
                                               & " @UPD_TIME              AS CRT_TIME,                 " & vbNewLine _
                                               & " @SYS_ENT_USER          AS UPD_USER,                 " & vbNewLine _
                                               & " @SYS_ENT_DATE          AS UPD_DATE,                 " & vbNewLine _
                                               & " @UPD_TIME              AS UPD_TIME,                 " & vbNewLine _
                                               & " @SYS_ENT_DATE          AS SYS_ENT_DATE,             " & vbNewLine _
                                               & " @SYS_ENT_TIME          AS SYS_ENT_TIME,             " & vbNewLine _
                                               & " @SYS_ENT_PGID          AS SYS_ENT_PGID,             " & vbNewLine _
                                               & " @SYS_ENT_USER          AS SYS_ENT_USER,             " & vbNewLine _
                                               & " @SYS_UPD_DATE          AS SYS_UPD_DATE,             " & vbNewLine _
                                               & " @SYS_UPD_TIME          AS SYS_UPD_TIME,             " & vbNewLine _
                                               & " @SYS_UPD_PGID          AS SYS_UPD_PGID,             " & vbNewLine _
                                               & " @SYS_UPD_USER          AS SYS_UPD_USER,             " & vbNewLine _
                                               & " @SYS_DEL_FLG           AS SYS_DEL_FLG               " & vbNewLine _
                                               & " FROM $LM_TRN$..H_OUTKAEDI_L OUTEDIL                 " & vbNewLine _
                                               & " LEFT JOIN $LM_TRN$..H_NRSCUST_DIC NRSCUST           " & vbNewLine _
                                               & " ON                                                  " & vbNewLine _
                                               & " OUTEDIL.FREE_C02 = NRSCUST.ZBUKACD                  " & vbNewLine _
                                               & " WHERE                                               " & vbNewLine _
                                               & "       OUTEDIL.DEL_KB = '0'                          " & vbNewLine _
                                               & "   AND OUTEDIL.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                                               & "   AND OUTEDIL.EDI_CTL_NO = @EDI_CTL_NO              " & vbNewLine _
                                               & "   AND NRSCUST.ZBUKACD IS NULL                       " & vbNewLine

#End Region

#End Region

#Region "Field"

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
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "検索対象データのデータ件数検索"

    ''' <summary>
    ''' 検索対象データのデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_SELECT_COUNT)         'SQL構築 SELECT句
        Me._StrSql.Append(LMH060DAC.SQL_SELECT_FROM_KENDATA)  'SQL構築 FROM句
        Call SetSQLWhereData(inTbl.Rows(0))                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "SelectKensakuData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索対象データの検索"

    ''' <summary>
    ''' 検索対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_SELECT_KENDATA)       'SQL構築 SELECT句
        Me._StrSql.Append(LMH060DAC.SQL_SELECT_FROM_KENDATA)  'SQL構築 FROM句
        Call SetSQLWhereData(inTbl.Rows(0))                   '条件設定
        Me._StrSql.Append(LMH060DAC.SQL_SELECT_ORDER_KENDATA) 'SQL構築 ORDER句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "SelectKensakuData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("ZBUKACD", "ZBUKACD")
        map.Add("CUST_CD_UPD", "CUST_CD_UPD")
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH060OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "EDI出荷データの更新(荷主セット処理)"

    ''' <summary>
    ''' EDI出荷データの更新(荷主セット処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateNinushiSet(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_UPDATE_NINUSHISET) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdNinushiSetParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "UpdateNinushiSet", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "EDI出荷データの更新(キャンセル処理)"

    ''' <summary>
    ''' EDI出荷データの更新(キャンセル処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_UPDATE_CANCEL) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdCancelParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "UpdateCancel", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "DIC出荷EDI受信データヘッダ更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データヘッダ更新(登録処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonDicHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_UPDATE_DICHED) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdDicHedParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "UpdateHozonDicHed", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "DIC出荷EDI受信データ明細更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データ明細更新(登録処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonDicDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_UPDATE_DICDTL) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdDicDtlParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "UpdateHozonDicDtl", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "EDI出荷データの更新(登録処理)"

    ''' <summary>
    ''' EDI出荷データの更新(登録処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonDic(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_UPDATE_HOZONDIC) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdDicParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "UpdateHozonDic", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "DIC日陸荷主テーブルの新規追加(登録処理)"

    ''' <summary>
    ''' DIC日陸荷主テーブルの新規追加(登録処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertHozonDicCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH060IN")
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH060DAC.SQL_INSERT_DICCUST) 'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetInsDicCustParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH060DAC", "InsertHozonDicCust", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 検索対象データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTEDIL.DEL_KB = '0' ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUTEDIL.CUST_CD_L = @CUST_CD_LX")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND OUTEDIL.CUST_CD_M = @CUST_CD_MX")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_LX", .Item("CUST_CD_LX").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_MX", .Item("CUST_CD_MX").ToString(), DBDataType.CHAR))

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード
            whereStr = .Item("CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.FREE_C19 LIKE @CUST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_NM_L LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.EDI_CTL_NO LIKE @EDI_CTL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷予定日
            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.DEST_CD LIKE @DEST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '在庫部課コード
            whereStr = .Item("ZBUKA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.FREE_C02 LIKE @ZBUKA_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 EDI出荷データの更新(荷主セット処理)"

    ''' <summary>
    ''' EDI出荷データの更新(荷主セット処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdNinushiSetParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_LX", .Item("CUST_CD_LX").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_MX", .Item("CUST_CD_MX").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_UPD", .Item("CUST_CD_L_UPD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Item("CUST_CD_L").ToString(), .Item("CUST_CD_M").ToString()), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 EDI出荷データの更新(キャンセル処理)"

    ''' <summary>
    ''' EDI出荷データの更新(キャンセル処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdCancelParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_LX", .Item("CUST_CD_LX").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_MX", .Item("CUST_CD_MX").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_UPD", .Item("CUST_CD_L_UPD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Item("CUST_CD_LX").ToString(), .Item("CUST_CD_MX").ToString()), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 DIC出荷EDI受信データヘッダ更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データヘッダ更新(登録処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdDicHedParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_EDIL", .Item("CUST_CD_L_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M_EDIL", .Item("CUST_CD_M_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 DIC出荷EDI受信データ明細更新(登録処理)"

    ''' <summary>
    ''' DIC出荷EDI受信データ明細更新(登録処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdDicDtlParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_EDIL", .Item("CUST_CD_L_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M_EDIL", .Item("CUST_CD_M_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 EDI出荷データの更新(登録処理)"

    ''' <summary>
    ''' EDI出荷データの更新(登録処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdDicParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_EDIL", .Item("CUST_CD_L_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M_EDIL", .Item("CUST_CD_M_EDIL").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 DIC日陸荷主テーブルの新規追加(登録処理)"

    ''' <summary>
    ''' DIC日陸荷主テーブルの新規追加(登録処理)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsDicCustParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns(ByVal prmList As ArrayList)

        'パラメータ設定
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

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

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessage("E011")
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
