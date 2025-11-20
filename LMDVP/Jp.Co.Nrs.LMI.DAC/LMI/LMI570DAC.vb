' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI570DAC : TSMC請求データ検索
'  作  成  者       :  [HORI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI570DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI570DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' データ検索(1/4)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_1 As String = "" _
        & "SELECT                           " & vbNewLine _
        & "   SMT.INV_DATE_TO               " & vbNewLine _
        & "  ,SEI.SEIQTO_CD                 " & vbNewLine _
        & "  ,SEI.SEIQTO_NM                 " & vbNewLine _
        & "  ,SMT.SYS_ENT_DATE AS ENT_DATE  " & vbNewLine _
        & "  ,USR.USER_NM AS ENT_USER_NM    " & vbNewLine _
        & "  ,SMT.JOB_NO                    " & vbNewLine

    ''' <summary>
    ''' データ検索(2/4)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_2 As String = "" _
        & "FROM                                         " & vbNewLine _
        & "  $LM_MST$..M_SEIQTO AS SEI                  " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..M_CUST AS CST                  " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          CST.NRS_BR_CD = SEI.NRS_BR_CD      " & vbNewLine _
        & "      AND CST.OYA_SEIQTO_CD = SEI.SEIQTO_CD  " & vbNewLine _
        & "      AND CST.SYS_DEL_FLG = '0'              " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    (                                        " & vbNewLine _
        & "      SELECT                                 " & vbNewLine _
        & "         NRS_BR_CD                           " & vbNewLine _
        & "        ,JOB_NO                              " & vbNewLine _
        & "        ,SEIQTO_CD                           " & vbNewLine _
        & "        ,MAX(INV_DATE_TO) AS INV_DATE_TO     " & vbNewLine _
        & "        ,MAX(SYS_ENT_DATE) AS SYS_ENT_DATE   " & vbNewLine _
        & "        ,MAX(SYS_ENT_USER) AS SYS_ENT_USER   " & vbNewLine _
        & "      FROM                                   " & vbNewLine _
        & "        $LM_TRN$..I_SEKY_MEISAI_TSMC         " & vbNewLine _
        & "      GROUP BY                               " & vbNewLine _
        & "         NRS_BR_CD                           " & vbNewLine _
        & "        ,JOB_NO                              " & vbNewLine _
        & "        ,SEIQTO_CD                           " & vbNewLine _
        & "    ) AS SMT                                 " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          SMT.NRS_BR_CD = SEI.NRS_BR_CD      " & vbNewLine _
        & "      AND SMT.SEIQTO_CD = SEI.SEIQTO_CD      " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..S_USER AS USR                  " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "      USR.USER_CD = SMT.SYS_ENT_USER         " & vbNewLine _
        & "WHERE                                        " & vbNewLine _
        & "      SMT.JOB_NO IS NOT NULL                 " & vbNewLine _
        & "  AND SEI.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
        & "  AND SEI.SYS_DEL_FLG = '0'                  " & vbNewLine

    ''' <summary>
    ''' データ検索(3/4)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_3 As String = "" _
        & "GROUP BY             " & vbNewLine _
        & "   SMT.INV_DATE_TO   " & vbNewLine _
        & "  ,SEI.SEIQTO_CD     " & vbNewLine _
        & "  ,SEI.SEIQTO_NM     " & vbNewLine _
        & "  ,SMT.SYS_ENT_DATE  " & vbNewLine _
        & "  ,USR.USER_NM       " & vbNewLine _
        & "  ,SMT.JOB_NO        " & vbNewLine

    ''' <summary>
    ''' データ検索(4/4)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_4 As String = "" _
        & "ORDER BY                 " & vbNewLine _
        & "   SMT.INV_DATE_TO DESC  " & vbNewLine _
        & "  ,SEI.SEIQTO_CD         " & vbNewLine

    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    ''' <remarks>SQL_SELECT_SEARCH_1と差し替えて利用</remarks>
    Private Const SQL_SELECT_SEARCH_CNT As String = "" _
        & "SELECT                   " & vbNewLine _
        & "  COUNT(*) AS SELECT_CNT " & vbNewLine

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
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_CNT)
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_2)
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI570DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_1)
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_2)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_3)
        Me._StrSql.Append(LMI570DAC.SQL_SELECT_SEARCH_4)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI570DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("ENT_DATE", "ENT_DATE")
        map.Add("ENT_USER_NM", "ENT_USER_NM")
        map.Add("JOB_NO", "JOB_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI570OUT")

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND SEI.SEIQTO_CD = @SEIQTO_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND CST.CUST_CD_L = @CUST_CD_L ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND CST.CUST_CD_M = @CUST_CD_M ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND CST.CUST_CD_S = @CUST_CD_S ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND CST.CUST_CD_SS = @CUST_CD_SS ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If

            '請求月
            whereStr = .Item("INV_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND SMT.INV_DATE_TO = CASE SEI.CLOSE_KB                                                                                ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("      WHEN '00' THEN FORMAT(DATEADD(DAY, -1, DATEADD(MONTH, 1, CONVERT(DATE, CONCAT(@INV_DATE, '01')))), 'yyyyMMdd')     ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("      ELSE CONCAT(@INV_DATE, SEI.CLOSE_KB)                                                                               ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("      END                                                                                                                ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE", whereStr, DBDataType.CHAR))
            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND SEI.SEIQTO_NM LIKE @SEIQTO_NM  ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "共通"

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
