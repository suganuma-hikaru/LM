' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI660DAC : 請求鑑(DIC鑑種別：A+鑑種別B（横持料）)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI660DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI660DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	 SEKY_MEI.NRS_BR_CD                                     AS NRS_BR_CD  " & vbNewLine _
                                            & ", 'BB'                                                   AS PTN_ID     " & vbNewLine _
                                            & ", MR1.PTN_CD                                             AS PTN_CD     " & vbNewLine _
                                            & ", MR1.RPT_ID                                             AS RPT_ID     " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                        " & vbNewLine _
                                            & "  MR1.RPT_ID                             AS RPT_ID            " & vbNewLine _
                                            & ", SEKY_MEI.NRS_BR_CD                     AS NRS_BR_CD         " & vbNewLine _
                                            & ", SEKY_MEI.SKYU_DATE                     AS SKYU_DATE         " & vbNewLine _
                                            & ", SEKY_MEI.KAGAMI_KB                     AS KAGAMI_KB         " & vbNewLine _
                                            & ", SUM(SEKY_MEI.TTL)                      AS TTL               " & vbNewLine _
                                            & ", SUM(SEKY_MEI.TAX_TTL)                  AS TAX_TTL           " & vbNewLine _
                                            & ", SUM(SEKY_MEI.G_TTL)                    AS G_TTL             " & vbNewLine _
                                            & ", NRS_BR.NRS_BR_NM                       AS NRS_BR_NM         " & vbNewLine _
                                            & ", NRS_BR.AD_1                            AS AD_1              " & vbNewLine _
                                            & ", NRS_BR.AD_2                            AS AD_2              " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                        " & vbNewLine _
                                            & " $LM_TRN$..I_DIC_SEKY_MEISAI SEKY_MEI       " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & " $LM_MST$..M_NRS_BR NRS_BR                  " & vbNewLine _
                                            & " ON SEKY_MEI.NRS_BR_CD = NRS_BR.NRS_BR_CD   " & vbNewLine _
                                            & "--帳票マスタから取得                        " & vbNewLine _
                                            & "LEFT JOIN                                   " & vbNewLine _
                                            & " $LM_MST$..M_RPT MR1                        " & vbNewLine _
                                            & "  ON MR1.NRS_BR_CD = SEKY_MEI.NRS_BR_CD     " & vbNewLine _
                                            & " AND MR1.PTN_ID = 'BB'                      " & vbNewLine _
                                            & " AND MR1.STANDARD_FLAG = '01'               " & vbNewLine _
                                            & " AND MR1.SYS_DEL_FLG = '0'                  " & vbNewLine

#End Region

#Region "WHERE句"

    ''' <summary>
    ''' 印刷データ抽出用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE As String = "WHERE                                " & vbNewLine _
                                             & "    SEKY_MEI.KAGAMI_KB = 'A'         " & vbNewLine _
                                             & "AND SEKY_MEI.NRS_BR_CD = @NRS_BR_CD  " & vbNewLine _
                                             & "AND SEKY_MEI.SKYU_DATE = @SEKY_DATE  " & vbNewLine _
                                             & "AND SEKY_MEI.SEIQTO_CD = @SEIQTO_CD  " & vbNewLine _
                                             & "AND SEKY_MEI.SYS_DEL_FLG = '0'       " & vbNewLine

#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 印刷データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY              " & vbNewLine _
                                         & "  MR1.RPT_ID          " & vbNewLine _
                                         & ", SEKY_MEI.NRS_BR_CD  " & vbNewLine _
                                         & ", SEKY_MEI.SKYU_DATE  " & vbNewLine _
                                         & ", SEKY_MEI.KAGAMI_KB  " & vbNewLine _
                                         & ", NRS_BR.NRS_BR_NM    " & vbNewLine _
                                         & ", NRS_BR.AD_1         " & vbNewLine _
                                         & ", NRS_BR.AD_2         " & vbNewLine

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY              " & vbNewLine _
                                         & "  SEKY_MEI.KAGAMI_KB  " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI660IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用Where句)
        Call Me.SetSQLSelectWhere()                       'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI660DAC", "SelectMPRT", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMI660IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI660DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMI660DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMI660DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)
        Call Me.SetSQLSelectWhere()                       'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI660DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("KAGAMI_KB", "KAGAMI_KB")
        map.Add("TTL", "TTL")
        map.Add("TAX_TTL", "TAX_TTL")
        map.Add("G_TTL", "G_TTL")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI660OUT")

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_DATE", Me._Row.Item("SEKY_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))

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
