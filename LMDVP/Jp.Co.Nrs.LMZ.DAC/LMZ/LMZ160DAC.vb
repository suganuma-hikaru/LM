' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ160DAC : 乗務員マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ160DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ160DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(DRIVER.DRIVER_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' DRIVER_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                        & "      DRIVER.YUSO_BR_CD                             AS NRS_BR_CD                    " & vbNewLine _
                                        & "     ,DRIVER.DRIVER_CD                              AS DRIVER_CD                    " & vbNewLine _
                                        & "     ,DRIVER.DRIVER_NM                              AS DRIVER_NM                    " & vbNewLine _
                                        & "     ,DRIVER.AVAL_YN                                AS AVAL_YN                      " & vbNewLine _
                                        & "     ,DRIVER.LCAR_LICENSE_YN                        AS LCAR_LICENSE_YN              " & vbNewLine _
                                        & "     ,DRIVER.TRAILER_LICENSE_YN                     AS TRAILER_LICENSE_YN           " & vbNewLine _
                                        & "     ,DRIVER.OTSU1_YN                               AS OTSU1_YN                     " & vbNewLine _
                                        & "     ,DRIVER.OTSU2_YN                               AS OTSU2_YN                     " & vbNewLine _
                                        & "     ,DRIVER.OTSU3_YN                               AS OTSU3_YN                     " & vbNewLine _
                                        & "     ,DRIVER.OTSU4_YN                               AS OTSU4_YN                     " & vbNewLine _
                                        & "     ,DRIVER.OTSU5_YN                               AS OTSU5_YN                     " & vbNewLine _
                                        & "     ,DRIVER.OTSU6_YN                               AS OTSU6_YN                     " & vbNewLine _
                                        & "     ,DRIVER.HICOMPGAS_YN                           AS HICOMPGAS_YN                 " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_DRIVER    AS DRIVER        " & vbNewLine _
                                          & "WHERE                 DRIVER.SYS_DEL_FLG    = '0'            " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "     DRIVER.DRIVER_CD,DRIVER.DRIVER_NM                                   " & vbNewLine

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
    ''' 乗務員マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ160DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ160DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ160DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 乗務員マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>乗務員マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ160IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ160DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ160DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ160DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ160DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DRIVER_CD", "DRIVER_CD")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("AVAL_YN", "AVAL_YN")
        map.Add("LCAR_LICENSE_YN", "LCAR_LICENSE_YN")
        map.Add("TRAILER_LICENSE_YN", "TRAILER_LICENSE_YN")
        map.Add("OTSU1_YN", "OTSU1_YN")
        map.Add("OTSU2_YN", "OTSU2_YN")
        map.Add("OTSU3_YN", "OTSU3_YN")
        map.Add("OTSU4_YN", "OTSU4_YN")
        map.Add("OTSU5_YN", "OTSU5_YN")
        map.Add("OTSU6_YN", "OTSU6_YN")
        map.Add("HICOMPGAS_YN", "HICOMPGAS_YN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ160OUT")

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

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND DRIVER.YUSO_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DRIVER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND DRIVER.DRIVER_CD LIKE @DRIVER_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("DRIVER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND DRIVER.DRIVER_NM LIKE @DRIVER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
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
