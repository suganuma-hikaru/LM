' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF070DAC : 運賃試算比較
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF070DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

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

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' F_UNCHIN_TRSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT TOP 1001                                                        " & vbNewLine _
                                            & "   UNSOCO.UNSOCO_NM                               AS UNSOCO_NM          " & vbNewLine _
                                            & " , UNSOCO.UNSOCO_BR_NM                            AS UNSOCO_BR_NM       " & vbNewLine _
                                            & " , UNSO.UNSO_ONDO_KB                              AS UNSO_ONDO_KB       " & vbNewLine _
                                            & " , UNSO.UNSO_TEHAI_KB                             AS UNSO_TEHAI_KB      " & vbNewLine _
                                            & " , UNSO.VCLE_KB                                   AS VCLE_KB            " & vbNewLine _
                                            & " , UNSO.DEST_CD                                   AS DEST_CD            " & vbNewLine _
                                            & " , ISNULL(DEST.DEST_NM,DEST2.DEST_NM)             AS DEST_NM            " & vbNewLine _
                                            & " , ISNULL(DEST.JIS,DEST2.JIS)                     AS DEST_JIS_CD        " & vbNewLine _
                                            & " , ISNULL(DEST.AD_1,DEST2.AD_1)                   AS DEST_AD            " & vbNewLine _
                                            & " , UNSO.OUTKA_PLAN_DATE                           AS OUTKA_PLAN_DATE    " & vbNewLine _
                                            & " , UNSO.ARR_PLAN_DATE                             AS ARR_PLAN_DATE      " & vbNewLine _
                                            & " , UNCHIN.SEIQ_WT                                 AS SEIQ_WT            " & vbNewLine _
                                            & " , UNCHIN.SEIQ_KYORI                              AS SEIQ_KYORI         " & vbNewLine _
                                            & " , UNCHIN.SEIQ_TARIFF_CD                          AS SEIQ_TARIFF_CD     " & vbNewLine _
                                            & " , UNCHIN.SEIQ_ETARIFF_CD                         AS SEIQ_ETARIFF_CD    " & vbNewLine _
                                            & " , UNCHIN.DECI_CITY_EXTC                          AS DECI_CITY_EXTC     " & vbNewLine _
                                            & " , UNCHIN.DECI_WINT_EXTC                          AS DECI_WINT_EXTC     " & vbNewLine _
                                            & " , UNCHIN.DECI_RELY_EXTC                          AS DECI_RELY_EXTC     " & vbNewLine _
                                            & " , UNCHIN.DECI_TOLL                               AS DECI_TOLL          " & vbNewLine _
                                            & " , UNCHIN.DECI_INSU                               AS DECI_INSU          " & vbNewLine _
                                            & " , UNCHIN.DECI_UNCHIN                             AS DECI_UNCHIN        " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_UNCHIN    " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_CITY_EXTC " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_WINT_EXTC " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_RELY_EXTC " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_TOLL      " & vbNewLine _
                                            & " , 0                                              AS NEW_DECI_INSU      " & vbNewLine _
                                            & " , UNSO.MOTO_DATA_KB                              AS MOTO_DATA_KB       " & vbNewLine _
                                            & " , KBN.KBN_NM1                                    AS MOTO_DATA_NM       " & vbNewLine _
                                            & " , UNSO.INOUTKA_NO_L                              AS INOUTKA_NO_L       " & vbNewLine _
                                            & " , UNCHIN.SEIQTO_CD                               AS SEIQTO_CD          " & vbNewLine _
                                            & " , SEIQTO.SEIQTO_NM                               AS SEIQ_NM            " & vbNewLine _
                                            & " , UNSO.TARIFF_BUNRUI_KB                          AS TARIFF_BUNRUI_KB   " & vbNewLine _
                                            & " , 0                                              AS UNSO_TTL_QT        " & vbNewLine _
                                            & " , UNCHIN.SEIQ_NG_NB                              AS SEIQ_NG_NB         " & vbNewLine _
                                            & " , UNCHIN.SEIQ_PKG_UT                             AS SEIQ_PKG_UT        " & vbNewLine _
                                            & " , UNCHIN.NRS_BR_CD                               AS NRS_BR_CD          " & vbNewLine _
                                            & " , UNSO.CUST_CD_L                                 AS CUST_CD_L          " & vbNewLine _
                                            & " , UNSO.CUST_CD_M                                 AS CUST_CD_M          " & vbNewLine _
                                            & " , UNCHIN.SIZE_KB                                 AS SIZE_KB            " & vbNewLine _
                                            & " , UNCHIN.SEIQ_DANGER_KB                          AS SEIQ_DANGER_KB     " & vbNewLine _
                                            & " , UNSOCO.UNSOCO_CD                               AS UNSOCO_CD          " & vbNewLine _
                                            & " , ISNULL(KYORI.KYORI,'0')                        AS NEW_SEIQ_KYORI     " & vbNewLine _
                                            & " , SOKO_JIS.WH_NM                                 AS SOKO_NM            " & vbNewLine _
                                            & " , @NEW_SOKO_NM                                   AS NEW_SOKO_NM        " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' F_UNCHIN_TRSデータ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA As String = "FROM                                                                                                     " & vbNewLine _
                                          & "    $LM_TRN$..F_UNCHIN_TRS UNCHIN                                                                        " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    $LM_TRN$..F_UNSO_L UNSO                                                                              " & vbNewLine _
                                          & " ON UNCHIN.NRS_BR_CD = UNSO.NRS_BR_CD                                                                    " & vbNewLine _
                                          & "AND UNCHIN.UNSO_NO_L = UNSO.UNSO_NO_L                                                                    " & vbNewLine _
                                          & "AND UNSO.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                          & "LEFT JOIN(                                                                                               " & vbNewLine _
                                          & "          SELECT                                                                                         " & vbNewLine _
                                          & "            UNSO.NRS_BR_CD                   AS NRS_BR_CD                                                " & vbNewLine _
                                          & "          , UNSO.UNSO_NO_L                   AS UNSO_NO_L                                                " & vbNewLine _
                                          & "          , ISNULL(SOKO.JIS_CD,SOKO2.JIS_CD) AS JIS_CD                                                   " & vbNewLine _
                                          & "          , ISNULL(SOKO.WH_NM,SOKO2.WH_NM)   AS WH_NM                                                    " & vbNewLine _
                                          & "          FROM                                                                                           " & vbNewLine _
                                          & "              $LM_TRN$..F_UNSO_L UNSO                                                                    " & vbNewLine _
                                          & "          LEFT JOIN                                                                                      " & vbNewLine _
                                          & "              $LM_TRN$..B_INKA_L INKA_L                                                                  " & vbNewLine _
                                          & "           ON UNSO.NRS_BR_CD = INKA_L.NRS_BR_CD                                                          " & vbNewLine _
                                          & "          AND UNSO.INOUTKA_NO_L = INKA_L.INKA_NO_L                                                       " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_L = INKA_L.CUST_CD_L                                                          " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_M = INKA_L.CUST_CD_M                                                          " & vbNewLine _
                                          & "          AND INKA_L.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
                                          & "          LEFT JOIN                                                                                      " & vbNewLine _
                                          & "              $LM_TRN$..C_OUTKA_L OUTKA_L                                                                " & vbNewLine _
                                          & "           ON UNSO.NRS_BR_CD = OUTKA_L.NRS_BR_CD                                                         " & vbNewLine _
                                          & "          AND UNSO.INOUTKA_NO_L = OUTKA_L.OUTKA_NO_L                                                     " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_L = OUTKA_L.CUST_CD_L                                                         " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_M = OUTKA_L.CUST_CD_M                                                         " & vbNewLine _
                                          & "          AND OUTKA_L.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                          & "          LEFT JOIN                                                                                      " & vbNewLine _
                                          & "              $LM_MST$..M_SOKO SOKO                                                                      " & vbNewLine _
                                          & "           ON INKA_L.NRS_BR_CD = SOKO.NRS_BR_CD                                                          " & vbNewLine _
                                          & "          AND INKA_L.WH_CD = SOKO.WH_CD                                                                  " & vbNewLine _
                                          & "          AND SOKO.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                          & "          LEFT JOIN                                                                                      " & vbNewLine _
                                          & "              $LM_MST$..M_SOKO SOKO2                                                                     " & vbNewLine _
                                          & "           ON OUTKA_L.NRS_BR_CD = SOKO2.NRS_BR_CD                                                        " & vbNewLine _
                                          & "          AND OUTKA_L.WH_CD = SOKO2.WH_CD                                                                " & vbNewLine _
                                          & "          AND SOKO2.SYS_DEL_FLG = '0'                                                                    " & vbNewLine _
                                          & "          WHERE                                                                                          " & vbNewLine _
                                          & "              UNSO.NRS_BR_CD = @NRS_BR_CD                                                                " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_L = @CUST_CD_L                                                                " & vbNewLine _
                                          & "          AND UNSO.CUST_CD_M  = @CUST_CD_M                                                               " & vbNewLine _
                                          & "          AND UNSO.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE                                                   " & vbNewLine _
                                          & "          AND UNSO.OUTKA_PLAN_DATE <= @OUTKA_PLAN_TO                                                     " & vbNewLine _
                                          & "          AND UNSO.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                          & "          ) SOKO_JIS                                                                                     " & vbNewLine _
                                          & " ON UNSO.NRS_BR_CD = SOKO_JIS.NRS_BR_CD                                                                  " & vbNewLine _
                                          & "AND UNSO.UNSO_NO_L = SOKO_JIS.UNSO_NO_L                                                                  " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "   $LM_MST$..M_UNSOCO UNSOCO                                                                             " & vbNewLine _
                                          & " ON UNSO.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                    " & vbNewLine _
                                          & "AND UNSO.UNSO_CD = UNSOCO.UNSOCO_CD                                                                      " & vbNewLine _
                                          & "AND UNSO.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                " & vbNewLine _
                                          & "AND UNSOCO.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    $LM_MST$..M_DEST DEST                                                                                " & vbNewLine _
                                          & " ON UNSO.NRS_BR_CD = DEST.NRS_BR_CD                                                                      " & vbNewLine _
                                          & "AND UNSO.CUST_CD_L = DEST.CUST_CD_L                                                                      " & vbNewLine _
                                          & "AND UNSO.DEST_CD = DEST.DEST_CD                                                                          " & vbNewLine _
                                          & "AND DEST.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    $LM_MST$..M_DEST DEST2                                                                               " & vbNewLine _
                                          & " ON UNSO.NRS_BR_CD = DEST2.NRS_BR_CD                                                                     " & vbNewLine _
                                          & "AND 'ZZZZZ' = DEST2.CUST_CD_L                                                                            " & vbNewLine _
                                          & "AND UNSO.DEST_CD = DEST2.DEST_CD                                                                         " & vbNewLine _
                                          & "AND DEST2.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    $LM_MST$..Z_KBN KBN                                                                                  " & vbNewLine _
                                          & " ON UNSO.MOTO_DATA_KB = KBN_CD                                                                           " & vbNewLine _
                                          & "AND KBN.KBN_GROUP_CD = 'M004'                                                                            " & vbNewLine _
                                          & "AND KBN.SYS_DEL_FLG = '0'                                                                                " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    $LM_MST$..M_SEIQTO SEIQTO                                                                            " & vbNewLine _
                                          & " ON UNCHIN.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                  " & vbNewLine _
                                          & "AND UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD                                                                  " & vbNewLine _
                                          & "AND SEIQTO.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    LM_MST..M_CUST CUST                                                                                  " & vbNewLine _
                                          & " ON UNCHIN.NRS_BR_CD = CUST.NRS_BR_CD                                                                    " & vbNewLine _
                                          & "AND UNCHIN.CUST_CD_L = CUST.CUST_CD_L                                                                    " & vbNewLine _
                                          & "AND UNCHIN.CUST_CD_M = CUST.CUST_CD_M                                                                    " & vbNewLine _
                                          & "AND UNCHIN.CUST_CD_S = CUST.CUST_CD_S                                                                    " & vbNewLine _
                                          & "AND UNCHIN.CUST_CD_SS = CUST.CUST_CD_SS                                                                  " & vbNewLine _
                                          & "AND CUST.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                          & "LEFT JOIN                                                                                                " & vbNewLine _
                                          & "    LM_MST..M_KYORI KYORI                                                                                " & vbNewLine _
                                          & " ON KYORI.NRS_BR_CD = CASE WHEN @NEW_NRS_BR_CD = '' THEN                                                 " & vbNewLine _
                                          & "                                UNCHIN.NRS_BR_CD                                                         " & vbNewLine _
                                          & "                      ELSE                                                                               " & vbNewLine _
                                          & "                                @NEW_NRS_BR_CD                                                           " & vbNewLine _
                                          & "                      END                                                                                " & vbNewLine _
                                          & "AND KYORI.KYORI_CD = CASE WHEN @NEW_KYORI_CD = '' THEN                                                   " & vbNewLine _
                                          & "                               CUST.BETU_KYORI_CD                                                        " & vbNewLine _
                                          & "                     ELSE                                                                                " & vbNewLine _
                                          & "                               @NEW_KYORI_CD                                                             " & vbNewLine _
                                          & "                     END                                                                                 " & vbNewLine _
                                          & "AND KYORI.ORIG_JIS_CD = CASE WHEN @NEW_ORIG_JIS = '' THEN                                                " & vbNewLine _
                                          & "                                  SOKO_JIS.JIS_CD                                                        " & vbNewLine _
                                          & "                             WHEN ISNULL(DEST.JIS,DEST2.JIS) > @NEW_ORIG_JIS THEN                        " & vbNewLine _
                                          & "                                  @NEW_ORIG_JIS                                                          " & vbNewLine _
                                          & "                        ELSE                                                                             " & vbNewLine _
                                          & "                                  ISNULL(DEST.JIS,DEST2.JIS)                                             " & vbNewLine _
                                          & "                        END                                                                              " & vbNewLine _
                                          & "AND KYORI.DEST_JIS_CD = CASE WHEN @NEW_KYORI_CD = '' AND @NEW_ORIG_JIS = '' THEN                         " & vbNewLine _
                                          & "                                  ''                                                                     " & vbNewLine _
                                          & "                             WHEN @NEW_ORIG_JIS = '' OR ISNULL(DEST.JIS,DEST2.JIS) > @NEW_ORIG_JIS THEN  " & vbNewLine _
                                          & "                                  ISNULL(DEST.JIS,DEST2.JIS)                                             " & vbNewLine _
                                          & "                        ELSE                                                                             " & vbNewLine _
                                          & "                                  @NEW_ORIG_JIS                                                          " & vbNewLine _
                                          & "                        END                                                                              " & vbNewLine _
                                          & "AND KYORI.SYS_DEL_FLG = '0'                                                                              " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY               " & vbNewLine _
                                         & "  UNSO.OUTKA_PLAN_DATE " & vbNewLine _
                                         & ", UNSO.MOTO_DATA_KB    " & vbNewLine _
                                         & ", UNSO.INOUTKA_NO_L    " & vbNewLine

