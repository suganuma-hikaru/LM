' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ100DAC : 横持ちタリフ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ100DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ100DAC
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
    ''' カウント用(運賃タリフセットマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_UNCHIN As String = " SELECT COUNT(UNCHIN_SETCNT.YOKO_TARIFF_CD)   AS SELECT_CNT   " & vbNewLine _
                                        & "                  FROM  (                                         " & vbNewLine


    ''' <summary>
    ''' M_YOKO_TARIFF_HDデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_YOKO As String = " SELECT                                                 " & vbNewLine _
                                        & "      YOKO_HD.YOKO_TARIFF_CD            AS YOKO_TARIFF_CD        " & vbNewLine _
                                        & "     ,KBN.KBN_NM1                       AS CALC_KB_NM            " & vbNewLine _
                                        & "     ,YOKO_HD.CALC_KB                   AS CALC_KB               " & vbNewLine _
                                        & "     ,YOKO_HD.YOKO_REM                  AS YOKO_REM              " & vbNewLine _
                                        & "     ,YOKO_HD.NRS_BR_CD                 AS NRS_BR_CD             " & vbNewLine _
                                        & "     ,''                                AS CUST_CD_L             " & vbNewLine _
                                        & "     ,''                                AS CUST_NM_L             " & vbNewLine _
                                        & "     ,''                                AS CUST_CD_M             " & vbNewLine _
                                        & "     ,''                                AS CUST_NM_M             " & vbNewLine _
                                        & "     ,''                                AS SET_MST_CD            " & vbNewLine _
                                        & "     ,''                                AS DEST_CD               " & vbNewLine _
                                        & "     ,''                                AS SET_KB                " & vbNewLine _
                                        & "     ,''                                AS TARIFF_BUNRUI_KB      " & vbNewLine _
                                        & "     ,''                                AS UNCHIN_TARIFF_CD1     " & vbNewLine _
                                        & "     ,''                                AS UNCHIN_TARIFF_CD2     " & vbNewLine _
                                        & "     ,''                                AS EXTC_TARIFF_CD        " & vbNewLine _
                                        & "     ,''                                AS EXTC_TARIFF_REM       " & vbNewLine _
                                        & "     ,YOKO_HD.SPLIT_FLG                 AS SPLIT_FLG             " & vbNewLine _
                                        & "     ,YOKO_HD.YOKOMOCHI_MIN             AS YOKOMOCHI_MIN         " & vbNewLine



    ''' <summary>
    ''' M_UNCHIN_TARIFF_SETデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNCHIN As String = " SELECT                                                           " & vbNewLine _
                                                   & "      UNCHIN_SET.YOKO_TARIFF_CD             AS YOKO_TARIFF_CD     " & vbNewLine _
                                                   & "     ,KBN.KBN_NM1                           AS CALC_KB_NM         " & vbNewLine _
                                                   & "     ,YOKO_HD.CALC_KB                       AS CALC_KB            " & vbNewLine _
                                                   & "     ,YOKO_HD.YOKO_REM                      AS YOKO_REM           " & vbNewLine _
                                                   & "     ,UNCHIN_SET.NRS_BR_CD                  AS NRS_BR_CD          " & vbNewLine _
                                                   & "     ,UNCHIN_SET.CUST_CD_L                  AS CUST_CD_L          " & vbNewLine _
                                                   & "     ,CUST.CUST_NM_L                        AS CUST_NM_L          " & vbNewLine _
                                                   & "     ,UNCHIN_SET.CUST_CD_M                  AS CUST_CD_M          " & vbNewLine _
                                                   & "     ,CUST.CUST_NM_M                        AS CUST_NM_M          " & vbNewLine _
                                                   & "     ,''                                    AS SET_MST_CD         " & vbNewLine _
                                                   & "     ,''                                    AS DEST_CD            " & vbNewLine _
                                                   & "     ,''                                    AS SET_KB             " & vbNewLine _
                                                   & "     ,''                                    AS TARIFF_BUNRUI_KB   " & vbNewLine _
                                                   & "     ,''                                    AS UNCHIN_TARIFF_CD1  " & vbNewLine _
                                                   & "     ,''                                    AS UNCHIN_TARIFF_CD2  " & vbNewLine _
                                                   & "     ,UNCHIN_SET.EXTC_TARIFF_CD             AS EXTC_TARIFF_CD     " & vbNewLine _
                                                   & "     ,EXTC.EXTC_TARIFF_REM                  AS EXTC_TARIFF_REM    " & vbNewLine _
                                                   & "     ,YOKO_HD.SPLIT_FLG                     AS SPLIT_FLG          " & vbNewLine _
                                                   & "     ,YOKO_HD.YOKOMOCHI_MIN                 AS YOKOMOCHI_MIN      " & vbNewLine

#End Region

#Region "FROM句"


    ''' <summary>
    ''' 横持ちタリフヘッダ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_YOKO As String = "FROM                                                                " & vbNewLine _
                                              & "                      $LM_MST$..M_YOKO_TARIFF_HD    AS YOKO_HD       " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN               AS KBN           " & vbNewLine _
                                              & "        ON YOKO_HD.CALC_KB     = KBN.KBN_CD                          " & vbNewLine _
                                              & "       AND KBN.KBN_GROUP_CD    = 'K012'                              " & vbNewLine _
                                              & "       AND KBN.SYS_DEL_FLG     = '0'                                 " & vbNewLine _
                                              & "WHERE   YOKO_HD.SYS_DEL_FLG    = '0'                                 " & vbNewLine


    Private Const SQL_FROM_DATA_UNCHIN As String = "FROM                                                               " & vbNewLine _
                                          & "           $LM_MST$..M_UNCHIN_TARIFF_SET             AS UNCHIN_SET        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_YOKO_TARIFF_HD     AS YOKO_HD           " & vbNewLine _
                                          & "        ON UNCHIN_SET.YOKO_TARIFF_CD     = YOKO_HD.YOKO_TARIFF_CD         " & vbNewLine _
                                          & "       AND UNCHIN_SET.NRS_BR_CD          = YOKO_HD.NRS_BR_CD              " & vbNewLine _
                                          & "       AND YOKO_HD.SYS_DEL_FLG           = '0'                            " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_EXTC_UNCHIN     AS EXTC                 " & vbNewLine _
                                          & "        ON UNCHIN_SET.EXTC_TARIFF_CD     = EXTC.EXTC_TARIFF_CD            " & vbNewLine _
                                          & "       AND UNCHIN_SET.NRS_BR_CD          = EXTC.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND EXTC.JIS_CD                   = '0000000'                      " & vbNewLine _
                                          & "       AND EXTC.SYS_DEL_FLG              = '0'                            " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                AS KBN               " & vbNewLine _
                                          & "        ON YOKO_HD.CALC_KB               = KBN.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN.KBN_GROUP_CD              = 'K012'                         " & vbNewLine _
                                          & "       AND KBN.SYS_DEL_FLG               = '0'                            " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                     " & vbNewLine _
                                          & "                      ( SELECT                                            " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M                                " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S            " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS           " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_CUST               AS CUST      " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                           " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_M                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_M                         " & vbNewLine _
                                          & "                        ) AS CUST                                         " & vbNewLine _
                                          & "        ON UNCHIN_SET.NRS_BR_CD          = CUST.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND UNCHIN_SET.CUST_CD_L          = CUST.CUST_CD_L                 " & vbNewLine _
                                          & "       AND UNCHIN_SET.CUST_CD_M          = CUST.CUST_CD_M                 " & vbNewLine _
                                          & "WHERE      UNCHIN_SET.SYS_DEL_FLG        = '0'                            " & vbNewLine _
                                          & "       AND YOKO_HD.YOKO_REM             <> ''                             " & vbNewLine _
                                          & "       AND YOKO_HD.YOKO_REM             IS NOT NULL                       " & vbNewLine

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY(運賃セット)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_UNCHINSET As String = "GROUP BY         UNCHIN_SET.YOKO_TARIFF_CD         " & vbNewLine _
                                                & "                ,KBN.KBN_NM1                       " & vbNewLine _
                                                & "                ,YOKO_HD.CALC_KB                   " & vbNewLine _
                                                & "                ,YOKO_HD.YOKO_REM                  " & vbNewLine _
                                                & "                ,UNCHIN_SET.NRS_BR_CD              " & vbNewLine _
                                                & "                ,UNCHIN_SET.CUST_CD_L              " & vbNewLine _
                                                & "                ,CUST.CUST_NM_L                    " & vbNewLine _
                                                & "                ,UNCHIN_SET.CUST_CD_M              " & vbNewLine _
                                                & "                ,CUST.CUST_NM_M                    " & vbNewLine _
                                                & "                ,UNCHIN_SET.EXTC_TARIFF_CD         " & vbNewLine _
                                                & "                ,EXTC.EXTC_TARIFF_REM              " & vbNewLine _
                                                & "                ,YOKO_HD.SPLIT_FLG                 " & vbNewLine _
                                                & "                ,YOKO_HD.YOKOMOCHI_MIN             " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(横持ちヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_YOKO As String = "ORDER BY                          " & vbNewLine _
                                         & "     YOKO_HD.YOKO_TARIFF_CD            " & vbNewLine
    ''' <summary>
    ''' ORDER BY(運賃セット)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNCHIN As String = "ORDER BY                        " & vbNewLine _
                                         & "     UNCHIN_SET.YOKO_TARIFF_CD         " & vbNewLine

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
    Private Const SQL_EXIST_CUST As String = "SELECT                                                                   " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD       AS NRS_BR_CD             " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L       AS CUST_CD_L             " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L       AS CUST_NM_L             " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M       AS CUST_CD_M             " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M       AS CUST_NM_M             " & vbNewLine _
                                          & "                        FROM                                              " & vbNewLine _
                                          & "                      ( SELECT                                            " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M                                " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M                                " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S            " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS           " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_CUST               AS CUST      " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                           " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_M                         " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_M                         " & vbNewLine _
                                          & "                        ) AS CUST                                         " & vbNewLine _
                                          & "WHERE                                                                     " & vbNewLine


    Private Const SQL_CUST_GROUP As String = "GROUP BY                    CUST.NRS_BR_CD                               " & vbNewLine _
                                          & "                            ,CUST.CUST_CD_L                               " & vbNewLine _
                                          & "                            ,CUST.CUST_NM_L                               " & vbNewLine _
                                          & "                            ,CUST.CUST_CD_M                               " & vbNewLine _
                                          & "                            ,CUST.CUST_NM_M                               " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMZ100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If String.IsNullOrEmpty(Me._Row.Item("CUST_CD_L").ToString()) = True Then
            Me._StrSql.Append(LMZ100DAC.SQL_SELECT_COUNT_YOKO)     'SQL構築(カウント用Select句:横持ちヘッダ)
            Me._StrSql.Append(LMZ100DAC.SQL_FROM_DATA_YOKO)        'SQL構築(カウント用from句)
            Call Me.SetConditionMasterSQL("YOKO_HD")               '条件設定
        Else
            Me._StrSql.Append(LMZ100DAC.SQL_SELECT_COUNT_UNCHIN)   'SQL構築(カウント用Select句:運賃タリフセット)
            Me._StrSql.Append(LMZ100DAC.SQL_SELECT_DATA_UNCHIN)   'SQL構築(Select句:運賃タリフセット)
            Me._StrSql.Append(LMZ100DAC.SQL_FROM_DATA_UNCHIN)      'SQL構築(from句)
            Call Me.SetConditionMasterSQL("UNCHIN_SET")            '条件設定
            Me._StrSql.Append(LMZ100DAC.SQL_GROUP_UNCHINSET)      'SQL構築(GroupBy)
            Me._StrSql.Append(LMZ100DAC.SQL_COUNT_TBLNM)          'SQL構築(カウント用テーブル名)
        End If


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ100DAC", "SelectData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMZ100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        If String.IsNullOrEmpty(Me._Row.Item("CUST_CD_L").ToString()) = True Then
            sql = Me.SelectYokomochiTariff()
        Else
            sql = Me.SelectUnchinTariff()
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ100DAC", "SelectListData", cmd)

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
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SET_MST_CD", "SET_MST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SET_KB", "SET_KB")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("UNCHIN_TARIFF_CD1", "UNCHIN_TARIFF_CD1")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("SPLIT_FLG", "SPLIT_FLG")
        map.Add("YOKOMOCHI_MIN", "YOKOMOCHI_MIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ100OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistCustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim sql As String = String.Empty

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        sql = Me.SelecrtCustExistChk()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ100DAC", "CheckExistCustM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ100CUST")

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectYokomochiTariff() As String

        'SQL作成
        Me._StrSql.Append(LMZ100DAC.SQL_SELECT_DATA_YOKO)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ100DAC.SQL_FROM_DATA_YOKO)             'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("YOKO_HD")                   '条件設定
        Me._StrSql.Append(LMZ100DAC.SQL_ORDER_BY_YOKO)         'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    '''  運賃タリフセットマスタ検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariff() As String

        'SQL作成
        Me._StrSql.Append(LMZ100DAC.SQL_SELECT_DATA_UNCHIN)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ100DAC.SQL_FROM_DATA_UNCHIN)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL("UNCHIN_SET")                   '条件設定
        Me._StrSql.Append(LMZ100DAC.SQL_GROUP_UNCHINSET)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMZ100DAC.SQL_ORDER_BY_UNCHIN)         'SQL構築(データ抽出用ORDER BY句)

        Return Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック用SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelecrtCustExistChk() As String

        'SQL作成
        Me._StrSql.Append(LMZ100DAC.SQL_EXIST_CUST)      'SQL構築(荷主マスタ存在チェック用Select句)
        Call Me.SetCustMasterSQL()                       '条件設定
        Me._StrSql.Append(LMZ100DAC.SQL_CUST_GROUP)      'SQL構築(荷主マスタ存在チェック用Group句)
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

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNCHIN_SET.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                whereStr = .Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("AND UNCHIN_SET.CUST_CD_M = @CUST_CD_M")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                Else
                    Me._StrSql.Append(String.Concat("AND UNCHIN_SET.CUST_CD_M = ", "'00'"))
                    Me._StrSql.Append(vbNewLine)
                End If
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

    Private Sub SetCustMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("CUST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                whereStr = .Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("AND CUST.CUST_CD_M = @CUST_CD_M")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                Else
                    Me._StrSql.Append(String.Concat("AND CUST.CUST_CD_M = ", "00"))
                    Me._StrSql.Append(vbNewLine)
                End If
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
