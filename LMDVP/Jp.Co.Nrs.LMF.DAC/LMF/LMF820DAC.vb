' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF820    : 車載受注渡し処理
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMF820DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF820DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SELECT処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' 車載受注渡しデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                " & vbNewLine _
                                            & "        CASE WHEN UNSO_L.NRS_BR_CD = 10 THEN '10320'                                   " & vbNewLine _
                                            & "             WHEN UNSO_L.NRS_BR_CD = 20 THEN '10324'                                   " & vbNewLine _
                                            & "             WHEN UNSO_L.NRS_BR_CD = 30 THEN '10321'                                   " & vbNewLine _
                                            & "             WHEN UNSO_L.NRS_BR_CD = 50 THEN '10322'                                   " & vbNewLine _
                                            & "        ELSE ''                                                                        " & vbNewLine _
                                            & "        END                                AS EGS_CD           --営業所コード          " & vbNewLine _
                                            & "      , UNSO_L.TRIP_NO                     AS JYT_NO           --受注番号              " & vbNewLine _
                                            & "      , ''                                 AS JYT_NO_ED        --受注番号枝番          " & vbNewLine _
                                            & "      , UNSO_L.OUTKA_PLAN_DATE             AS JYT_DATE         --受注日                " & vbNewLine _
                                            & "      , UNSO_L.SYS_ENT_USER                AS JYTTNT_CD        --受注作業者            " & vbNewLine _
                                            & "      , UNSO_L.ARR_PLAN_DATE               AS YOT_SGYSTR_DATE  --配送予定日            " & vbNewLine _
                                            & "      , ''                                 AS YOT_SGYSTR_TIME  --配送予定時間          " & vbNewLine _
                                            & "      , UNSO_L.UNSO_WT                     AS SURYO            --運送重量              " & vbNewLine _
                                            & "      , OUTKA_PLAN_DATE                    AS SGY_NO1          --作業№1               " & vbNewLine _
                                            & "      , UNSO_L.UNSO_NO_L                   AS SGY_NO2          --作業№2               " & vbNewLine _
                                            & "      , CASE WHEN UNSO_LL.DRIVER_CD IS NULL  THEN '99999'                              " & vbNewLine _
                                            & "        ELSE UNSO_LL.DRIVER_CD                                                         " & vbNewLine _
                                            & "        END                                AS YOT_JMI_CD1      --乗務員コード          " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_L                   AS NNU_TRSRYK_MEI   --荷主略称              " & vbNewLine _
                                            & "      , M_DEST.DEST_NM                     AS OROSI_TRSRYK_MEI --積降略称              " & vbNewLine _
                                            & "      , M_DEST.AD_1                        AS OROSI_KUIKI_MEI  --積降区域              " & vbNewLine _
                                            & "      , UNSO_M.GOODS_NM                    AS HINMEIRYK        --品名略称              " & vbNewLine _
                                            & "      , CASE WHEN UNSO_L.MOTO_DATA_KB = 10 THEN '積'                                   " & vbNewLine _
                                            & "        ELSE '卸'                                                                      " & vbNewLine _
                                            & "        END                                AS SGY_MEI          --作業名                " & vbNewLine _
                                            & "      , SUM(ISNULL(UNCHIN.DECI_UNCHIN,0))  AS SOKO_UNCHIN_01   --倉庫_運賃01           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_02   --倉庫_運賃02           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_03   --倉庫_運賃03           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_04   --倉庫_運賃04           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_05   --倉庫_運賃05           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_06   --倉庫_運賃06           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_07   --倉庫_運賃07           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_08   --倉庫_運賃08           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_09   --倉庫_運賃09           " & vbNewLine _
                                            & "      , 0                                  AS SOKO_UNCHIN_10   --倉庫_運賃10           " & vbNewLine _
                                            & "      , ''                                 AS SOKO_UNCHIN_BIKO --倉庫_運賃備考         " & vbNewLine _
                                            & "      , UNSO_L.NRS_BR_CD                   AS NRS_BR_CD        --営業所コード(制御用)  " & vbNewLine _
                                            & "      , UNSO_L.MOTO_DATA_KB                AS MOTO_DATA_KB     --元データ区分(制御用)  " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_SELECT_FROM As String = " FROM $LM_TRN$..F_UNSO_L UNSO_L                        " & vbNewLine _
                                            & "      --運送(中)                                       " & vbNewLine _
                                            & "      LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M              " & vbNewLine _
                                            & "             ON UNSO_M.NRS_BR_CD = UNSO_L.NRS_BR_CD    " & vbNewLine _
                                            & "            AND UNSO_M.UNSO_NO_L = UNSO_L.UNSO_NO_L    " & vbNewLine _
                                            & "            AND UNSO_M.SYS_DEL_FLG = '0'               " & vbNewLine _
                                            & "      --運賃                                           " & vbNewLine _
                                            & "      LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UNCHIN          " & vbNewLine _
                                            & "             ON UNCHIN.NRS_BR_CD = UNSO_L.NRS_BR_CD    " & vbNewLine _
                                            & "            AND UNCHIN.UNSO_NO_L = UNSO_L.UNSO_NO_L    " & vbNewLine _
                                            & "            AND UNCHIN.SYS_DEL_FLG = '0'               " & vbNewLine _
                                            & "      --運行                                           " & vbNewLine _
                                            & "      LEFT JOIN $LM_TRN$..F_UNSO_LL UNSO_LL            " & vbNewLine _
                                            & "             ON UNSO_LL.NRS_BR_CD = UNSO_L.NRS_BR_CD   " & vbNewLine _
                                            & "            AND UNSO_LL.TRIP_NO   = UNSO_L.TRIP_NO     " & vbNewLine _
                                            & "            AND UNSO_LL.SYS_DEL_FLG = '0'              " & vbNewLine _
                                            & "      --届先Ｍ                                         " & vbNewLine _
                                            & "      LEFT JOIN $LM_MST$..M_DEST M_DEST                " & vbNewLine _
                                            & "             ON M_DEST.NRS_BR_CD = UNSO_L.NRS_BR_CD    " & vbNewLine _
                                            & "            AND M_DEST.CUST_CD_L = UNSO_L.CUST_CD_L    " & vbNewLine _
                                            & "            AND M_DEST.DEST_CD   = UNSO_L.DEST_CD      " & vbNewLine _
                                            & "      --荷主Ｍ                                         " & vbNewLine _
                                            & "      LEFT JOIN $LM_MST$..M_CUST M_CUST                " & vbNewLine _
                                            & "             ON M_CUST.NRS_BR_CD  = UNSO_L.NRS_BR_CD   " & vbNewLine _
                                            & "            AND M_CUST.CUST_CD_L  = UNSO_L.CUST_CD_L   " & vbNewLine _
                                            & "            AND M_CUST.CUST_CD_M  = UNSO_L.CUST_CD_M   " & vbNewLine _
                                            & "            AND M_CUST.CUST_CD_S  = '00'               " & vbNewLine _
                                            & "            AND M_CUST.CUST_CD_SS = '00'               " & vbNewLine

