' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ060DAC : 郵便番号マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZIP.ZIP_NO)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' ZIP_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                          " & vbNewLine _
                                        & "      ZIP.ZIP_NO                          AS ZIP_NO               " & vbNewLine _
                                        & "     ,ZIP.JIS_CD                          AS JIS_CD               " & vbNewLine _
                                        & "     ,ZIP.KEN_K                           AS KEN_K                " & vbNewLine _
                                        & "     ,ZIP.CITY_K                          AS CITY_K               " & vbNewLine _
                                        & "     ,ZIP.TOWN_K                          AS TOWN_K               " & vbNewLine _
                                        & "     ,ZIP.KEN_N                           AS KEN_N                " & vbNewLine _
                                        & "     ,ZIP.CITY_N                          AS CITY_N               " & vbNewLine _
                                        & "     ,ZIP.TOWN_N                          AS TOWN_N               " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                       " & vbNewLine _
                                          & "                      $LM_MST$..M_ZIP    AS ZIP            " & vbNewLine _
                                          & "WHERE   ZIP.SYS_DEL_FLG    = '0'                        " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY             " & vbNewLine _
                                         & "     ZIP.ZIP_NO      " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    ''' 郵便番号マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ060DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ060DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), ""))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ060DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ060DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ060DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ060DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), ""))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ060DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZIP_NO", "ZIP_NO")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("KEN_K", "KEN_K")
        map.Add("CITY_K", "CITY_K")
        map.Add("TOWN_K", "TOWN_K")
        map.Add("KEN_N", "KEN_N")
        map.Add("CITY_N", "CITY_N")
        map.Add("TOWN_N", "TOWN_N")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ060OUT")

        Return ds

    End Function

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

            'whereStr = .Item(Trim(Replace("ZIP_NO", "-", ""))).ToString()
            whereStr = .Item("ZIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZIP.ZIP_NO LIKE @ZIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("KEN_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZIP.KEN_N LIKE @KEN_N")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_N", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CITY_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZIP.CITY_N LIKE @CITY_N")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_N", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("TOWN_N").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZIP.TOWN_N LIKE @TOWN_N")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOWN_N", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZIP.JIS_CD LIKE @JIS_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

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

End Class
