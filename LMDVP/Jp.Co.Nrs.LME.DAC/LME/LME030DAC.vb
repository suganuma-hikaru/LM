' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "検索対象データの検索"

#Region "検索対象データの検索 SQL SELECT句"

    ''' <summary>
    ''' 検索対象データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENDATA As String = " SELECT                                                                  " & vbNewLine _
                                               & " SAGYOSIJI.NRS_BR_CD                   AS NRS_BR_CD                      " & vbNewLine _
                                               & ",SAGYOSIJI.SAGYO_SIJI_NO               AS SAGYO_SIJI_NO                  " & vbNewLine _
                                               & ",SAGYOSIJI.SAGYO_SIJI_DATE             AS SAGYO_COMP_DATE                " & vbNewLine _
                                               & ",CUST.CUST_NM_L                        AS CUST_NM_L                      " & vbNewLine _
                                               & ",MIN(GOODS.GOODS_NM_1)                 AS GOODS_NM                       " & vbNewLine _
                                               & ",MIN(SAGYO.SAGYO_NM)                   AS SAGYO_NM                       " & vbNewLine _
                                               & ",SUSER.USER_NM                         AS USER_NM                        " & vbNewLine _
                                               & ",SAGYO.CUST_CD_L                       AS CUST_CD_L                      " & vbNewLine _
                                               & ",SAGYO.CUST_CD_M                       AS CUST_CD_M                      " & vbNewLine _
                                               & ",KBN1.KBN_NM1                          AS WH_TAB_STATUS_NM               " & vbNewLine _
                                               & ",SAGYOSIJI.SYS_UPD_DATE                AS SYS_UPD_DATE                   " & vbNewLine _
                                               & ",SAGYOSIJI.SYS_UPD_TIME                AS SYS_UPD_TIME                   " & vbNewLine _
                                               & ",KBN2.KBN_NM1                          AS SAGYO_SIJI_STATUS_NM           " & vbNewLine _
                                               & ",SAGYOSIJI.SAGYO_SIJI_STATUS           AS SAGYO_SIJI_STATUS              " & vbNewLine

#End Region

#Region "検索対象データの検索 SQL FROM句"

    ''' <summary>
    ''' 検索対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENDATA1 As String = "FROM                                                               " & vbNewLine _
                                                     & "$LM_TRN$..E_SAGYO_SIJI SAGYOSIJI                                   " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_TRN$..E_SAGYO SAGYO                                            " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "SAGYOSIJI.NRS_BR_CD = SAGYO.NRS_BR_CD                              " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "SAGYOSIJI.SAGYO_SIJI_NO = SAGYO.SAGYO_SIJI_NO                      " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "SAGYOSIJI.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                     & "LEFT JOIN                                                         " & vbNewLine _
                                                     & "$LM_MST$..M_CUST CUST                                              " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "CUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_L = SAGYO.CUST_CD_L                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_M = SAGYO.CUST_CD_M                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_S = '00'                                              " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_SS = '00'                                             " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..Z_KBN KBN1                                               " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "KBN1.KBN_GROUP_CD = 'S118'                                         " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "KBN1.KBN_CD = SAGYOSIJI.WH_TAB_STATUS                              " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..Z_KBN KBN2                                               " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "KBN2.KBN_GROUP_CD = 'S122'                                         " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "KBN2.KBN_CD = SAGYOSIJI.SAGYO_SIJI_STATUS                          " & vbNewLine

    ''' <summary>
    ''' 検索対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENDATA2 As String = "LEFT JOIN                                                         " & vbNewLine _
                                                     & "$LM_MST$..M_GOODS GOODS                                            " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "GOODS.NRS_BR_CD = SAGYO.NRS_BR_CD                                  " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "GOODS.GOODS_CD_NRS = SAGYO.GOODS_CD_NRS                            " & vbNewLine


    ''' <summary>
    ''' 検索対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENDATA3 As String = "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..S_USER SUSER                                             " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "SUSER.USER_CD = SAGYOSIJI.SYS_ENT_USER                             " & vbNewLine

#End Region