#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                      " & vbNewLine _
                                         & "       UNSO_L.NRS_BR_CD        " & vbNewLine _
                                         & "     , UNSO_L.TRIP_NO          " & vbNewLine _
                                         & "     , UNSO_L.OUTKA_PLAN_DATE  " & vbNewLine _
                                         & "     , UNSO_L.ARR_PLAN_DATE    " & vbNewLine _
                                         & "     , UNSO_L.UNSO_WT          " & vbNewLine _
                                         & "     , UNSO_L.UNSO_NO_L        " & vbNewLine _
                                         & "     , UNSO_L.MOTO_DATA_KB     " & vbNewLine _
                                         & "     , UNSO_L.SYS_ENT_USER     " & vbNewLine _
                                         & "     , UNSO_M.GOODS_NM         " & vbNewLine _
                                         & "     , UNSO_LL.DRIVER_CD       " & vbNewLine _
                                         & "     , M_CUST.CUST_NM_L        " & vbNewLine _
                                         & "     , M_DEST.DEST_NM          " & vbNewLine _
                                         & "     , M_DEST.AD_1             " & vbNewLine _
                                         & "     , UNSO_M.UNSO_NO_M        " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                           " & vbNewLine _
                                         & "       UNSO_L.NRS_BR_CD             " & vbNewLine _
                                         & "     , UNSO_L.TRIP_NO               " & vbNewLine _
                                         & "     , UNSO_L.UNSO_NO_L             " & vbNewLine _
                                         & "     , UNSO_M.UNSO_NO_M             " & vbNewLine

