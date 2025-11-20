' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ320DAC : 支払横持ちタリフ照会
'  作  成  者       :  本明
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ320DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ320DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用(横持ちタリフヘッダマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_YOKO As String = " SELECT COUNT(YOKO_HD.YOKO_TARIFF_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' M_YOKO_TARIFF_HD_SHIHARAIデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_YOKO As String = " SELECT                                                 " & vbNewLine _
                                        & "      YOKO_HD.YOKO_TARIFF_CD            AS YOKO_TARIFF_CD        " & vbNewLine _
                                        & "     ,KBN.KBN_NM1                       AS CALC_KB_NM            " & vbNewLine _
                                        & "     ,YOKO_HD.CALC_KB                   AS CALC_KB               " & vbNewLine _
                                        & "     ,YOKO_HD.YOKO_REM                  AS YOKO_REM              " & vbNewLine _
                                        & "     ,YOKO_HD.NRS_BR_CD                 AS NRS_BR_CD             " & vbNewLine _
                                        & "     ,YOKO_HD.SPLIT_FLG                 AS SPLIT_FLG             " & vbNewLine _
                                        & "     ,YOKO_HD.YOKOMOCHI_MIN             AS YOKOMOCHI_MIN         " & vbNewLine

#Region "FROM句"

    ''' <summary>
    ''' 横持ちタリフヘッダ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_YOKO As String = "FROM                                                                " & vbNewLine _
                            & "                      $LM_MST$..M_YOKO_TARIFF_HD_SHIHARAI    AS YOKO_HD       " & vbNewLine _
                            & "      LEFT OUTER JOIN $LM_MST$..Z_KBN               AS KBN           " & vbNewLine _
                            & "        ON YOKO_HD.CALC_KB     = KBN.KBN_CD                          " & vbNewLine _
                            & "       AND KBN.KBN_GROUP_CD    = 'K012'                              " & vbNewLine _
                            & "       AND KBN.SYS_DEL_FLG     = '0'                                 " & vbNewLine _
                            & "WHERE   YOKO_HD.SYS_DEL_FLG    = '0'                                 " & vbNewLine

#End Region

#Region "GROUP BY"


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(横持ちヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_YOKO As String = "ORDER BY                          " & vbNewLine _
                                         & "     YOKO_HD.YOKO_TARIFF_CD            " & vbNewLine
#End Region


#Region "入力チェック"

#End Region

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
    ''' 検索件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ320DAC.SQL_SELECT_COUNT_YOKO)      'SQL構築(カウント用Select句:横持ちヘッダ)
        Me._StrSql.Append(LMZ320DAC.SQL_FROM_DATA_YOKO)         'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL("YOKO_HD")                '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ320DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        sql = Me.SelectYokomochiTariff()
        
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ320DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("CALC_KB_NM", "CALC_KB_NM")
        map.Add("CALC_KB", "CALC_KB")
        map.Add("YOKO_REM", "YOKO_REM")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SPLIT_FLG", "SPLIT_FLG")
        map.Add("YOKOMOCHI_MIN", "YOKOMOCHI_MIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ320OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectYokomochiTariff() As String

        'SQL作成
        Me._StrSql.Append(LMZ320DAC.SQL_SELECT_DATA_YOKO)       'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ320DAC.SQL_FROM_DATA_YOKO)         'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("YOKO_HD")                '条件設定
        Me._StrSql.Append(LMZ320DAC.SQL_ORDER_BY_YOKO)          'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal tblNm As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".NRS_BR_CD = @NRS_BR_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("YOKO_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".YOKO_TARIFF_CD LIKE @YOKO_TARIFF_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CALC_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND YOKO_HD.CALC_KB = @CALC_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CALC_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("YOKO_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND YOKO_HD.YOKO_REM LIKE @YOKO_REM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
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
