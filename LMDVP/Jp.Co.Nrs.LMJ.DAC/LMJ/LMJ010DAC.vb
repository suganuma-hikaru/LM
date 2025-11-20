' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ010DAC : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMJ010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 処理内容 = 指定された荷主のみチェックする
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHORI_SONOTA As String = "99"

    ''' <summary>
    ''' LMJ010INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMJ010IN"

    ''' <summary>
    ''' GETU_INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_GETU_IN As String = "GETU_IN"

    ''' <summary>
    ''' GETU_OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_GETU_OUT As String = "GETU_OUT"

    ''' <summary>
    ''' ZAIK_ZANテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ZAIK_ZAN As String = "ZAIK_ZAN"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMJ010DAC"

#End Region

#Region "検索処理 SQL"

#Region "月末在庫コンボ用"

    Private Const SQL_SELECT_GETU_DATA As String = "SELECT                                                        " & vbNewLine _
                                                 & " MAIN.RIREKI_DATE AS RIREKI_DATE                              " & vbNewLine _
                                                 & "FROM                                                          " & vbNewLine _
                                                 & "(                                                             " & vbNewLine _
                                                 & "        SELECT                                                " & vbNewLine _
                                                 & "                 D01_01.NRS_BR_CD   AS NRS_BR_CD              " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_L   AS CUST_CD_L              " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_M   AS CUST_CD_M              " & vbNewLine _
                                                 & "                ,D05_01.RIREKI_DATE AS RIREKI_DATE            " & vbNewLine _
                                                 & "        FROM       $LM_TRN$..D_ZAI_TRS       D01_01           " & vbNewLine _
                                                 & "        INNER JOIN $LM_TRN$..D_ZAI_ZAN_JITSU D05_01           " & vbNewLine _
                                                 & "           ON D01_01.NRS_BR_CD             = D05_01.NRS_BR_CD " & vbNewLine _
                                                 & "          AND D01_01.ZAI_REC_NO            = D05_01.ZAI_REC_NO" & vbNewLine _
                                                 & "          AND D01_01.SYS_DEL_FLG           = '0'              " & vbNewLine _
                                                 & "        WHERE D01_01.NRS_BR_CD             = @NRS_BR_CD       " & vbNewLine _
                                                 & "          AND D01_01.CUST_CD_L             = @CUST_CD_L       " & vbNewLine _
                                                 & "          AND D01_01.CUST_CD_M             = @CUST_CD_M       " & vbNewLine _
                                                 & "          AND D01_01.SYS_DEL_FLG           = '0'              " & vbNewLine _
                                                 & "          AND D05_01.RIREKI_DATE          <> '0000000'        " & vbNewLine _
                                                 & "        GROUP BY D01_01.NRS_BR_CD                             " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_L                             " & vbNewLine _
                                                 & "                ,D01_01.CUST_CD_M                             " & vbNewLine _
                                                 & "                ,D05_01.RIREKI_DATE                           " & vbNewLine _
                                                 & ") MAIN                                                        " & vbNewLine _
                                                 & "ORDER BY MAIN.RIREKI_DATE DESC                                " & vbNewLine

#End Region

