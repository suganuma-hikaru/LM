' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC960    : トールCSV出力
'  作  成  者       :  []
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC960DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC960DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' トールCSV作成データ(区分マスタ)検索用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TOLL_CSV_KBN_N010 As String _
            = " SELECT                      " & vbNewLine _
            & "       KBN_GROUP_CD          " & vbNewLine _
            & "     , KBN_CD                " & vbNewLine _
            & "     , KBN_NM1               " & vbNewLine _
            & " FROM                        " & vbNewLine _
            & " -- 区分マスタ(納入予定区分) " & vbNewLine _
            & "     $LM_MST$..Z_KBN         " & vbNewLine _
            & " WHERE                       " & vbNewLine _
            & "     KBN_GROUP_CD = 'N010'   " & vbNewLine _
            & " AND SYS_DEL_FLG  = '0'      " & vbNewLine _
            & " ORDER BY                    " & vbNewLine _
            & "       KBN_GROUP_CD          " & vbNewLine _
            & "     , KBN_CD                " & vbNewLine

    ''' <summary>
    ''' トールCSV作成データ(営業所マスタ)検索用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TOLL_CSV_NRS_BR As String _
            = " SELECT                     " & vbNewLine _
            & "       NRS_BR_CD            " & vbNewLine _
            & "     , NRS_BR_NM            " & vbNewLine _
            & "     , TEL                  " & vbNewLine _
            & "     , ZIP                  " & vbNewLine _
            & "     , AD_1                 " & vbNewLine _
            & "     , AD_2                 " & vbNewLine _
            & "     , AD_3                 " & vbNewLine _
            & " FROM                       " & vbNewLine _
            & "     $LM_MST$..M_NRS_BR     " & vbNewLine _
            & " WHERE                      " & vbNewLine _
            & "     NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
            & " AND SYS_DEL_FLG  = '0'     " & vbNewLine

    ''' <summary>
    ''' トールCSV作成データ(出荷データM)検索用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TOLL_CSV_OUTKA_M As String _
            = "SELECT                                                    " & vbNewLine _
            & "      C_OUTKA_M.NRS_BR_CD    AS NRS_BR_CD                 " & vbNewLine _
            & "    , C_OUTKA_M.OUTKA_NO_L   AS OUTKA_NO_L                " & vbNewLine _
            & "    , C_OUTKA_M.OUTKA_NO_M   AS OUTKA_NO_M                " & vbNewLine _
            & "    , C_OUTKA_M.GOODS_CD_NRS AS GOODS_CD_NRS              " & vbNewLine _
            & "    , C_OUTKA_M.PRINT_SORT   AS PRINT_SORT                " & vbNewLine _
            & "    , M_GOODS.GOODS_CD_CUST  AS GOODS_CD_CUST             " & vbNewLine _
            & "    , M_GOODS.GOODS_NM_1     AS GOODS_NM                  " & vbNewLine _
            & "FROM                                                      " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_M                                   " & vbNewLine _
            & "LEFT JOIN                                                 " & vbNewLine _
            & "    $LM_MST$..M_GOODS                                     " & vbNewLine _
            & "        ON  M_GOODS.NRS_BR_CD    = C_OUTKA_M.NRS_BR_CD    " & vbNewLine _
            & "        AND M_GOODS.GOODS_CD_NRS = C_OUTKA_M.GOODS_CD_NRS " & vbNewLine _
            & "--      AND M_GOODS.SYS_DEL_FLG  = '0'                    " & vbNewLine _
            & "WHERE                                                     " & vbNewLine _
            & "    C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
            & "AND C_OUTKA_M.OUTKA_NO_L  = @OUTKA_NO_L                   " & vbNewLine _
            & "AND C_OUTKA_M.SYS_DEL_FLG = '0'                           " & vbNewLine _
            & "ORDER BY                                                  " & vbNewLine _
            & "      C_OUTKA_M.OUTKA_NO_L                                " & vbNewLine _
            & "    , C_OUTKA_M.PRINT_SORT                                " & vbNewLine _
            & "    , C_OUTKA_M.OUTKA_NO_M                                " & vbNewLine

#End Region

#Region "更新 SQL"

    Private Const SQL_UPDATE_TOLL_CSV As String _
            = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
            & " DENP_FLAG         = '01'            " & vbNewLine _
            & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
            & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
            & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
            & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
            & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
            & "  AND OUTKA_NO_L   = @OUTKA_NO_L     " & vbNewLine

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
    ''' トールCSV作成データ(区分マスタ)検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTollCsvZKbnN010(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC960IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC960DAC.SQL_SELECT_TOLL_CSV_KBN_N010)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMC960DAC", "SelectTollCsvZKbnN010", cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ' DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ' 取得データの格納先のマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM1", "KBN_NM1")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC960OUT_KBN")

        ' 処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC960OUT_KBN").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' トールCSV作成データ(営業所マスタ)検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTollCsvNrsBr(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC960IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC960DAC.SQL_SELECT_TOLL_CSV_NRS_BR)

        ' 条件設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC960DAC", "SelectTollCsvNrsBr", cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ' DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ' 取得データの格納先のマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("TEL", "TEL")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC960OUT_NRS_BR")

        ' 処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC960OUT_NRS_BR").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' トールCSV作成データ(出荷データM)検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTollCsvOutkaM(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC960IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC960DAC.SQL_SELECT_TOLL_CSV_OUTKA_M)

        ' 条件設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC960DAC", "SelectTollCsvOutkaM", cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ' DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ' 取得データの格納先のマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC960OUT_OUTKA_M")

        ' 処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC960OUT_OUTKA_M").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（トールCSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateTollCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC960OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC960DAC.SQL_UPDATE_TOLL_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC960DAC", "UpdateTollCsv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

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

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
