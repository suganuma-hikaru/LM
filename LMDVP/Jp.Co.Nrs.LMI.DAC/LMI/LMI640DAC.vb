' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI640DAC : 引取運賃明細書
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI640DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI640DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	 HIKI.NRS_BR_CD                                         AS NRS_BR_CD  " & vbNewLine _
                                            & ", 'AL'                                                   AS PTN_ID     " & vbNewLine _
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
    Private Const SQL_SELECT_DATA As String = "SELECT                                                        " & vbNewLine _
                                            & "  CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID            " & vbNewLine _
                                            & "       ELSE MR2.RPT_ID END               AS RPT_ID            " & vbNewLine _
                                            & ", HIKI.NRS_BR_CD                         AS NRS_BR_CD         " & vbNewLine _
                                            & ", NRS_BR.NRS_BR_NM                       AS NRS_BR_NM         " & vbNewLine _
                                            & ", HIKI.CUST_CD_L                         AS CUST_CD_L         " & vbNewLine _
                                            & ", HIKI.CUST_CD_M                         AS CUST_CD_M         " & vbNewLine _
                                            & ", CUST.CUST_NM_L                         AS CUST_NM_L         " & vbNewLine _
                                            & ", CUST.CUST_NM_M                         AS CUST_NM_M         " & vbNewLine _
                                            & ", HIKI.HIKI_DATE                         AS HIKI_DATE         " & vbNewLine _
                                            & ", HIKI.MEISAI_NO                         AS MEISAI_NO         " & vbNewLine _
                                            & ", HIKI.HIN_CD                            AS HIN_CD            " & vbNewLine _
                                            & ", KBN.KBN_NM1                            AS HIN_NM            " & vbNewLine _
                                            & ", HIKI.DEST_CD                           AS HIKITORI_CD       " & vbNewLine _
                                            & ", DEST.DEST_NM                           AS HIKITORI_NM       " & vbNewLine _
                                            & ", HIKI.FC_NB                             AS FC_NB             " & vbNewLine _
                                            & ", HIKI.FC_TANKA                          AS FC_TANKA          " & vbNewLine _
                                            & ", HIKI.FC_TOTAL                          AS FC_TOTAL          " & vbNewLine _
                                            & ", HIKI.DM_NB                             AS DM_NB             " & vbNewLine _
                                            & ", HIKI.DM_TANKA                          AS DM_TANKA          " & vbNewLine _
                                            & ", HIKI.DM_TOTAL                          AS DM_TOTAL          " & vbNewLine _
                                            & ", HIKI.CNTN_KISU                         AS KISU              " & vbNewLine _
                                            & ", HIKI.SEIHIN_GAKU                       AS SEIHIN            " & vbNewLine _
                                            & ", HIKI.SUKURAP_GAKU                      AS SUKURAP           " & vbNewLine _
                                            & ", HIKI.WARIMASI_GAKU                     AS WARIMASI          " & vbNewLine _
                                            & ", HIKI.SEIKEI_GAKU                       AS SEIKEI            " & vbNewLine _
                                            & ", HIKI.ROSEN_GAKU                        AS ROSEN             " & vbNewLine _
                                            & ", HIKI.KOUSOKU_GAKU                      AS KOUSOKU           " & vbNewLine _
                                            & ", HIKI.SONOTA_GAKU                       AS SONOTA            " & vbNewLine _
                                            & ", HIKI.ALL_TOTAL                         AS ALL_TOTAL         " & vbNewLine _
                                            & ", @T_DATE                                AS T_DATE            " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                        " & vbNewLine _
                                            & "  $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI HIKI   " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "  $LM_MST$..M_NRS_BR NRS_BR                 " & vbNewLine _
                                            & "  ON HIKI.NRS_BR_CD = NRS_BR.NRS_BR_CD      " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "  $LM_MST$..M_CUST CUST                     " & vbNewLine _
                                            & "  ON HIKI.NRS_BR_CD = CUST.NRS_BR_CD        " & vbNewLine _
                                            & " AND HIKI.CUST_CD_L = CUST.CUST_CD_L        " & vbNewLine _
                                            & " AND HIKI.CUST_CD_M = CUST.CUST_CD_M        " & vbNewLine _
                                            & " AND CUST.CUST_CD_S = '00'                  " & vbNewLine _
                                            & " AND CUST.CUST_CD_SS = '00'                 " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "  $LM_MST$..M_DEST DEST                     " & vbNewLine _
                                            & "  ON HIKI.NRS_BR_CD = DEST.NRS_BR_CD        " & vbNewLine _
                                            & " AND HIKI.CUST_CD_L = DEST.CUST_CD_L        " & vbNewLine _
                                            & " AND HIKI.DEST_CD = DEST.DEST_CD            " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBN                       " & vbNewLine _
                                            & "  ON KBN.KBN_GROUP_CD = 'H019'              " & vbNewLine _
                                            & " AND HIKI.HIN_CD = KBN.KBN_CD               " & vbNewLine _
                                            & "--荷主帳票マスタから取得                    " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "  $LM_MST$..M_CUST_RPT MCR1                 " & vbNewLine _
                                            & "  ON HIKI.NRS_BR_CD = MCR1.NRS_BR_CD        " & vbNewLine _
                                            & " AND HIKI.CUST_CD_L = MCR1.CUST_CD_L        " & vbNewLine _
                                            & " AND HIKI.CUST_CD_M = MCR1.CUST_CD_M        " & vbNewLine _
                                            & " AND MCR1.PTN_ID    = 'AL'                  " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & "   $LM_MST$..M_RPT MR1                      " & vbNewLine _
                                            & "  ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD         " & vbNewLine _
                                            & " AND MR1.PTN_ID = MCR1.PTN_ID               " & vbNewLine _
                                            & " AND MR1.PTN_CD = MCR1.PTN_CD               " & vbNewLine _
                                            & " AND MR1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                            & "--帳票マスタから取得                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2               " & vbNewLine _
                                            & "  ON MR2.NRS_BR_CD = HIKI.NRS_BR_CD         " & vbNewLine _
                                            & " AND MR2.PTN_ID = 'AL'                      " & vbNewLine _
                                            & " AND MR2.STANDARD_FLAG = '01'               " & vbNewLine _
                                            & " AND MR2.SYS_DEL_FLG = '0'                  " & vbNewLine

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY          " & vbNewLine _
                                         & "  HIKI.HIN_CD     " & vbNewLine _
                                         & ", HIKI.HIKI_DATE  " & vbNewLine _
                                         & ", HIKI.MEISAI_NO  " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI640IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI640DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI640DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Call Me.SetSQLSelectWhere()                       '条件設定(WHERE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI640DAC", "SelectMPRT", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMI640IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMI640DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI640DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Call Me.SetSQLSelectWhere()                       '条件設定(WHERE句)
        Me._StrSql.Append(LMI640DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI640DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("HIKI_DATE", "HIKI_DATE")
        map.Add("MEISAI_NO", "MEISAI_NO")
        map.Add("HIN_CD", "HIN_CD")
        map.Add("HIN_NM", "HIN_NM")
        map.Add("HIKITORI_CD", "HIKITORI_CD")
        map.Add("HIKITORI_NM", "HIKITORI_NM")
        map.Add("FC_NB", "FC_NB")
        map.Add("FC_TANKA", "FC_TANKA")
        map.Add("FC_TOTAL", "FC_TOTAL")
        map.Add("DM_NB", "DM_NB")
        map.Add("DM_TANKA", "DM_TANKA")
        map.Add("DM_TOTAL", "DM_TOTAL")
        map.Add("KISU", "KISU")
        map.Add("SEIHIN", "SEIHIN")
        map.Add("SUKURAP", "SUKURAP")
        map.Add("WARIMASI", "WARIMASI")
        map.Add("SEIKEI", "SEIKEI")
        map.Add("ROSEN", "ROSEN")
        map.Add("KOUSOKU", "KOUSOKU")
        map.Add("SONOTA", "SONOTA")
        map.Add("ALL_TOTAL", "ALL_TOTAL")
        map.Add("T_DATE", "T_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI640OUT")

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

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '引取日 FROM
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIKI_DATE >= @F_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '引取日 TO
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIKI_DATE <= @T_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            '品名コード
            whereStr = .Item("HIN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIN_CD = @HIN_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", whereStr, DBDataType.CHAR))
            End If

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '削除フラグ
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(andstr)
            End If

        End With

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
