' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 在庫
'  プログラムID     :  LMD070DAC : 在庫印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD070DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"



#Region "検索"

    ''' <summary>
    ''' SQL(カウント)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT As String = "SELECT COUNT (RIREKI_DATE) AS SELECT_CNT                       " & vbNewLine


    ''' <summary>
    ''' SQL(検索)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = "SELECT  RIREKI_DATE                          " & vbNewLine

    ''' <summary>
    ''' SQL(カウント)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KB As String = "SELECT  COUNT(*)   AS SELECT_CNT                       " & vbNewLine



#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                               " & vbNewLine _
                                          & "      $LM_TRN$..D_ZAI_ZAN_JITSU AS JITSU                           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..D_ZAI_TRS  AS ZAI                  " & vbNewLine _
                                          & "        ON ZAI.NRS_BR_CD=@NRS_BR_CD                                " & vbNewLine _
                                          & "        AND ZAI.CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                          & "        AND ZAI.CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                           & "        AND ZAI.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                          & "      WHERE                                                        " & vbNewLine _
                                          & "        ZAI.NRS_BR_CD=  JITSU.NRS_BR_CD                            " & vbNewLine _
                                          & "        AND ZAI.ZAI_REC_NO=JITSU.ZAI_REC_NO                        " & vbNewLine

    Private Const SQL_FROM_GETU As String = "FROM                                                               " & vbNewLine _
                                          & "      $LM_TRN$..D_ZAI_ZAN_JITSU AS JITSU                           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..D_ZAI_TRS  AS ZAI                  " & vbNewLine _
                                          & "        ON ZAI.NRS_BR_CD=@NRS_BR_CD                                " & vbNewLine _
                                          & "        AND ZAI.CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                          & "        AND ZAI.CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                          & "        AND ZAI.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                          & "      WHERE                                                        " & vbNewLine _
                                          & "        ZAI.NRS_BR_CD=  JITSU.NRS_BR_CD                            " & vbNewLine _
                                          & "        AND ZAI.ZAI_REC_NO=JITSU.ZAI_REC_NO                        " & vbNewLine _
                                          & "        AND JITSU.RIREKI_DATE=@GETSUMATSU_ZAIKO                        " & vbNewLine

    Private Const SQL_FROM_DATA_KB As String = "FROM                                                               " & vbNewLine _
                                        & "      $LM_TRN$..B_INKA_L AS B                                " & vbNewLine _
                                        & "      LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L  AS C                  " & vbNewLine _
                                        & "        ON C.NRS_BR_CD=@NRS_BR_CD                             " & vbNewLine _
                                        & "        AND C.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                        & "        AND C.CUST_CD_M = @CUST_CD_M                  " & vbNewLine _
                                        & "        AND C.OUTKA_PLAN_DATE <= @DATA_TO                  " & vbNewLine _
                                         & "        AND C.OUTKA_STATE_KB < '60'                 " & vbNewLine _
                                         & "        AND C.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                        & "      WHERE                       " & vbNewLine _
                                         & "        B.NRS_BR_CD=@NRS_BR_CD                             " & vbNewLine _
                                        & "        AND B.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                        & "        AND B.CUST_CD_M = @CUST_CD_M                  " & vbNewLine _
                                        & "        AND B.INKA_DATE <= @DATA_TO                  " & vbNewLine _
                                         & "        AND B.INKA_STATE_KB < '50'            " & vbNewLine _
                                         & "        AND B.SYS_DEL_FLG ='0'                  " & vbNewLine


#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY  RIREKI_DATE                                                                                                                 " & vbNewLine


#End Region
#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY RIREKI_DATE          DESC                                                    " & vbNewLine


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

#Region "検索"

    ''' <summary>
    ''' 月末在庫件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD070_GETU_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'SQL作成
        Me._StrSql.Append(LMD070DAC.SQL_COUNT)      'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD070DAC.SQL_FROM_DATA)  'SQL構築(カウント用from句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD070DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds


    End Function

    ''' <summary>
    ''' 月末在庫データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD070_GETU_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'SQL作成
        Me._StrSql.Append(LMD070DAC.SQL_SELECT)           'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD070DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Me._StrSql.Append(LMD070DAC.SQL_GROUP_BY)         'SQL構築(カウント用GROUP_BY句)
        Me._StrSql.Append(LMD070DAC.SQL_ORDER_BY)         'SQL構築(カウント用ORDER_BY句)



        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF210DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RIREKI_DATE", "RIREKI_DATE")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD070_GETU_OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 完了件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function SelectDataKanryou(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化/設定
        Call Me.SetParamChk()
        'SQL作成
        Me._StrSql.Append(LMD070DAC.SQL_SELECT_KB)      'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD070DAC.SQL_FROM_DATA_KB)   'SQL構築(カウント用from句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD070DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds


    End Function
    ''' <summary>
    ''' 月末在庫件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function SelectDataSeigo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化/設定
        Call Me.SetParamChk()
        'SQL作成
        Me._StrSql.Append(LMD070DAC.SQL_COUNT)      'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD070DAC.SQL_FROM_GETU)  'SQL構築(カウント用from句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD070DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
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

#End Region

#End Region


#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))




        End With

    End Sub
    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TO", .Item("PRT_YMD_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GETSUMATSU_ZAIKO", .Item("GETSUMATSU_ZAIKO").ToString(), DBDataType.CHAR))



        End With

    End Sub


#End Region


#End Region

End Class