#Region "締め荷主の取得"

    Private Const SQL_SELECT_SHIME_CUST_001 As String = " SELECT                                        " & vbNewLine _
                                                      & " D01_01.NRS_BR_CD          AS      NRS_BR_CD   " & vbNewLine _
                                                      & ",D01_01.CUST_CD_L          AS      CUST_CD_L   " & vbNewLine _
                                                      & ",M07_01.CUST_NM_L          AS      CUST_NM_L   " & vbNewLine _
                                                      & ",D01_01.CUST_CD_M          AS      CUST_CD_M   " & vbNewLine _
                                                      & ",M07_01.CUST_NM_M          AS      CUST_NM_M   " & vbNewLine _
                                                      & ",@SEIKYU_DATE              AS      SEIKYU_DATE " & vbNewLine _
                                                      & ",@RIREKI_DATE              AS      RIREKI_DATE " & vbNewLine _
                                                      & ",@CLOSE_KB                 AS      CLOSE_KB    " & vbNewLine _
                                                      & ",@SERIAL_FLG               AS      SERIAL_FLG  " & vbNewLine _
                                                      & " FROM      $LM_TRN$..D_ZAI_TRS D01_01          " & vbNewLine _
                                                      & "INNER JOIN $LM_MST$..M_CUST    M07_01          " & vbNewLine _
                                                      & "   ON D01_01.NRS_BR_CD       = M07_01.NRS_BR_CD" & vbNewLine _
                                                      & "  AND D01_01.CUST_CD_L       = M07_01.CUST_CD_L" & vbNewLine _
                                                      & "  AND D01_01.CUST_CD_M       = M07_01.CUST_CD_M" & vbNewLine _
                                                      & "  AND M07_01.CUST_CD_S       = '00'            " & vbNewLine _
                                                      & "  AND M07_01.CUST_CD_SS      = '00'            " & vbNewLine _
                                                      & "  AND M07_01.SYS_DEL_FLG     = '0'             " & vbNewLine _
                                                      & "INNER JOIN $LM_MST$..M_SEIQTO  M06_01          " & vbNewLine _
                                                      & "   ON M07_01.NRS_BR_CD       = M06_01.NRS_BR_CD" & vbNewLine _
                                                      & "  AND M07_01.HOKAN_SEIQTO_CD = M06_01.SEIQTO_CD" & vbNewLine _
                                                      & "  AND M06_01.SYS_DEL_FLG     = '0'             " & vbNewLine _
                                                      & "WHERE D01_01.NRS_BR_CD       = @NRS_BR_CD      " & vbNewLine _
                                                      & "  AND D01_01.SYS_DEL_FLG     = '0'             " & vbNewLine

    Private Const SQL_SELECT_SHIME_CUST_002 As String = "GROUP BY D01_01.NRS_BR_CD                      " & vbNewLine _
                                                      & "        ,D01_01.CUST_CD_L                      " & vbNewLine _
                                                      & "        ,M07_01.CUST_NM_L                      " & vbNewLine _
                                                      & "        ,D01_01.CUST_CD_M                      " & vbNewLine _
                                                      & "        ,M07_01.CUST_NM_M                      " & vbNewLine _
                                                      & "ORDER BY D01_01.CUST_CD_L                      " & vbNewLine _
                                                      & "        ,D01_01.CUST_CD_M                      " & vbNewLine


#End Region

#Region "請求在庫チェック"

    Private Const SQL_SELECT_SEIQ_ZAIKO_COUNT As String = "SELECT COUNT(G06_01.NRS_BR_CD) AS REC_CNT        " & vbNewLine

    Private Const SQL_SELECT_SEIQ_ZAIKO_DATA As String = "SELECT DISTINCT                                  " & vbNewLine _
                                                       & "       M08_01.CUST_CD_L  AS CUST_CD_L            " & vbNewLine _
                                                       & "      ,M08_01.CUST_CD_M  AS CUST_CD_M            " & vbNewLine _
                                                       & "      ,M08_01.CUST_CD_S  AS CUST_CD_S            " & vbNewLine _
                                                       & "      ,M08_01.CUST_CD_SS AS CUST_CD_SS           " & vbNewLine

    Private Const SQL_SELECT_SEIQ_ZAIKO_FROM As String = "  FROM      $LM_TRN$..G_ZAIK_ZAN G06_01          " & vbNewLine _
                                                       & " INNER JOIN $LM_MST$..M_GOODS M08_01             " & vbNewLine _
                                                       & "    ON G06_01.NRS_BR_CD     = M08_01.NRS_BR_CD   " & vbNewLine _
                                                       & "   AND G06_01.GOODS_CD_NRS  = M08_01.GOODS_CD_NRS" & vbNewLine _
                                                       & "   AND M08_01.SYS_DEL_FLG   = '0'                " & vbNewLine _
                                                       & " WHERE G06_01.NRS_BR_CD     = @NRS_BR_CD         " & vbNewLine _
                                                       & "   AND M08_01.CUST_CD_L     = @CUST_CD_L         " & vbNewLine _
                                                       & "   AND M08_01.CUST_CD_M     = @CUST_CD_M         " & vbNewLine _
                                                       & "   AND G06_01.INV_DATE_TO   = @SEIKYU_DATE       " & vbNewLine _
                                                       & "   AND G06_01.SYS_DEL_FLG   = '0'                " & vbNewLine

