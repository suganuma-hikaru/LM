' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI020DAC : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' GETU_INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "GETU_IN"

    ''' <summary>
    ''' GETU_OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "GETU_OUT"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI020DAC"

#End Region

#Region "検索処理 SQL"

#Region "月末在庫コンボ用"

    Private Const SQL_SELECT_GETU_DATA As String = "SELECT                                                        " & vbNewLine _
                                                 & " MAIN.RIREKI_DATE AS RIREKI_DATE                              " & vbNewLine _
                                                 & ",MAIN.CUST_CD_L   AS CUST_CD_L                                " & vbNewLine _
                                                 & ",MAIN.CUST_CD_M   AS CUST_CD_M                                " & vbNewLine _
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
                                                 & "ORDER BY MAIN.RIREKI_DATE                                     " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(LMI020DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI020DAC.SQL_SELECT_GETU_DATA)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RIREKI_DATE", "RIREKI_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI020DAC.TABLE_NM_OUT)

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

#End Region

#End Region

End Class