#End Region

#End Region

#Region "INSERT処理 SQL"

    ''' <summary>
    ''' 車載受注渡しデータ追加処理
    ''' </summary>
    ''' <remarks>車載システム側へINSERT</remarks>
    Private Const SQL_INSERT As String = " INSERT INTO T_SYASAI_JUTYU_SOKO " & vbNewLine _
                                       & "   VALUES (                      " & vbNewLine _
                                       & "            @EGS_CD              " & vbNewLine _
                                       & "          , @JYT_NO              " & vbNewLine _
                                       & "          , @JYT_NO_ED           " & vbNewLine _
                                       & "          , @JYT_DATE            " & vbNewLine _
                                       & "          , @JYTTNT_CD           " & vbNewLine _
                                       & "          , @YOT_SGYSTR_DATE     " & vbNewLine _
                                       & "          , @YOT_SGYSTR_TIME     " & vbNewLine _
                                       & "          , @SURYO               " & vbNewLine _
                                       & "          , @SGY_NO1             " & vbNewLine _
                                       & "          , @SGY_NO2             " & vbNewLine _
                                       & "          , @YOT_JMI_CD1         " & vbNewLine _
                                       & "          , @NNU_TRSRYK_MEI      " & vbNewLine _
                                       & "          , @OROSI_TRSRYK_MEI    " & vbNewLine _
                                       & "          , @OROSI_KUIKI_MEI     " & vbNewLine _
                                       & "          , @HINMEIRYK           " & vbNewLine _
                                       & "          , @SGY_MEI             " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_01      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_02      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_03      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_04      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_05      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_06      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_07      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_08      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_09      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_10      " & vbNewLine _
                                       & "          , @SOKO_UNCHIN_BIKO    " & vbNewLine _
                                       & "          , @PG_ID               " & vbNewLine _
                                       & "          , GETDATE()            " & vbNewLine _
                                       & "          , @KOUSIN_BI           " & vbNewLine _
                                       & "          , @SAKUJYO_BI          " & vbNewLine _
                                       & "          , @TOUROKU_UID         " & vbNewLine _
                                       & "          , @KOUSIN_UID          " & vbNewLine _
                                       & "          , @SAKUJYO_UID         " & vbNewLine _
                                       & "          , @SAKUJYO_FLG         " & vbNewLine _
                                       & "          )                      " & vbNewLine

#End Region