#End Region

#Region "月末在庫チェック"

    Private Const SQL_SELECT_ZAIZAN_COUNT As String = "SELECT COUNT(D05_01.NRS_BR_CD) AS REC_CNT              " & vbNewLine _
                                                    & "  FROM      $LM_TRN$..D_ZAI_ZAN_JITSU D05_01           " & vbNewLine _
                                                    & " INNER JOIN $LM_TRN$..D_ZAI_TRS       D01_01           " & vbNewLine _
                                                    & "    ON D05_01.NRS_BR_CD             = D01_01.NRS_BR_CD " & vbNewLine _
                                                    & "   AND D05_01.ZAI_REC_NO            = D01_01.ZAI_REC_NO" & vbNewLine _
                                                    & "   AND D01_01.SYS_DEL_FLG           = '0'              " & vbNewLine _
                                                    & " WHERE D05_01.NRS_BR_CD             = @NRS_BR_CD       " & vbNewLine _
                                                    & "   AND D01_01.CUST_CD_L             = @CUST_CD_L       " & vbNewLine _
                                                    & "   AND D01_01.CUST_CD_M             = @CUST_CD_M       " & vbNewLine _
                                                    & "   AND D05_01.RIREKI_DATE           = @RIREKI_DATE     " & vbNewLine _
                                                    & "   AND D05_01.SYS_DEL_FLG           = '0'              " & vbNewLine


#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

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
    ''' 月末在庫コンボのデータを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLの構築・発行</remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ010DAC.TABLE_NM_GETU_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_GETU_DATA)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RIREKI_DATE", "RIREKI_DATE")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMJ010DAC.TABLE_NM_GETU_OUT)

        Return ds

    End Function

    ''' <summary>
    ''' 締め荷主の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLの構築・発行</remarks>
    Private Function SelectShimeCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SHIME_CUST_001)
        Call Me.SetConditionShimeCustSQL(Me._StrSql, Me._SqlPrmList, Me._Row)
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SHIME_CUST_002)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SEIKYU_DATE", "SEIKYU_DATE")
        map.Add("RIREKI_DATE", "RIREKI_DATE")
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("SERIAL_FLG", "SERIAL_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMJ010DAC.TABLE_NM_IN)

        Return ds

    End Function

    ''' <summary>
    ''' 請求在庫データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqZaikoCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SEIQ_ZAIKO_COUNT)
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SEIQ_ZAIKO_FROM)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIKYU_DATE", Me._Row.Item("SEIKYU_DATE").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 請求在庫データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqZaikoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SEIQ_ZAIKO_DATA)
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_SEIQ_ZAIKO_FROM)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIKYU_DATE", Me._Row.Item("SEIKYU_DATE").ToString(), DBDataType.CHAR))
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMJ010DAC.TABLE_NM_ZAIK_ZAN)

        Return ds

    End Function

    ''' <summary>
    ''' 月末在庫データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetsuZaikoCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMJ010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMJ010DAC.SQL_SELECT_ZAIZAN_COUNT)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", Me._Row.Item("RIREKI_DATE").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMJ010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名取得
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "抽出条件"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetConditionShimeCustSQL(ByVal sql As StringBuilder, ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIKYU_DATE", .Item("SEIKYU_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", .Item("RIREKI_DATE").ToString(), DBDataType.CHAR))

            Dim closeKb As String = .Item("CLOSE_KB").ToString()
            prmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", closeKb, DBDataType.CHAR))

            '指定荷主の場合
            If LMJ010DAC.SHORI_SONOTA.Equals(closeKb) = True Then

                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                sql.Append("  AND D01_01.CUST_CD_L       = @CUST_CD_L      ")
                sql.Append(vbNewLine)
                sql.Append("  AND D01_01.CUST_CD_M       = @CUST_CD_M      ")
                sql.Append(vbNewLine)

            Else

                '全荷主の場合
                sql.Append("  AND M06_01.CLOSE_KB        = @CLOSE_KB       ")
                sql.Append(vbNewLine)

            End If

            prmList.Add(MyBase.GetSqlParameter("@SERIAL_FLG", .Item("SERIAL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

End Class