#Region "検索対象データの検索 SQL GROUP BY句"

    ''' <summary>
    ''' 検索対象データの検索 SQL GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_KENDATA As String = "GROUP BY                                                           " & vbNewLine _
                                                     & " SAGYOSIJI.NRS_BR_CD                                               " & vbNewLine _
                                                     & ",SAGYOSIJI.SAGYO_SIJI_NO                                           " & vbNewLine _
                                                     & ",SAGYOSIJI.SAGYO_SIJI_DATE                                         " & vbNewLine _
                                                     & ",CUST.CUST_NM_L                                                    " & vbNewLine _
                                                     & ",SUSER.USER_NM                                                     " & vbNewLine _
                                                     & ",SAGYO.CUST_CD_L                                                   " & vbNewLine _
                                                     & ",SAGYO.CUST_CD_M                                                   " & vbNewLine _
                                                     & ",KBN1.KBN_NM1                                                      " & vbNewLine _
                                                     & ",SAGYOSIJI.SYS_UPD_DATE                                            " & vbNewLine _
                                                     & ",SAGYOSIJI.SYS_UPD_TIME                                            " & vbNewLine _
                                                     & ",KBN2.KBN_NM1                                                      " & vbNewLine _
                                                     & ",SAGYOSIJI.SAGYO_SIJI_STATUS                                       " & vbNewLine


#End Region

#Region "検索対象データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 検索対象データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENDATA As String = "ORDER BY                                                           " & vbNewLine _
                                                     & " SAGYOSIJI.SAGYO_SIJI_DATE                                         " & vbNewLine _
                                                     & ",SAGYOSIJI.SAGYO_SIJI_NO                                           " & vbNewLine

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

#Region "検索対象データの検索"

    ''' <summary>
    ''' 検索対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME030IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME030DAC.SQL_SELECT_KENDATA)       'SQL構築 SELECT句
        Me._StrSql.Append(LME030DAC.SQL_SELECT_FROM_KENDATA1) 'SQL構築 FROM句
        Call SetSQLWhereData1(inTbl.Rows(0))                  '条件設定
        Me._StrSql.Append(LME030DAC.SQL_SELECT_FROM_KENDATA2) 'SQL構築 FROM句
        Call SetSQLWhereData2(inTbl.Rows(0))                  '条件設定
        Me._StrSql.Append(LME030DAC.SQL_SELECT_FROM_KENDATA3) 'SQL構築 FROM句
        Call SetSQLWhereData3(inTbl.Rows(0))                  '条件設定
        Me._StrSql.Append(LME030DAC.SQL_SELECT_GROUP_KENDATA) 'SQL構築 GROUP句
        Me._StrSql.Append(LME030DAC.SQL_SELECT_ORDER_KENDATA) 'SQL構築 ORDER句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME030DAC", "SelectKensakuData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("USER_NM", "USER_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("WH_TAB_STATUS_NM", "WH_TAB_STATUS_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SAGYO_SIJI_STATUS_NM", "SAGYO_SIJI_STATUS_NM")
        map.Add("SAGYO_SIJI_STATUS", "SAGYO_SIJI_STATUS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME030OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 追加対象データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            '荷主名
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_NM_L LIKE @CUST_NM_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData3(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("SAGYOSIJI.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '作業日FROM
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.SAGYO_SIJI_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '作業日TO
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.SAGYO_SIJI_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '作業指示書番号
            whereStr = .Item("SAGYO_SIJI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.SAGYO_SIJI_NO LIKE @SAGYO_SIJI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作業内容
            whereStr = .Item("SAGYO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.SAGYO_NM LIKE @SAGYO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '現作業指示ステータス
            whereStr = .Item("WH_TAB_STATUS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.WH_TAB_STATUS = @WH_TAB_STATUS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", whereStr, DBDataType.CHAR))
            End If

            '進捗区分
            Dim arr As ArrayList = New ArrayList()
            If LMConst.FLG.ON.Equals(.Item("SAGYO_SIJI_STATUS1").ToString()) Then
                arr.Add("'00'")
            End If
            If LMConst.FLG.ON.Equals(.Item("SAGYO_SIJI_STATUS2").ToString()) Then
                arr.Add("'01'")
            End If
            '進捗区分を設定
            Call Me.SetCheckBoxData(arr, "SAGYO_SIJI_STATUS")

        End With

    End Sub
    ''' <summary>
    ''' チェックボックスの条件を設定
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="colNm">列名</param>
    ''' <remarks></remarks>
    Private Sub SetCheckBoxData(ByVal arr As ArrayList, ByVal colNm As String)

        Dim max As Integer = arr.Count - 1

        If -1 < max Then

            Me._StrSql.Append(String.Concat(" AND SAGYOSIJI.", colNm, " IN ("))

            For i As Integer = 0 To max

                If i = max Then

                    Me._StrSql.Append(String.Concat(arr(i).ToString(), " ) "))

                Else

                    Me._StrSql.Append(String.Concat(arr(i).ToString(), " , "))

                End If

            Next

            Me._StrSql.Append(vbNewLine)

        End If

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