#End Region

#End Region

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF070DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF070DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMF070DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF070DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("NEW_DECI_UNCHIN", "NEW_DECI_UNCHIN")
        map.Add("NEW_DECI_CITY_EXTC", "NEW_DECI_CITY_EXTC")
        map.Add("NEW_DECI_WINT_EXTC", "NEW_DECI_WINT_EXTC")
        map.Add("NEW_DECI_RELY_EXTC", "NEW_DECI_RELY_EXTC")
        map.Add("NEW_DECI_TOLL", "NEW_DECI_TOLL")
        map.Add("NEW_DECI_INSU", "NEW_DECI_INSU")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("MOTO_DATA_NM", "MOTO_DATA_NM")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQ_NM", "SEIQ_NM")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("NEW_SEIQ_KYORI", "NEW_SEIQ_KYORI")
        map.Add("SOKO_NM", "SOKO_NM")
        map.Add("NEW_SOKO_NM", "NEW_SOKO_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF070OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.NRS_BR_CD = @NRS_BR_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.CUST_CD_L = @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.CUST_CD_M  = @CUST_CD_M   ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OUTKA_PLAN_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.OUTKA_PLAN_DATE <= @OUTKA_PLAN_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQ_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.SEIQ_TARIFF_CD LIKE @SEIQ_TARIFF_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SEIQ_ETARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.SEIQ_ETARIFF_CD LIKE @SEIQ_ETARIFF_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_CD LIKE @UNSOCO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If


            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.SEIQTO_CD LIKE @SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SPREAD_DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @SPREAD_DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPREAD_DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If


            whereStr = .Item("SPREAD_DEST_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.AD_1 LIKE @SPREAD_DEST_JIS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPREAD_DEST_JIS_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SPREAD_INOUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.INOUTKA_NO_L LIKE @SPREAD_INOUTKA_NO_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPREAD_INOUTKA_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            whereStr = .Item("MOTO_DATA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO.MOTO_DATA_KB = @MOTO_DATA_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_NM + '　' + UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_NRS_BR_CD", .Item("NEW_NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_KYORI_CD", .Item("NEW_KYORI_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_ORIG_JIS", .Item("NEW_ORIG_JIS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SOKO_NM", .Item("NEW_SOKO_NM").ToString(), DBDataType.NVARCHAR))

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

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
