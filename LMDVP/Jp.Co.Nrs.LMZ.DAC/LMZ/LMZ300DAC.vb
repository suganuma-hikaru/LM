' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ300DAC : 支払割増運賃タリフマスタ照会
'  作  成  者       :  terakawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ300DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ300DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用(支払割増運賃タリフマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_EXTC As String = " SELECT COUNT(EXTC.EXTC_TARIFF_CD)		   AS SELECT_CNT   " & vbNewLine


    ''' <summary>
    ''' カウント用(運賃タリフセットマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_UNCHIN As String = " SELECT COUNT(UNCHIN_SETCNT.EXTC_TARIFF_CD)   AS SELECT_CNT      " & vbNewLine _
                                                    & " FROM   (                                                        " & vbNewLine




    ''' <summary>
    ''' M_EXTC_UNCHINデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_EXTC As String = " SELECT                                           " & vbNewLine _
                                        & "      EXTC.NRS_BR_CD                 AS NRS_BR_CD          " & vbNewLine _
                                        & "     ,''                             AS UNSOCO_CD          " & vbNewLine _
                                        & "     ,''                             AS UNSOCO_NM          " & vbNewLine _
                                        & "     ,''                             AS UNSOCO_BR_CD       " & vbNewLine _
                                        & "     ,''                             AS UNSOCO_BR_NM       " & vbNewLine _
                                        & "     ,EXTC.EXTC_TARIFF_CD            AS EXTC_TARIFF_CD     " & vbNewLine _
                                        & "     ,EXTC.EXTC_TARIFF_REM           AS EXTC_TARIFF_REM    " & vbNewLine


    ''' <summary>
    ''' M_UNCHIN_TARIFF_SETデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNCHIN As String = " SELECT                                                           " & vbNewLine _
                                                    & "     UNCHIN_SET.NRS_BR_CD                  AS NRS_BR_CD          " & vbNewLine _
                                                   & "     ,UNCHIN_SET.CUST_CD_L                  AS CUST_CD_L          " & vbNewLine _
                                                   & "     ,CUST.CUST_NM_L                        AS CUST_NM_L          " & vbNewLine _
                                                   & "     ,UNCHIN_SET.CUST_CD_M                  AS CUST_CD_M          " & vbNewLine _
                                                   & "     ,CUST.CUST_NM_M                        AS CUST_NM_M          " & vbNewLine _
                                                   & "     ,UNCHIN_SET.EXTC_TARIFF_CD             AS EXTC_TARIFF_CD     " & vbNewLine _
                                                   & "     ,EXTC.EXTC_TARIFF_REM                  AS EXTC_TARIFF_REM    " & vbNewLine

#End Region

#Region "FROM句"


    ''' <summary>
    ''' 支払割増運賃タリフ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_EXTC As String = "FROM                                                                                " & vbNewLine _
                                              & "      $LM_MST$..M_EXTC_SHIHARAI  AS EXTC                                                 " & vbNewLine _
                                              & "      WHERE                                                                          " & vbNewLine _
                                              & "           EXTC.EXTC_TARIFF_REM IS NOT NULL                                          " & vbNewLine _
                                              & "       AND EXTC.EXTC_TARIFF_REM <> ''                                                " & vbNewLine _
                                              & "       AND EXTC.JIS_CD = '0000000'                                                   " & vbNewLine _
                                              & "       AND                                                                           " & vbNewLine

    ''' <summary>
    ''' 運賃セットタリフ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_UNCHIN As String = "FROM                                                                   " & vbNewLine _
                                              & "           $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET        " & vbNewLine _
                                              & "      LEFT OUTER JOIN                                                     " & vbNewLine _
                                              & "           $LM_MST$..M_EXTC_SHIHARAI                   AS EXTC              " & vbNewLine _
                                              & "       ON UNCHIN_SET.NRS_BR_CD          = EXTC.NRS_BR_CD                  " & vbNewLine _
                                              & "      AND UNCHIN_SET.EXTC_TARIFF_CD     = EXTC.EXTC_TARIFF_CD             " & vbNewLine _
                                              & "      AND EXTC.SYS_DEL_FLG              = '0'                             " & vbNewLine _
                                              & "      AND EXTC.JIS_CD                   = '0000000'                       " & vbNewLine _
                                              & "      LEFT OUTER JOIN                                                     " & vbNewLine _
                                              & "                      ( SELECT                                            " & vbNewLine _
                                              & "                            UNSOCO.NRS_BR_CD                                " & vbNewLine _
                                              & "                           ,UNSOCO.UNSOCO_CD                               " & vbNewLine _
                                              & "                           ,UNSOCO.UNSOCO_NM                                " & vbNewLine _
                                              & "                           ,UNSOCO.UNSOCO_BR_CD                                " & vbNewLine _
                                              & "                           ,UNSOCO.UNSOCO_BR_NM                                " & vbNewLine _
                                              & "                        FROM  $LM_MST$..M_UNSOCO               AS UNSOCO      " & vbNewLine _
                                              & "                        WHERE SYS_DEL_FLG = '0'                           " & vbNewLine _
                                              & "                        GROUP BY   UNSOCO.NRS_BR_CD                         " & vbNewLine _
                                              & "                                  ,UNSOCO.UNSOCO_CD                         " & vbNewLine _
                                              & "                                  ,UNSOCO.UNSOCO_NM                         " & vbNewLine _
                                              & "                                  ,UNSOCO.UNSOCO_BR_CD                         " & vbNewLine _
                                              & "                                  ,UNSOCO.UNSOCO_BR_NM                         " & vbNewLine _
                                              & "                        ) AS UNSOCO                                         " & vbNewLine _
                                              & "        ON UNCHIN_SET.NRS_BR_CD          = UNSOCO.NRS_BR_CD                 " & vbNewLine _
                                              & "       AND UNCHIN_SET.CUST_CD_L          = UNSOCO.UNSO                 " & vbNewLine _
                                              & "       AND UNCHIN_SET.CUST_CD_M          = UNSOCO.CUST_CD_M                 " & vbNewLine _
                                              & "WHERE                                                                     " & vbNewLine _
                                              & "           UNCHIN_SET.SYS_DEL_FLG        = '0'                            " & vbNewLine _
                                              & "       AND EXTC.EXTC_TARIFF_REM          IS NOT NULL                      " & vbNewLine _
                                              & "       AND EXTC.EXTC_TARIFF_REM          <> ''                            " & vbNewLine _
                                              & "       AND                                                                " & vbNewLine

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY(運賃セット)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_UNCHINSET As String = "GROUP BY         UNCHIN_SET.NRS_BR_CD               " & vbNewLine _
                                                & "                ,UNCHIN_SET.CUST_CD_L               " & vbNewLine _
                                                & "                ,CUST.CUST_NM_L                     " & vbNewLine _
                                                & "                ,UNCHIN_SET.CUST_CD_M               " & vbNewLine _
                                                & "                ,CUST.CUST_NM_M                     " & vbNewLine _
                                                & "                ,UNCHIN_SET.EXTC_TARIFF_CD          " & vbNewLine _
                                                & "                ,EXTC.EXTC_TARIFF_REM               " & vbNewLine

#End Region



#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(割増運賃タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_EXTC As String = "ORDER BY                          " & vbNewLine _
                                         & "     EXTC.EXTC_TARIFF_CD            " & vbNewLine
    ''' <summary>
    ''' ORDER BY(運賃セット)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNCHIN As String = "ORDER BY                        " & vbNewLine _
                                         & "     UNCHIN_SET.EXTC_TARIFF_CD         " & vbNewLine


#End Region

#Region "カウント用テーブル名"

    ''' <summary>
    ''' 運賃セットマスタカウント用テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_TBLNM As String = "       ) AS UNCHIN_SETCNT"

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 荷主コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_UNSOCO As String = "SELECT                                                                   " & vbNewLine _
                                          & "                            UNSOCO.NRS_BR_CD       AS NRS_BR_CD             " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_CD       AS UNSOCO_CD             " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_NM       AS UNSOCO_NM             " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_BR_CD    AS UNSOCO_BR_CD          " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_BR_NM    AS UNSOCO_BR_NM          " & vbNewLine _
                                          & "                           ,UNSOCO.EXTC_TARIFF_CD  AS EXTC_TARIFF_CD        " & vbNewLine _
                                          & "                        FROM                                                " & vbNewLine _
                                          & "                      ( SELECT                                              " & vbNewLine _
                                          & "                            UNSOCO.NRS_BR_CD                                " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_CD                                " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_NM                                " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_BR_CD                             " & vbNewLine _
                                          & "                           ,UNSOCO.UNSOCO_BR_NM                             " & vbNewLine _
                                          & "                           ,UNSOCO.EXTC_TARIFF_CD                           " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_UNSOCO               AS UNSOCO    " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "                        GROUP BY   UNSOCO.NRS_BR_CD                         " & vbNewLine _
                                          & "                                  ,UNSOCO.UNSOCO_CD                         " & vbNewLine _
                                          & "                                  ,UNSOCO.UNSOCO_NM                         " & vbNewLine _
                                          & "                                  ,UNSOCO.UNSOCO_BR_CD                      " & vbNewLine _
                                          & "                                  ,UNSOCO.UNSOCO_BR_NM                      " & vbNewLine _
                                          & "                                  ,UNSOCO.EXTC_TARIFF_CD                    " & vbNewLine _
                                          & "                        ) AS UNSOCO                                         " & vbNewLine _
                                          & "WHERE                                                                       " & vbNewLine


    Private Const SQL_UNSOCO_GROUP As String = "GROUP BY                    UNSOCO.NRS_BR_CD                             " & vbNewLine _
                                          & "                            ,UNSOCO.UNSOCO_CD                               " & vbNewLine _
                                          & "                            ,UNSOCO.UNSOCO_NM                               " & vbNewLine _
                                          & "                            ,UNSOCO.UNSOCO_BR_CD                            " & vbNewLine _
                                          & "                            ,UNSOCO.UNSOCO_BR_NM                            " & vbNewLine _
                                          & "                            ,UNSOCO.EXTC_TARIFF_CD                          " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMZ300IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'If String.IsNullOrEmpty(Me._Row.Item("UNSOCO_CD").ToString()) = True Then
        Me._StrSql.Append(LMZ300DAC.SQL_SELECT_COUNT_EXTC)     'SQL構築(カウント用Select句:割増ヘッダ)
        Me._StrSql.Append(LMZ300DAC.SQL_FROM_DATA_EXTC)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL("EXTC")               '条件設定
        'Else
        '    Me._StrSql.Append(LMZ300DAC.SQL_SELECT_COUNT_UNCHIN)   'SQL構築(カウント用Select句:運賃タリフセット)
        '    Me._StrSql.Append(LMZ300DAC.SQL_SELECT_DATA_UNCHIN)      'SQL構築(データ抽出用Select句)
        '    Me._StrSql.Append(LMZ300DAC.SQL_FROM_DATA_UNCHIN)      'SQL構築(カウント用from句)
        '    Call Me.SetConditionMasterSQL("UNCHIN_SET")            '条件設定
        '    Me._StrSql.Append(LMZ300DAC.SQL_GROUP_UNCHINSET)      'SQL構築(GROUP BY句)
        '    Me._StrSql.Append(LMZ300DAC.SQL_COUNT_TBLNM)          'SQL構築(カウント用テーブル名)
        'End If


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ300DAC", "SelectData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMZ300IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        'If String.IsNullOrEmpty(Me._Row.Item("UNSOCO_CD").ToString()) = True Then
        sql = Me.SelectExtc()
        'Else
        '    sql = Me.SelectUnchinTariff()
        'End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ300DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ300OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ300IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim sql As String = String.Empty

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        sql = Me.SelecrtUnsocoExistChk()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ300DAC", "CheckExistUnsocoM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD") '追加 2012.08.20

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ300UNSOCO")

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃タリフマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectExtc() As String

        'SQL作成
        Me._StrSql.Append(LMZ300DAC.SQL_SELECT_DATA_EXTC)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ300DAC.SQL_FROM_DATA_EXTC)             'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("EXTC")                   '条件設定
        Me._StrSql.Append(LMZ300DAC.SQL_ORDER_BY_EXTC)         'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    '''  運賃タリフセットマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariff() As String

        'SQL作成
        Me._StrSql.Append(LMZ300DAC.SQL_SELECT_DATA_UNCHIN)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ300DAC.SQL_FROM_DATA_UNCHIN)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("UNCHIN_SET")                   '条件設定
        Me._StrSql.Append(LMZ300DAC.SQL_GROUP_UNCHINSET)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMZ300DAC.SQL_ORDER_BY_UNCHIN)         'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック用SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelecrtUnsocoExistChk() As String

        'SQL作成
        Me._StrSql.Append(LMZ300DAC.SQL_EXIST_UNSOCO)      'SQL構築(運送会社マスタ存在チェック用Select句)
        Call Me.SetUnsocoMasterSQL()                       '条件設定
        Me._StrSql.Append(LMZ300DAC.SQL_UNSOCO_GROUP)      'SQL構築(運送会社マスタ存在チェック用Group句)
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
                Me._StrSql.Append(String.Concat(" ", tblNm, ".NRS_BR_CD = @NRS_BR_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'whereStr = .Item("UNSOCO_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("AND UNCHIN_SET.UNSOCO_CD = @UNSOCO_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", whereStr, DBDataType.CHAR))

            '    whereStr = .Item("UNSOCO_BR_CD").ToString()
            '    If String.IsNullOrEmpty(whereStr) = False Then
            '        Me._StrSql.Append("AND UNCHIN_SET.UNSOCO_BR_CD = @UNSOCO_BR_CD")
            '        Me._StrSql.Append(vbNewLine)
            '        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", whereStr, DBDataType.CHAR))
            '    Else
            '        Me._StrSql.Append(String.Concat("AND UNCHIN_SET.UNSOCO_BR_CD = ", "'00'"))
            '        Me._StrSql.Append(vbNewLine)
            '    End If
            'End If

            whereStr = .Item("EXTC_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(String.Concat("AND ", tblNm, ".EXTC_TARIFF_CD LIKE @EXTC_TARIFF_CD"))
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("EXTC_TARIFF_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND EXTC.EXTC_TARIFF_REM LIKE @EXTC_TARIFF_REM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    Private Sub SetUnsocoMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("UNSOCO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_CD = @UNSOCO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", whereStr, DBDataType.CHAR))

                whereStr = .Item("UNSOCO_BR_CD").ToString()
                'If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_BR_CD = @UNSOCO_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", whereStr, DBDataType.CHAR))
                'Else
                '    Me._StrSql.Append(String.Concat("AND UNSOCO.UNSOCO_BR_CD = ", "00"))
                '    Me._StrSql.Append(vbNewLine)
                'End If
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