#Region "DELETE処理 SQL"

    ''' <summary>
    ''' 車載受注渡しデータ削除処理
    ''' </summary>
    ''' <remarks>車載システム側 DELETE</remarks>
    Private Const SQL_DELETE As String = " DELETE FROM T_SYASAI_JUTYU_SOKO  " & vbNewLine

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
    ''' 車載受注渡し対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSyasaiWatashi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF820IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF820DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF820DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF820DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMF820DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF820DAC", "SelectSyasaiWatashi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EGS_CD", "EGS_CD")
        map.Add("JYT_NO", "JYT_NO")
        map.Add("JYT_NO_ED", "JYT_NO_ED")
        map.Add("JYT_DATE", "JYT_DATE")
        map.Add("JYTTNT_CD", "JYTTNT_CD")
        map.Add("YOT_SGYSTR_DATE", "YOT_SGYSTR_DATE")
        map.Add("YOT_SGYSTR_TIME", "YOT_SGYSTR_TIME")
        map.Add("SURYO", "SURYO")
        map.Add("SGY_NO1", "SGY_NO1")
        map.Add("SGY_NO2", "SGY_NO2")
        map.Add("YOT_JMI_CD1", "YOT_JMI_CD1")
        map.Add("NNU_TRSRYK_MEI", "NNU_TRSRYK_MEI")
        map.Add("OROSI_TRSRYK_MEI", "OROSI_TRSRYK_MEI")
        map.Add("OROSI_KUIKI_MEI", "OROSI_KUIKI_MEI")
        map.Add("HINMEIRYK", "HINMEIRYK")
        map.Add("SGY_MEI", "SGY_MEI")
        map.Add("SOKO_UNCHIN_01", "SOKO_UNCHIN_01")
        map.Add("SOKO_UNCHIN_02", "SOKO_UNCHIN_02")
        map.Add("SOKO_UNCHIN_03", "SOKO_UNCHIN_03")
        map.Add("SOKO_UNCHIN_04", "SOKO_UNCHIN_04")
        map.Add("SOKO_UNCHIN_05", "SOKO_UNCHIN_05")
        map.Add("SOKO_UNCHIN_06", "SOKO_UNCHIN_06")
        map.Add("SOKO_UNCHIN_07", "SOKO_UNCHIN_07")
        map.Add("SOKO_UNCHIN_08", "SOKO_UNCHIN_08")
        map.Add("SOKO_UNCHIN_09", "SOKO_UNCHIN_09")
        map.Add("SOKO_UNCHIN_10", "SOKO_UNCHIN_10")
        map.Add("SOKO_UNCHIN_BIKO", "SOKO_UNCHIN_BIKO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF820OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" UNSO_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運送管理番号(大)
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_L.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "追加処理"

#Region "メイン処理"

    ''' <summary>
    ''' 車載受注渡しデータ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>車載受注渡しデータ更新SQLの構築・発行</remarks>
    Private Function SyasaiJutyuWatashi(ByVal ds As DataSet) As DataSet

        '別インスタンス格納
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMF820OUT")

        Dim rtnDs As DataSet = Nothing
        Dim max As Integer = ds.Tables("LMF820OUT").Rows.Count - 1   '最大件数格納

        '他DBへ直接接続
        Using conYuso As SqlConnection = New SqlConnection

            Try
                conYuso.ConnectionString = ConnectionMGR.GetConnectionString_DirectConnection(ConnectionMGR.ConnectionDb.YUSO)
                conYuso.Open()

                For i As Integer = 0 To max

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    setDt.ImportRow(ds.Tables("LMF820OUT").Rows(i))

                    '①車載受注渡し対象データの削除 ----------------------
                    rtnDs = Me.DeleteSyasaiWatashi(setDs, conYuso)

                    '②車載受注渡し対象データの更新 ----------------------
                    rtnDs = Me.InsertSyasaiWatashi(setDs, conYuso)

                Next

            Catch ex As Exception
                Throw
            Finally
                conYuso.Close()
                conYuso.Dispose()

            End Try

        End Using

        Return rtnDs

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 車載受注渡しデータ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>車載受注渡しデータ削除SQLの構築・発行</remarks>
    Private Function DeleteSyasaiWatashi(ByVal ds As DataSet, ByVal conYuso As SqlConnection) As DataSet

        'DataSetのOUT情報を取得
        Dim outTbl As DataTable = ds.Tables("LMF820OUT")

        'INTableの条件rowの格納
        Me._Row = outTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '削除登録処理　---------------------------------------------------

        'SQL作成
        Me._StrSql.Append(LMF820DAC.SQL_DELETE) 'SQL構築(データ抽出用DELETE句)
        Call Me.SetConditionMasterDEL_SQL()     'SQL構築(条件設定)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommandYuso(Me._StrSql.ToString(), conYuso)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF820DAC", "Delete_T_SYASAI_JUTYU_SOKO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterDEL_SQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '(2012.09.07) 修正START
            '千葉・大阪・群馬・埼玉以外の営業所は空設定されるので順番変更
            '旧：営業所⇒作業№1⇒作業№2 
            '新：作業№2⇒作業№1⇒営業所
            '作業№2
            whereStr = .Item("SGY_NO2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" SGY_NO2 = @SGY_NO2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SGY_NO2", whereStr, DBDataType.CHAR))
            End If

            '作業№1
            whereStr = .Item("SGY_NO1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SGY_NO1 = @SGY_NO1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SGY_NO1", whereStr, DBDataType.CHAR))
            End If

            '営業所コード
            whereStr = .Item("EGS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EGS_CD = @EGS_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EGS_CD", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub


#End Region

#Region "新規追加処理"
    ''' <summary>
    ''' 車載受注渡しデータ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>車載受注渡しデータを車載システムや新規追加する</remarks>
    Private Function InsertSyasaiWatashi(ByVal ds As DataSet, ByVal conYuso As SqlConnection) As DataSet

        'DataSetのOUT情報を取得
        Dim outTbl As DataTable = ds.Tables("LMF820OUT")

        'INTableの条件rowの格納
        Me._Row = outTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '新規登録処理　---------------------------------------------------

        'SQL作成
        Me._StrSql.Append(LMF820DAC.SQL_INSERT)                     'SQL構築(データ抽出用INSERT句)
        Call Me.SetSyasaiJutyuSokoParam(Me._Row, Me._SqlPrmList)    'SQL構築(更新値設定)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommandYuso(Me._StrSql.ToString(), conYuso)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF820DAC", "Insert_T_SYASAI_JUTYU_SOKO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(車載受注倉庫テーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyasaiJutyuSokoParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'パラメータ設定(2012.08.14 テーブルレイアウトを手に入れるまで、型は暫定で設定)
            prmList.Add(MyBase.GetSqlParameter("@EGS_CD", .Item("EGS_CD").ToString(), DBDataType.NVARCHAR))                      '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@JYT_NO", .Item("JYT_NO").ToString(), DBDataType.NVARCHAR))                      '受注番号
            prmList.Add(MyBase.GetSqlParameter("@JYT_NO_ED", .Item("JYT_NO_ED").ToString(), DBDataType.NVARCHAR))                '受注番号枝番
            prmList.Add(MyBase.GetSqlParameter("@JYT_DATE", .Item("JYT_DATE").ToString(), DBDataType.NVARCHAR))                  '受注日
            prmList.Add(MyBase.GetSqlParameter("@JYTTNT_CD", .Item("JYTTNT_CD").ToString(), DBDataType.NVARCHAR))                '受注作業者
            prmList.Add(MyBase.GetSqlParameter("@YOT_SGYSTR_DATE", .Item("YOT_SGYSTR_DATE").ToString(), DBDataType.NVARCHAR))    '配送予定日
            prmList.Add(MyBase.GetSqlParameter("@YOT_SGYSTR_TIME", .Item("YOT_SGYSTR_TIME").ToString(), DBDataType.NVARCHAR))    '配送予定時間
            prmList.Add(MyBase.GetSqlParameter("@SURYO", .Item("SURYO").ToString(), DBDataType.NUMERIC))                        '数量
            prmList.Add(MyBase.GetSqlParameter("@SGY_NO1", .Item("SGY_NO1").ToString(), DBDataType.NVARCHAR))                    '作業№1
            prmList.Add(MyBase.GetSqlParameter("@SGY_NO2", .Item("SGY_NO2").ToString(), DBDataType.NVARCHAR))                    '作業№2
            prmList.Add(MyBase.GetSqlParameter("@YOT_JMI_CD1", .Item("YOT_JMI_CD1").ToString(), DBDataType.NVARCHAR))            '乗務員コード
            prmList.Add(MyBase.GetSqlParameter("@NNU_TRSRYK_MEI", .Item("NNU_TRSRYK_MEI").ToString(), DBDataType.NVARCHAR))      '荷主略称
            prmList.Add(MyBase.GetSqlParameter("@OROSI_TRSRYK_MEI", .Item("OROSI_TRSRYK_MEI").ToString(), DBDataType.NVARCHAR))  '積卸略称
            prmList.Add(MyBase.GetSqlParameter("@OROSI_KUIKI_MEI", .Item("OROSI_KUIKI_MEI").ToString(), DBDataType.NVARCHAR))    '積卸区域
            prmList.Add(MyBase.GetSqlParameter("@HINMEIRYK", .Item("HINMEIRYK").ToString(), DBDataType.NVARCHAR))                '品名略称
            prmList.Add(MyBase.GetSqlParameter("@SGY_MEI", .Item("SGY_MEI").ToString(), DBDataType.NVARCHAR))                    '作業名
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_01", .Item("SOKO_UNCHIN_01").ToString(), DBDataType.NUMERIC))      '倉庫_運賃01
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_02", .Item("SOKO_UNCHIN_02").ToString(), DBDataType.NUMERIC))      '倉庫_運賃02
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_03", .Item("SOKO_UNCHIN_03").ToString(), DBDataType.NUMERIC))      '倉庫_運賃03
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_04", .Item("SOKO_UNCHIN_03").ToString(), DBDataType.NUMERIC))      '倉庫_運賃04
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_05", .Item("SOKO_UNCHIN_05").ToString(), DBDataType.NUMERIC))      '倉庫_運賃05
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_06", .Item("SOKO_UNCHIN_06").ToString(), DBDataType.NUMERIC))      '倉庫_運賃06
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_07", .Item("SOKO_UNCHIN_07").ToString(), DBDataType.NUMERIC))      '倉庫_運賃07
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_08", .Item("SOKO_UNCHIN_08").ToString(), DBDataType.NUMERIC))      '倉庫_運賃08
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_09", .Item("SOKO_UNCHIN_09").ToString(), DBDataType.NUMERIC))      '倉庫_運賃09
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_10", .Item("SOKO_UNCHIN_10").ToString(), DBDataType.NUMERIC))      '倉庫_運賃10
            prmList.Add(MyBase.GetSqlParameter("@SOKO_UNCHIN_BIKO", .Item("SOKO_UNCHIN_BIKO").ToString(), DBDataType.NVARCHAR))  '倉庫_運賃備考

            '共通情報
            prmList.Add(MyBase.GetSqlParameter("@PG_ID", MyBase.GetPGID(), DBDataType.NVARCHAR))                                 'PGIDF
            'prmList.Add(MyBase.GetSqlParameter("@TOUROKU_BI", MyBase.GetSystemDate(), DBDataType.NVARCHAR))                    '登録日時(未使用)
            prmList.Add(MyBase.GetSqlParameter("@KOUSIN_BI", 0, DBDataType.NUMERIC))                                            '更新日時
            prmList.Add(MyBase.GetSqlParameter("@SAKUJYO_BI", "", DBDataType.NVARCHAR))                                         '削除日時
            prmList.Add(MyBase.GetSqlParameter("@TOUROKU_UID", MyBase.GetUserID(), DBDataType.NVARCHAR))                         '作成者
            prmList.Add(MyBase.GetSqlParameter("@KOUSIN_UID", "", DBDataType.NVARCHAR))                                          '更新者
            prmList.Add(MyBase.GetSqlParameter("@SAKUJYO_UID", "", DBDataType.NVARCHAR))                                         '削除者
            prmList.Add(MyBase.GetSqlParameter("@SAKUJYO_FLG", "0", DBDataType.NVARCHAR))                                        '削除フラグ

        End With

    End Sub

#End Region

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

#End Region

End Class
