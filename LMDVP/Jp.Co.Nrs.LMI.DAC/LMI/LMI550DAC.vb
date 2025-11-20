'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI550DAC : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMI550DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#End Region 'Const

#Region "SQL"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 検索処理：取得：SELECT句（件数取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SEARCH As String = "" _
            & "SELECT                   " & vbNewLine _
            & "  COUNT(*) AS SELECT_CNT " & vbNewLine _
            & ""

    Private Const SQL_SELECT_COUNT_SEARCH_2 As String = "" _
            & "SELECT                 " & vbNewLine _
            & "  ZAI_TSMC.TSMC_REC_NO " & vbNewLine _
            & ""

    ''' <summary>
    ''' 検索処理：取得：SELECT句（データ取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SEARCH As String = "" _
            & "SELECT                                                                       " & vbNewLine _
            & "      ZAI_TSMC.CUST_GOODS_CD                                                 " & vbNewLine _
            & "     ,ZAI_TSMC.GOODS_NM                                                      " & vbNewLine _
            & "     ,ZAI_TSMC.LOT_NO                                                        " & vbNewLine _
            & "     ,T035.KBN_NM1                               AS STATUS_NM                " & vbNewLine _
            & "     ,ZAI_TSMC.INKA_DATE                                                     " & vbNewLine _
            & "     ,ZAI_TSMC.OUTKA_PLAN_DATE                                               " & vbNewLine _
            & "     ,ZAI_TSMC.RTN_INKA_DATE                                                 " & vbNewLine _
            & "     ,ZAI_TSMC.RTN_OUTKA_PLAN_DATE                                           " & vbNewLine _
            & "     ,ZAI_TSMC.LAST_INV_DATE                                                 " & vbNewLine _
            & "     ,ZAI_TSMC.LVL1_CHECK                                                    " & vbNewLine _
            & "     ,ZAI_TSMC.LVL2_CHECK                                                    " & vbNewLine _
            & "     ,T037.KBN_NM1                               AS STOCK_TYPE_NM            " & vbNewLine _
            & "     ,ZAI_TSMC.SUPPLY_CD                                                     " & vbNewLine _
            & "     ,ZAI_TSMC.TSMC_REC_NO                                                   " & vbNewLine _
            & "     ,ZAI_TSMC.PLT_NO                                                        " & vbNewLine _
            & "     ,ZAI_TSMC.LV2_SERIAL_NO                                                 " & vbNewLine _
            & "     ,A008.KBN_NM1                               AS UP_FLG_NM                " & vbNewLine _
            & "     ,U009.KBN_NM1                               AS RETURN_FLAG_NM           " & vbNewLine _
            & "     ,ZAI_TSMC.ASN_NO                                                        " & vbNewLine _
            & "     ,KEIKA_DAYS.COUNT                           AS KEIKA_DAYS               " & vbNewLine _
            & "     ,ZAI_TSMC.LT_DATE                                                       " & vbNewLine _
            & "     ,ZAI_TSMC.CYLINDER_NO                                                   " & vbNewLine _
            & "     ,ZAI_TSMC.LAST_CLC_DATE                                                 " & vbNewLine _
            & "     ,ZAI_TSMC.STATUS                                                        " & vbNewLine _
            & "     ,ZAI_TSMC.STOCK_TYPE                                                    " & vbNewLine _
            & "     ,ZAI_TSMC.UP_FLG                                                        " & vbNewLine _
            & "     ,ZAI_TSMC.RETURN_FLAG                                                   " & vbNewLine _
            & "     ,ZAI_TSMC.LVL1_UT                                                       " & vbNewLine _
            & "     ,ZAI_TSMC.GRLVL1_PPNID                                                  " & vbNewLine _
            & "     ,ZAI_TSMC.DEPLT_NO                                                      " & vbNewLine _
            & "     ,ZAI_TSMC.NRS_WH_CD                                                     " & vbNewLine _
            & "     ,SOKO.WH_NM                                                             " & vbNewLine _
            & "     ,CASE ISNULL(SENDINEDI_TSMC.JISSEKI_SHORI_FLG, 'X')                     " & vbNewLine _
            & "        WHEN 'X' THEN '未登録'                                               " & vbNewLine _
            & "        WHEN '0' THEN '送信対象外'                                           " & vbNewLine _
            & "        WHEN '1' THEN '送信対象'                                             " & vbNewLine _
            & "        WHEN '2' THEN '実績送信エラー'                                       " & vbNewLine _
            & "        WHEN '3' THEN '実績送信済'                                           " & vbNewLine _
            & "        ELSE          ''                                                     " & vbNewLine _
            & "      END AS JISSEKI_SHORI_FLG_IN_NM                                         " & vbNewLine _
            & "     ,CASE ISNULL(SENDOUTEDI_TSMC.JISSEKI_SHORI_FLG, 'X')                    " & vbNewLine _
            & "        WHEN 'X' THEN '未登録'                                               " & vbNewLine _
            & "        WHEN '0' THEN '送信対象外'                                           " & vbNewLine _
            & "        WHEN '1' THEN '送信対象'                                             " & vbNewLine _
            & "        WHEN '2' THEN '実績送信エラー'                                       " & vbNewLine _
            & "        WHEN '3' THEN '実績送信済'                                           " & vbNewLine _
            & "        ELSE          ''                                                     " & vbNewLine _
            & "      END AS JISSEKI_SHORI_FLG_OUT_NM                                        " & vbNewLine _
            & "     ,ISNULL(SENDINEDI_TSMC.JISSEKI_SHORI_FLG, 'X') AS JISSEKI_SHORI_FLG_IN  " & vbNewLine _
            & "     ,ISNULL(SENDOUTEDI_TSMC.JISSEKI_SHORI_FLG, 'X') AS JISSEKI_SHORI_FLG_OUT" & vbNewLine _
            & "     ,ZAI_TRS.TOU_NO                                                         " & vbNewLine _
            & "     ,ZAI_TRS.SITU_NO                                                        " & vbNewLine _
            & "     ,TOU_SITU.TOU_SITU_NM                                                   " & vbNewLine _
            & "     ,ZAI_TRS.ZONE_CD                                                        " & vbNewLine _
            & "     ,ZONE.ZONE_NM                                                           " & vbNewLine _
            & "     ,ZAI_TRS.LOCA                                                           " & vbNewLine _
            & "     ,ZAI_TSMC.SYS_UPD_DATE                                                  " & vbNewLine _
            & "     ,ZAI_TSMC.SYS_UPD_TIME                                                  " & vbNewLine _
            & ""

    ''' <summary>
    ''' 検索処理：取得：FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEARCH As String = "" _
            & "FROM                                                                                                     " & vbNewLine _
            & "    $LM_TRN$..D_ZAI_TSMC ZAI_TSMC                                                                        " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    (                                                                                                    " & vbNewLine _
            & "         SELECT                                                                                          " & vbNewLine _
            & "               ZAI_TSMC.NRS_BR_CD                                                                        " & vbNewLine _
            & "              ,ZAI_TSMC.TSMC_REC_NO                                                                      " & vbNewLine _
            & "              ,CASE                                                                                      " & vbNewLine _
            & "                 WHEN ZAI_TSMC.RETURN_FLAG = '00'                                                        " & vbNewLine _
            & "                 -- 個品                                                                                 " & vbNewLine _
            & "                 THEN DATEDIFF( day                                                                      " & vbNewLine _
            & "                               ,CASE                                                                     " & vbNewLine _
            & "                                  WHEN LEN(RTRIM(ZAI_TSMC.INKA_DATE)) = 8                                " & vbNewLine _
            & "                                   AND ISNUMERIC(ZAI_TSMC.INKA_DATE) = 1                                 " & vbNewLine _
            & "                                  THEN CONVERT(DATETIME, ZAI_TSMC.INKA_DATE)                             " & vbNewLine _
            & "                                  ELSE GETDATE()                                                         " & vbNewLine _
            & "                                END                                                                      " & vbNewLine _
            & "                               ,CASE                                                                     " & vbNewLine _
            & "                                  WHEN LEN(RTRIM(ZAI_TSMC.OUTKA_PLAN_DATE)) = 8                          " & vbNewLine _
            & "                                   AND ISNUMERIC(ZAI_TSMC.OUTKA_PLAN_DATE) = 1                           " & vbNewLine _
            & "                                   AND ZAI_TSMC.OUTKA_PLAN_DATE < FORMAT(GETDATE(),'yyyyMMdd')           " & vbNewLine _
            & "                                  THEN CONVERT(DATETIME, ZAI_TSMC.OUTKA_PLAN_DATE)                       " & vbNewLine _
            & "                                  ELSE GETDATE()                                                         " & vbNewLine _
            & "                                END                                                                      " & vbNewLine _
            & "                              )                                                                          " & vbNewLine _
            & "                 -- 通い容器                                                                             " & vbNewLine _
            & "                 ELSE DATEDIFF( day                                                                      " & vbNewLine _
            & "                               ,CASE                                                                     " & vbNewLine _
            & "                                  WHEN LEN(RTRIM(ZAI_TSMC.INKA_DATE)) = 8                                " & vbNewLine _
            & "                                   AND ISNUMERIC(ZAI_TSMC.INKA_DATE) = 1                                 " & vbNewLine _
            & "                                  THEN CONVERT(DATETIME, ZAI_TSMC.INKA_DATE)                             " & vbNewLine _
            & "                                  ELSE GETDATE()                                                         " & vbNewLine _
            & "                                END                                                                      " & vbNewLine _
            & "                               ,CASE                                                                     " & vbNewLine _
            & "                                  WHEN LEN(RTRIM(ZAI_TSMC.RTN_OUTKA_PLAN_DATE)) = 8                      " & vbNewLine _
            & "                                   AND ISNUMERIC(ZAI_TSMC.RTN_OUTKA_PLAN_DATE) = 1                       " & vbNewLine _
            & "                                   AND ZAI_TSMC.RTN_OUTKA_PLAN_DATE < FORMAT(GETDATE(),'yyyyMMdd')       " & vbNewLine _
            & "                                  THEN CONVERT(DATETIME, ZAI_TSMC.RTN_OUTKA_PLAN_DATE)                   " & vbNewLine _
            & "                                  ELSE GETDATE()                                                         " & vbNewLine _
            & "                                END                                                                      " & vbNewLine _
            & "                              )                                                                          " & vbNewLine _
            & "               END AS COUNT                                                                              " & vbNewLine _
            & "         FROM                                                                                            " & vbNewLine _
            & "             $LM_TRN$..D_ZAI_TSMC ZAI_TSMC                                                               " & vbNewLine _
            & "         WHERE ZAI_TSMC.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
            & "    ) KEIKA_DAYS                                                                                         " & vbNewLine _
            & "        ON  KEIKA_DAYS.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                                    " & vbNewLine _
            & "        AND KEIKA_DAYS.TSMC_REC_NO = ZAI_TSMC.TSMC_REC_NO                                                " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..M_SOKO SOKO                                                                                " & vbNewLine _
            & "        ON  SOKO.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                                          " & vbNewLine _
            & "        AND SOKO.WH_CD = ZAI_TSMC.NRS_WH_CD                                                              " & vbNewLine _
            & "        AND SOKO.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..Z_KBN T035                                                                                 " & vbNewLine _
            & "        ON  T035.KBN_GROUP_CD = 'T035'                                                                   " & vbNewLine _
            & "        AND T035.KBN_CD = ZAI_TSMC.STATUS                                                                " & vbNewLine _
            & "        AND T035.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..Z_KBN T037                                                                                 " & vbNewLine _
            & "        ON  T037.KBN_GROUP_CD = 'T037'                                                                   " & vbNewLine _
            & "        AND T037.KBN_CD = ZAI_TSMC.STOCK_TYPE                                                            " & vbNewLine _
            & "        AND T037.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..Z_KBN A008                                                                                 " & vbNewLine _
            & "        ON  A008.KBN_GROUP_CD = 'A008'                                                                   " & vbNewLine _
            & "        AND A008.KBN_CD = ZAI_TSMC.UP_FLG                                                                " & vbNewLine _
            & "        AND A008.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..Z_KBN U009                                                                                 " & vbNewLine _
            & "        ON  U009.KBN_GROUP_CD = 'U009'                                                                   " & vbNewLine _
            & "        AND U009.KBN_CD = ZAI_TSMC.RETURN_FLAG                                                           " & vbNewLine _
            & "        AND U009.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_TRN$..H_SENDINEDI_TSMC SENDINEDI_TSMC                                                            " & vbNewLine _
            & "        ON  SENDINEDI_TSMC.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                                " & vbNewLine _
            & "        AND SENDINEDI_TSMC.INKA_CTL_NO_L = ZAI_TSMC.INKA_CTL_NO_L                                        " & vbNewLine _
            & "        AND SENDINEDI_TSMC.INKA_CTL_NO_M = ZAI_TSMC.INKA_CTL_NO_M                                        " & vbNewLine _
            & "        AND SENDINEDI_TSMC.GRLVL1_PPNID =                                                                " & vbNewLine _
            & "                CASE WHEN SENDINEDI_TSMC.GRLVL1_PPNID LIKE '%@%' AND LEN(ZAI_TSMC.GRLVL1_PPNID) >= 3     " & vbNewLine _
            & "                    THEN RIGHT(ZAI_TSMC.GRLVL1_PPNID, LEN(ZAI_TSMC.GRLVL1_PPNID) - 3)                    " & vbNewLine _
            & "                    ELSE ZAI_TSMC.GRLVL1_PPNID                                                           " & vbNewLine _
            & "                END                                                                                      " & vbNewLine _
            & "        AND SENDINEDI_TSMC.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_TRN$..H_SENDOUTEDI_TSMC SENDOUTEDI_TSMC                                                          " & vbNewLine _
            & "        ON  SENDOUTEDI_TSMC.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                               " & vbNewLine _
            & "        AND SENDOUTEDI_TSMC.OUTKA_CTL_NO = ZAI_TSMC.OUTKA_NO_L                                           " & vbNewLine _
            & "        AND SENDOUTEDI_TSMC.OUTKA_CTL_NO_CHU = ZAI_TSMC.OUTKA_NO_M                                       " & vbNewLine _
            & "        AND SENDOUTEDI_TSMC.LVL1_PPNID =                                                                 " & vbNewLine _
            & "                CASE WHEN SENDOUTEDI_TSMC.LVL1_PPNID LIKE '%@%' AND LEN(ZAI_TSMC.GRLVL1_PPNID) >= 3      " & vbNewLine _
            & "                    THEN RIGHT(ZAI_TSMC.GRLVL1_PPNID, LEN(ZAI_TSMC.GRLVL1_PPNID) - 3)                    " & vbNewLine _
            & "                    ELSE ZAI_TSMC.GRLVL1_PPNID                                                           " & vbNewLine _
            & "                END                                                                                      " & vbNewLine _
            & "        AND SENDOUTEDI_TSMC.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
            & ""
    Private Const SQL_SELECT_FROM_SEARCH_2 As String = "" _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_TRN$..D_ZAI_TRS ZAI_TRS                                                                          " & vbNewLine _
            & "        ON  ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                                                               " & vbNewLine _
            & "        AND ZAI_TRS.ZAI_REC_NO = ISNULL((                                                                " & vbNewLine _
            & "                SELECT TOP 1                                                                             " & vbNewLine _
            & "                    ZAI_TRS_SUBQ.ZAI_REC_NO                                                              " & vbNewLine _
            & "                FROM                                                                                     " & vbNewLine _
            & "                    $LM_TRN$..D_ZAI_TRS ZAI_TRS_SUBQ                                                     " & vbNewLine _
            & "                LEFT JOIN                                                                                " & vbNewLine _
            & "                    $LM_TRN$..D_IDO_TRS IDO_TRS_SUBQ                                                     " & vbNewLine _
            & "                        ON  IDO_TRS_SUBQ.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine _
            & "                        AND IDO_TRS_SUBQ.N_ZAI_REC_NO = ZAI_TRS_SUBQ.ZAI_REC_NO                          " & vbNewLine _
            & "                        AND IDO_TRS_SUBQ.SYS_DEL_FLG = '0'                                               " & vbNewLine _
            & "                WHERE                                                                                    " & vbNewLine _
            & "                    ZAI_TRS_SUBQ.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
            & "                AND ZAI_TRS_SUBQ.INKA_NO_L = ZAI_TSMC.INKA_CTL_NO_L                                      " & vbNewLine _
            & "                AND ZAI_TRS_SUBQ.INKA_NO_M = ZAI_TSMC.INKA_CTL_NO_M                                      " & vbNewLine _
            & "                AND ZAI_TRS_SUBQ.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
            & "                ORDER BY                                                                                 " & vbNewLine _
            & "                      ISNULL(IDO_TRS_SUBQ.IDO_DATE, ZAI_TRS_SUBQ.INKO_DATE) DESC                         " & vbNewLine _
            & "                    , ZAI_TRS_SUBQ.ZAI_REC_NO DESC                                                       " & vbNewLine _
            & "            ), ZAI_TRS.ZAI_REC_NO + '_X')                                                                " & vbNewLine _
            & "        AND ZAI_TRS.INKA_NO_L = ZAI_TSMC.INKA_CTL_NO_L                                                   " & vbNewLine _
            & "        AND ZAI_TRS.INKA_NO_M = ZAI_TSMC.INKA_CTL_NO_M                                                   " & vbNewLine _
            & "        AND ZAI_TRS.SYS_DEL_FLG = '0'                                                                    " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..M_TOU_SITU TOU_SITU                                                                        " & vbNewLine _
            & "        ON  TOU_SITU.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                                      " & vbNewLine _
            & "        AND TOU_SITU.WH_CD = ZAI_TSMC.NRS_WH_CD                                                          " & vbNewLine _
            & "        AND TOU_SITU.TOU_NO = ZAI_TRS.TOU_NO                                                             " & vbNewLine _
            & "        AND TOU_SITU.SITU_NO = ZAI_TRS.SITU_NO                                                           " & vbNewLine _
            & "        AND TOU_SITU.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
            & "LEFT JOIN                                                                                                " & vbNewLine _
            & "    $LM_MST$..M_ZONE ZONE                                                                                " & vbNewLine _
            & "        ON  ZONE.NRS_BR_CD = ZAI_TSMC.NRS_BR_CD                                                          " & vbNewLine _
            & "        AND ZONE.WH_CD = ZAI_TSMC.NRS_WH_CD                                                              " & vbNewLine _
            & "        AND ZONE.TOU_NO = ZAI_TRS.TOU_NO                                                                 " & vbNewLine _
            & "        AND ZONE.SITU_NO = ZAI_TRS.SITU_NO                                                               " & vbNewLine _
            & "        AND ZONE.ZONE_CD = ZAI_TRS.ZONE_CD                                                               " & vbNewLine _
            & "        AND ZONE.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
            & ""

#End Region ' "検索処理 SQL"

#Region "更新処理 SQL"

    ''' <summary>
    ''' 更新処理(検査状態) SQL
    ''' </summary>
    Private Const SQL_UPDATE_ZAI_TSMC As String = "" _
        & "UPDATE $LM_TRN$..D_ZAI_TSMC            " & vbNewLine _
        & "SET                                    " & vbNewLine _
        & "      STOCK_TYPE = @STOCK_TYPE         " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE     " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME     " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID     " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER     " & vbNewLine _
        & "WHERE                                  " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
        & "AND TSMC_REC_NO = @TSMC_REC_NO         " & vbNewLine _
        & "AND SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
        & "AND SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine _
        & ""

#End Region ' "更新処理 SQL"

#End Region 'SQL

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region 'Field

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI550IN")

        ' INTable の条件 row の格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        If IsExistsSearchConditionTouSituZoneLoca() Then
            ' D_ZAI_TRS および 棟室ゾーン名称に関わる検索条件ありの場合
            ' COUNT(*) での件数取得よりはましなレスポンスが得られる列 Select で件数を取得する。
            Me._StrSql.Append(LMI550DAC.SQL_SELECT_COUNT_SEARCH_2 & LMI550DAC.SQL_SELECT_FROM_SEARCH & LMI550DAC.SQL_SELECT_FROM_SEARCH_2)
        Else
            Me._StrSql.Append(LMI550DAC.SQL_SELECT_COUNT_SEARCH & LMI550DAC.SQL_SELECT_FROM_SEARCH)
        End If

        ' 条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        Dim selectCnt As Integer = 0

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            cmd.CommandTimeout = 200

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If IsExistsSearchConditionTouSituZoneLoca() Then
                    ' 取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    map.Add("TSMC_REC_NO", "TSMC_REC_NO")

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI550OUT")

                    ' 取得件数の設定
                    selectCnt = ds.Tables("LMI550OUT").Rows.Count()

                    ' DataSet 初期化(データを戻すことが目的ではないため)
                    Call ds.Tables("LMI550OUT").Clear()
                Else
                    ' 取得件数の設定
                    If reader.Read() Then
                        selectCnt = Convert.ToInt32(reader("SELECT_CNT"))
                    End If
                End If

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI550IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI550DAC.SQL_SELECT_DATA_SEARCH & LMI550DAC.SQL_SELECT_FROM_SEARCH & LMI550DAC.SQL_SELECT_FROM_SEARCH_2)

        ' 条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        ' 並びの設定
        Call Me.SearchSelectSetOrder()

        Dim selectCnt As Integer = 0

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            cmd.CommandTimeout = 200

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データの格納先をマッピング
                Dim map As Hashtable = New Hashtable()
                map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
                map.Add("GOODS_NM", "GOODS_NM")
                map.Add("LOT_NO", "LOT_NO")
                map.Add("STATUS_NM", "STATUS_NM")
                map.Add("INKA_DATE", "INKA_DATE")
                map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
                map.Add("RTN_INKA_DATE", "RTN_INKA_DATE")
                map.Add("RTN_OUTKA_PLAN_DATE", "RTN_OUTKA_PLAN_DATE")
                map.Add("LAST_INV_DATE", "LAST_INV_DATE")
                map.Add("LVL1_CHECK", "LVL1_CHECK")
                map.Add("LVL2_CHECK", "LVL2_CHECK")
                map.Add("STOCK_TYPE_NM", "STOCK_TYPE_NM")
                map.Add("SUPPLY_CD", "SUPPLY_CD")
                map.Add("TSMC_REC_NO", "TSMC_REC_NO")
                map.Add("PLT_NO", "PLT_NO")
                map.Add("LV2_SERIAL_NO", "LV2_SERIAL_NO")
                map.Add("UP_FLG_NM", "UP_FLG_NM")
                map.Add("RETURN_FLAG_NM", "RETURN_FLAG_NM")
                map.Add("ASN_NO", "ASN_NO")
                map.Add("KEIKA_DAYS", "KEIKA_DAYS")
                map.Add("LT_DATE", "LT_DATE")
                map.Add("CYLINDER_NO", "CYLINDER_NO")
                map.Add("LAST_CLC_DATE", "LAST_CLC_DATE")
                map.Add("STATUS", "STATUS")
                map.Add("STOCK_TYPE", "STOCK_TYPE")
                map.Add("UP_FLG", "UP_FLG")
                map.Add("RETURN_FLAG", "RETURN_FLAG")
                map.Add("LVL1_UT", "LVL1_UT")
                map.Add("GRLVL1_PPNID", "GRLVL1_PPNID")
                map.Add("DEPLT_NO", "DEPLT_NO")
                map.Add("NRS_WH_CD", "NRS_WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("JISSEKI_SHORI_FLG_IN_NM", "JISSEKI_SHORI_FLG_IN_NM")
                map.Add("JISSEKI_SHORI_FLG_OUT_NM", "JISSEKI_SHORI_FLG_OUT_NM")
                map.Add("JISSEKI_SHORI_FLG_IN", "JISSEKI_SHORI_FLG_IN")
                map.Add("JISSEKI_SHORI_FLG_OUT", "JISSEKI_SHORI_FLG_OUT")
                map.Add("TOU_NO", "TOU_NO")
                map.Add("SITU_NO", "SITU_NO")
                map.Add("TOU_SITU_NM", "TOU_SITU_NM")
                map.Add("ZONE_CD", "ZONE_CD")
                map.Add("ZONE_NM", "ZONE_NM")
                map.Add("LOCA", "LOCA")
                map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
                map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI550OUT")

                ' 取得件数の設定
                selectCnt = ds.Tables("LMI550OUT").Rows.Count()

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 検索条件有無判定(棟・室・ZONE・ロケーション)
    ''' </summary>
    ''' <returns></returns>
    Private Function IsExistsSearchConditionTouSituZoneLoca() As Boolean

        Dim whereStr As String = String.Empty

        With Me._Row

            ' 棟番号
            whereStr = .Item("TOU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

            ' 室番号
            whereStr = .Item("SITU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

            ' 棟室名
            whereStr = .Item("TOU_SITU_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

            ' ZONEコード
            whereStr = .Item("ZONE_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

            ' ZONE名称
            whereStr = .Item("ZONE_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

            ' ロケーション
            whereStr = .Item("LOCA").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Return True
            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 検索処理：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetCondition()

        Dim whereStr As String = String.Empty

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 条件
        Me._StrSql.Append("WHERE " & vbNewLine)
        With Me._Row
            ' 固定条件
            Me._StrSql.Append("    ZAI_TSMC.SYS_DEL_FLG = '0' " & vbNewLine)

            ' 営業所コード
            ' (固定条件)
            whereStr = .Item("NRS_BR_CD").ToString()
            'If Not String.IsNullOrEmpty(whereStr) Then
            Me._StrSql.Append("AND ZAI_TSMC.NRS_BR_CD = @NRS_BR_CD " & vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.VARCHAR))
            'End If

            ' 倉庫
            whereStr = .Item("WH_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.NRS_WH_CD = @WH_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.VARCHAR))
            End If

            ' 日付種類
            Select Case .Item("SEARCH_DATE_KBN").ToString()
                Case "01"
                    '入荷日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.INKA_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.INKA_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.INKA_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.INKA_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "02"
                    '出荷日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.OUTKA_PLAN_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.OUTKA_PLAN_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.OUTKA_PLAN_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.OUTKA_PLAN_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "03"
                    '空入荷日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.RTN_INKA_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.RTN_INKA_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.RTN_INKA_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.RTN_INKA_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "04"
                    '空出荷日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.RTN_OUTKA_PLAN_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.RTN_OUTKA_PLAN_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.RTN_OUTKA_PLAN_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.RTN_OUTKA_PLAN_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "05"
                    '最終請求日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LAST_INV_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LAST_INV_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LAST_INV_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LAST_INV_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "06"
                    '使用期限
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LT_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LT_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LT_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LT_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
                Case "07"
                    '最終セット料金計算日
                    whereStr = .Item("SEARCH_DATE_FROM").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LAST_CLC_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LAST_CLC_DATE >= @SEARCH_DATE_FROM " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.VARCHAR))
                    End If
                    whereStr = .Item("SEARCH_DATE_TO").ToString()
                    If Not String.IsNullOrEmpty(whereStr) Then
                        Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LAST_CLC_DATE) <> '' " & vbNewLine)
                        Me._StrSql.Append("AND ZAI_TSMC.LAST_CLC_DATE <= @SEARCH_DATE_TO " & vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.VARCHAR))
                    End If
            End Select

            ' 未請求
            whereStr = .Item("CHK_MISEIKYU").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("AND RTRIM(ZAI_TSMC.LAST_INV_DATE) = '' " & vbNewLine)
            End If

            ' 荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.CUST_CD_L = @CUST_CD_L " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.VARCHAR))
            End If

            ' 荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.CUST_CD_M = @CUST_CD_M " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.VARCHAR))
            End If

            ' ステータス
            Me._StrSql.Append("AND (1 = 0 " & vbNewLine)

            ' 在庫
            whereStr = .Item("CHK_ZAI").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("    OR ZAI_TSMC.STATUS = '01' " & vbNewLine)
            End If

            ' 出荷
            whereStr = .Item("CHK_OUTKA").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("    OR ZAI_TSMC.STATUS = '02' " & vbNewLine)
            End If

            ' 空在庫
            whereStr = .Item("CHK_RE_ZAI").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("    OR ZAI_TSMC.STATUS = '03' " & vbNewLine)
            End If

            ' 空出荷
            whereStr = .Item("CHK_RE_OUTKA").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("    OR ZAI_TSMC.STATUS = '04' " & vbNewLine)
            End If

            ' 返品出荷
            whereStr = .Item("CHK_HENPIN_OUTKA").ToString()
            If whereStr = "1" Then
                Me._StrSql.Append("    OR ZAI_TSMC.STATUS = '05' " & vbNewLine)
            End If

            Me._StrSql.Append("    )" & vbNewLine)

            '経過日数
            whereStr = .Item("NUM_KEIKA_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND KEIKA_DAYS.COUNT >= @NUM_KEIKA_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NUM_KEIKA_FROM", whereStr, DBDataType.NUMERIC))
            End If
            whereStr = .Item("NUM_KEIKA_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND KEIKA_DAYS.COUNT <= @NUM_KEIKA_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NUM_KEIKA_TO", whereStr, DBDataType.NUMERIC))
            End If

            ' 商品コード
            whereStr = .Item("CUST_GOODS_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.CUST_GOODS_CD LIKE @CUST_GOODS_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 商品名
            whereStr = .Item("GOODS_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.GOODS_NM LIKE @GOODS_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' ロット№
            whereStr = .Item("LOT_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.LOT_NO LIKE @LOT_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 検査番号1
            whereStr = .Item("LVL1_CHECK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.LVL1_CHECK LIKE @LVL1_CHECK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LVL1_CHECK", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 検査番号2
            whereStr = .Item("LVL2_CHECK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.LVL2_CHECK LIKE @LVL2_CHECK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LVL2_CHECK", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 検査状態
            whereStr = .Item("STOCK_TYPE_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.STOCK_TYPE = @STOCK_TYPE_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STOCK_TYPE_NM", whereStr, DBDataType.NVARCHAR))
            End If

            ' サプライヤーコード
            whereStr = .Item("SUPPLY_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.SUPPLY_CD LIKE @SUPPLY_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUPPLY_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' TSMC在庫番号
            whereStr = .Item("TSMC_REC_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.TSMC_REC_NO LIKE @TSMC_REC_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TSMC_REC_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' パレットNo.
            whereStr = .Item("PLT_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.PLT_NO LIKE @PLT_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' シリアルNo.
            whereStr = .Item("LV2_SERIAL_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.LV2_SERIAL_NO LIKE @LV2_SERIAL_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LV2_SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 最新区分
            whereStr = .Item("UP_FLG_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.UP_FLG = @UP_FLG_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_FLG_NM", whereStr, DBDataType.NVARCHAR))
            End If

            ' 回収区分
            whereStr = .Item("RETURN_FLAG_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.RETURN_FLAG = @RETURN_FLAG_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RETURN_FLAG_NM", whereStr, DBDataType.NVARCHAR))
            End If

            ' ASN No.
            whereStr = .Item("ASN_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.ASN_NO LIKE @ASN_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ASN_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 容器番号
            whereStr = .Item("CYLINDER_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.CYLINDER_NO LIKE @CYLINDER_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 荷姿
            whereStr = .Item("LVL1_UT").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.LVL1_UT LIKE @LVL1_UT " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LVL1_UT", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 個品ラベル
            whereStr = .Item("GRLVL1_PPNID").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.GRLVL1_PPNID LIKE @GRLVL1_PPNID " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GRLVL1_PPNID", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 出庫パレットNo.
            whereStr = .Item("DEPLT_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TSMC.DEPLT_NO LIKE @DEPLT_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPLT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 入荷送信実績
            whereStr = .Item("JISSEKI_SHORI_FLG_IN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ISNULL(SENDINEDI_TSMC.JISSEKI_SHORI_FLG, 'X') = @JISSEKI_SHORI_FLG_IN " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG_IN", whereStr, DBDataType.NVARCHAR))
            End If

            ' 出荷送信実績
            whereStr = .Item("JISSEKI_SHORI_FLG_OUT").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ISNULL(SENDOUTEDI_TSMC.JISSEKI_SHORI_FLG, 'X') = @JISSEKI_SHORI_FLG_OUT " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG_OUT", whereStr, DBDataType.NVARCHAR))
            End If

            ' 棟番号
            whereStr = .Item("TOU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TRS.TOU_NO LIKE @TOU_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 室番号
            whereStr = .Item("SITU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TRS.SITU_NO LIKE @SITU_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 棟室名
            whereStr = .Item("TOU_SITU_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND TOU_SITU.TOU_SITU_NM LIKE @TOU_SITU_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_SITU_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' ZONEコード
            whereStr = .Item("ZONE_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TRS.ZONE_CD LIKE @ZONE_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' ZONE名称
            whereStr = .Item("ZONE_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZONE.ZONE_NM LIKE @ZONE_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' ロケーション
            whereStr = .Item("LOCA").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND ZAI_TRS.LOCA LIKE @LOCA " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索処理：並び
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetOrder()

        ' 並び
        Me._StrSql.Append("ORDER BY" & vbNewLine)
        Me._StrSql.Append("      ZAI_TSMC.GOODS_NM " & vbNewLine)
        Me._StrSql.Append("    , ZAI_TSMC.LOT_NO " & vbNewLine)
        Me._StrSql.Append("    , ZAI_TSMC.LV2_SERIAL_NO " & vbNewLine)
        Me._StrSql.Append("    , ZAI_TSMC.CYLINDER_NO " & vbNewLine)

    End Sub

#End Region ' "検索処理"

#Region "更新処理"

    ''' <summary>
    ''' 更新処理(検査状態)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateZaiTsmc(ByVal ds As DataSet) As DataSet

        ' DataSet の IN情報の取得
        Dim dsIn As DataTable = ds.Tables("LMI550IN")

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI550DAC.SQL_UPDATE_ZAI_TSMC, dsIn.Rows(0).Item("NRS_BR_CD").ToString()))

            For i As Integer = 1 To dsIn.Rows.Count()
                Dim drIn As DataRow = dsIn.Rows(i - 1)

                ' SQLパラメータ初期化/設定
                Me._SqlPrmList = New ArrayList()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STOCK_TYPE", drIn.Item("STOCK_TYPE_NM").ToString(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", drIn.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TSMC_REC_NO", drIn.Item("TSMC_REC_NO").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", drIn.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", drIn.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                ' 更新実行および排他チェック
                If MyBase.GetUpdateResult(cmd) < 1 Then
                    MyBase.SetMessage("E011")
                    Return ds
                End If

                cmd.Parameters.Clear()

            Next

        End Using

        Return ds

    End Function

#End Region ' "更新処理"

#Region "共通処理"

#Region "SQL パラメータ設定"

#Region "SQL 共通パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    Private Sub SetDataInsertParameter(Optional ByVal sysDelFlg As String = BaseConst.FLG.OFF)

        'システム項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", sysDelFlg, DBDataType.CHAR))
        Call Me.SetSysdataParameter()

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    Private Sub SetSysdataParameter()

        'システム項目
        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    Private Sub SetSysdataTimeParameter()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

#End Region ' "SQL 共通パラメータ設定"

#End Region ' "SQL パラメータ設定"

#Region "スキーマ名称設定"

    ''' <summary>
    ''' 共通処理：スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region ' "スキーマ名称設定"

#Region "編集・変換"

#Region "Null変換"

    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region ' "Null変換"

#End Region ' "編集・変換"

#End Region ' "共通処理"

#End Region ' "Method"

End Class
