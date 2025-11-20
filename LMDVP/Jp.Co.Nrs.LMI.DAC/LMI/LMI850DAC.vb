' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI850  : 日医工在庫照合データCSV作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI850DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI850DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "CSV出力データの検索"

#Region "CSV出力データの検索 SQL SELECT句"

    ''' <summary>
    ''' CSV出力データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CSV As String = " SELECT                                                                  " & vbNewLine _
                                           & " ZAI.NRS_BR_CD                              AS NRS_BR_CD                 " & vbNewLine _
                                           & ",ISNULL(GOODS.CUST_CD_L,'')                 AS CUST_CD_L                 " & vbNewLine _
                                           & ",ISNULL(GOODS.CUST_CD_M,'')                 AS CUST_CD_M                 " & vbNewLine _
                                           & ",ISNULL(GOODS.GOODS_CD_CUST,'')             AS GOODS_CD_CUST             " & vbNewLine _
                                           & ",ISNULL(GOODS.GOODS_CD_NRS,'')              AS GOODS_CD_NRS              " & vbNewLine _
                                           & ",ZAI.LOT_NO                                 AS LOT_NO                    " & vbNewLine _
                                           & ",ZAI.LT_DATE                                AS LT_DATE                   " & vbNewLine _
                                           & ",ZAI.PORA_ZAI_NB                            AS PORA_ZAI_NB               " & vbNewLine _
                                           & ",ISNULL(GOODS.PKG_NB,0)                     AS PKG_NB                    " & vbNewLine _
                                           & ",ZAI.GOODS_COND_KB_1                        AS GOODS_COND_KB_1           " & vbNewLine _
                                           & ",ZAI.GOODS_COND_KB_2                        AS GOODS_COND_KB_2           " & vbNewLine _
                                           & ",ZAI.GOODS_COND_KB_3                        AS GOODS_COND_KB_3           " & vbNewLine _
                                           & ",ISNULL(CUSTCOND.INFERIOR_GOODS_KB,'')      AS INFERIOR_GOODS_KB         " & vbNewLine _
                                           & ",'0'                                        AS SUM_NB                    " & vbNewLine _
                                           & ",'0'                                        AS SUM_NB_OTHER              " & vbNewLine _
                                           & ",'0'                                        AS RECORD_CNT                " & vbNewLine _
                                           & ",''                                         AS MATOME_FLG                " & vbNewLine

#End Region

#Region "CSV出力データの検索 SQL FROM句"

    ''' <summary>
    ''' CSV出力データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_CSV As String = "FROM                                                             " & vbNewLine _
                                                & "$LM_TRN$..D_ZAI_TRS ZAI                                          " & vbNewLine _
                                                & "LEFT JOIN                                                        " & vbNewLine _
                                                & "$LM_MST$..M_GOODS GOODS                                          " & vbNewLine _
                                                & "ON                                                               " & vbNewLine _
                                                & "GOODS.NRS_BR_CD = ZAI.NRS_BR_CD                                  " & vbNewLine _
                                                & "AND                                                              " & vbNewLine _
                                                & "GOODS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                            " & vbNewLine _
                                                & "LEFT JOIN                                                        " & vbNewLine _
                                                & "$LM_MST$..M_CUSTCOND CUSTCOND                                    " & vbNewLine _
                                                & "ON                                                               " & vbNewLine _
                                                & "CUSTCOND.NRS_BR_CD = ZAI.NRS_BR_CD                               " & vbNewLine _
                                                & "AND                                                              " & vbNewLine _
                                                & "CUSTCOND.CUST_CD_L = ZAI.CUST_CD_L                               " & vbNewLine _
                                                & "AND                                                              " & vbNewLine _
                                                & "CUSTCOND.JOTAI_CD = ZAI.GOODS_COND_KB_3                          " & vbNewLine

#End Region

#Region "CSV出力データの検索 SQL ORDER句"

    ''' <summary>
    ''' CSV出力データの検索 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_CSV As String = "ORDER BY                                                         " & vbNewLine _
                                                 & " GOODS.GOODS_CD_NRS DESC                                         " & vbNewLine _
                                                 & ",ZAI.LOT_NO DESC                                                 " & vbNewLine _
                                                 & ",ZAI.LT_DATE DESC                                                " & vbNewLine

#End Region

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

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "CSV出力データの検索"

    ''' <summary>
    ''' CSV出力データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI850IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI850DAC.SQL_SELECT_CSV)       'SQL構築(Select句)
        Me._StrSql.Append(LMI850DAC.SQL_SELECT_FROM_CSV)  'SQL構築(From句)
        Call SetSQLWhereCSV(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI850DAC.SQL_SELECT_ORDER_CSV) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI850DAC", "SelectCSV", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("INFERIOR_GOODS_KB", "INFERIOR_GOODS_KB")
        map.Add("SUM_NB", "SUM_NB")
        map.Add("SUM_NB_OTHER", "SUM_NB_OTHER")
        map.Add("RECORD_CNT", "RECORD_CNT")
        map.Add("MATOME_FLG", "MATOME_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI850OUT_CSV")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 CSV出力データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereCSV(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" ZAI.SYS_DEL_FLG = '0'                                         ")
            'add 
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND ZAI.OFB_KB <> '02'                                        ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND ZAI.PORA_ZAI_NB > 0                                       ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAI.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAI.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAI.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

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
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#Region "ユーティリティ"

#End Region

#End Region

End Class
