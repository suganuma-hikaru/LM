' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320  : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI320DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI320DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 検索処理 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                       " & vbNewLine _
                                               & "  SEIQ.SEIQTO_CD                              AS SEIQTO_CD                   " & vbNewLine _
                                               & ", SEIQ.SEIQTO_NM                              AS SEIQTO_NM                   " & vbNewLine _
                                               & ", SEIQ.CUST_KAGAMI_TYPE1                      AS KAGAMI_KB                   " & vbNewLine _
                                               & ", SEIQ.CUST_KAGAMI_TYPE2                      AS TOKU_CD                     " & vbNewLine _
                                               & ", SEIQ.CUST_KAGAMI_TYPE3                      AS HUTANKA                     " & vbNewLine _
                                               & ", SEIQ.CUST_KAGAMI_TYPE4                      AS KIGYO_CD  --Notes1907 add   " & vbNewLine

#End Region

#Region "検索処理 FROM句"

    ''' <summary>
    ''' 検索処理 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                            " & vbNewLine _
                                                    & "$LM_MST$..M_SEIQTO SEIQ                         " & vbNewLine

#End Region

#Region "検索処理 ORDER BY句"

    ''' <summary>
    ''' 検索処理 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                       " & vbNewLine _
                                                     & "  SEIQ.CUST_KAGAMI_TYPE1                       " & vbNewLine _
                                                     & ", SEIQ.CUST_KAGAMI_TYPE2                       " & vbNewLine _
                                                     & ", SEIQ.SEIQTO_CD                               " & vbNewLine

#End Region

#End Region

#Region "作成処理"

    ''' <summary>
    ''' 物理削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DATA As String = "DELETE                                         " & vbNewLine _
                                            & "FROM                                           " & vbNewLine _
                                            & " $LM_TRN$..I_DIC_SEKY_MEISAI                   " & vbNewLine _
                                            & "WHERE                                          " & vbNewLine _
                                            & "    NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                            & "AND SEIQTO_CD = @SEIQTO_CD                     " & vbNewLine _
                                            & "AND SKYU_DATE = @SEIQ_DATE                     " & vbNewLine

    ''' <summary>
    ''' 保存用検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INS As String = "SELECT                                          " & vbNewLine _
                                           & "    HED.NRS_BR_CD          AS NRS_BR_CD         " & vbNewLine _
                                           & "--Del 2019/06/14 006223  , ZAIK.WH_CD             AS WH_CD             " & vbNewLine _
                                           & "  , ''                     AS WH_CD             " & vbNewLine _
                                           & "  , HED.SKYU_NO            AS SKYU_NO           " & vbNewLine _
                                           & "  , HED.SKYU_DATE          AS SKYU_DATE         " & vbNewLine _
                                           & "  , HED.SEIQTO_CD          AS SEIQTO_CD         " & vbNewLine _
                                           & "  , HED.SEIQTO_NM          AS SEIQTO_NM         " & vbNewLine _
                                           & "  , KBN.KBN_NM2            AS MEISAI_NO         " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE1 AS KAGAMI_KB         " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE2 AS TOKU_CD           " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE3 AS HUTANKA           " & vbNewLine _
                                           & "  , SUM(ZAIK.ZAN_QT)       AS ZAN_QT            " & vbNewLine _
                                           & "  , CASE WHEN DTL.GROUP_KB = '01'               " & vbNewLine _
                                           & "          AND DTL.SEIQKMK_CD ='01' THEN         " & vbNewLine _
                                           & "              DTL.KEISAN_TLGK                   " & vbNewLine _
                                           & "         ELSE 0                                 " & vbNewLine _
                                           & "    END                    AS HOKAN             " & vbNewLine _
                                           & "  , CASE WHEN DTL.GROUP_KB = '02'               " & vbNewLine _
                                           & "          AND DTL.SEIQKMK_CD ='01' THEN         " & vbNewLine _
                                           & "              DTL.KEISAN_TLGK                   " & vbNewLine _
                                           & "         ELSE 0                                 " & vbNewLine _
                                           & "    END                    AS NIYAKU            " & vbNewLine _
                                           & "  , CASE WHEN DTL.GROUP_KB = '05'               " & vbNewLine _
                                           & "          AND DTL.SEIQKMK_CD ='01' THEN         " & vbNewLine _
                                           & "              DTL.KEISAN_TLGK                   " & vbNewLine _
                                           & "         ELSE 0                                 " & vbNewLine _
                                           & "    END                    AS YOKOMOCHI         " & vbNewLine _
                                           & "  , CASE WHEN DTL.GROUP_KB = '04'               " & vbNewLine _
                                           & "          AND DTL.SEIQKMK_CD ='01' THEN         " & vbNewLine _
                                           & "              DTL.KEISAN_TLGK                   " & vbNewLine _
                                           & "         ELSE 0                                 " & vbNewLine _
                                           & "    END                    AS GAICHU            " & vbNewLine _
                                           & "  , TAX.TAX_RATE           AS TAX               " & vbNewLine _
                                           & "FROM                                            " & vbNewLine _
                                           & "   $LM_TRN$..G_KAGAMI_HED HED                   " & vbNewLine _
                                           & "LEFT JOIN                                       " & vbNewLine _
                                           & "   $LM_TRN$..G_KAGAMI_DTL DTL                   " & vbNewLine _
                                           & " ON HED.SKYU_NO = DTL.SKYU_NO                   " & vbNewLine _
                                           & "AND DTL.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                           & "LEFT JOIN (                                     " & vbNewLine _
                                           & "    SELECT                                      " & vbNewLine _
                                           & "        ZAIK.NRS_BR_CD          AS NRS_BR_CD    " & vbNewLine _
                                           & "--Del 2019/06/14 006223      , ZAIK.WH_CD              AS WH_CD        " & vbNewLine _
                                           & "      , SUM(ZAIK.ZAN_QT)        AS ZAN_QT       " & vbNewLine _
                                           & "      , CUST.HOKAN_SEIQTO_CD    AS SEIQTO_CD    " & vbNewLine _
                                           & "    FROM                                        " & vbNewLine _
                                           & "       $LM_TRN$..G_ZAIK_ZAN ZAIK                " & vbNewLine _
                                           & "    LEFT JOIN                                   " & vbNewLine _
                                           & "       $LM_MST$..M_GOODS GOODS                  " & vbNewLine _
                                           & "     ON ZAIK.NRS_BR_CD = GOODS.NRS_BR_CD        " & vbNewLine _
                                           & "    AND ZAIK.GOODS_CD_NRS = GOODS.GOODS_CD_NRS  " & vbNewLine _
                                           & "    LEFT JOIN                                   " & vbNewLine _
                                           & "       $LM_MST$..M_CUST CUST                    " & vbNewLine _
                                           & "     ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD        " & vbNewLine _
                                           & "    AND GOODS.CUST_CD_L = CUST.CUST_CD_L        " & vbNewLine _
                                           & "    AND GOODS.CUST_CD_M = CUST.CUST_CD_M        " & vbNewLine _
                                           & "    AND GOODS.CUST_CD_S = CUST.CUST_CD_S        " & vbNewLine _
                                           & "    AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS      " & vbNewLine _
                                           & "    WHERE                                       " & vbNewLine _
                                           & "        ZAIK.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                           & "    AND ZAIK.INV_DATE_TO =@SEIQ_DATE            " & vbNewLine _
                                           & "    AND CUST.HOKAN_SEIQTO_CD = @SEIQTO_CD       " & vbNewLine _
                                           & "    AND ZAIK.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                           & "    GROUP BY                                    " & vbNewLine _
                                           & "        ZAIK.NRS_BR_CD                          " & vbNewLine _
                                           & "--Del 2019/06/14 006223      , ZAIK.WH_CD                              " & vbNewLine _
                                           & "      , CUST.HOKAN_SEIQTO_CD)ZAIK               " & vbNewLine _
                                           & " ON HED.NRS_BR_CD = ZAIK.NRS_BR_CD              " & vbNewLine _
                                           & "AND HED.SEIQTO_CD = ZAIK.SEIQTO_CD              " & vbNewLine _
                                           & "LEFT JOIN                                       " & vbNewLine _
                                           & "   $LM_MST$..M_SEIQTO SEIQ                      " & vbNewLine _
                                           & " ON HED.NRS_BR_CD = SEIQ.NRS_BR_CD              " & vbNewLine _
                                           & "AND HED.SEIQTO_CD = SEIQ.SEIQTO_CD              " & vbNewLine _
                                           & "AND SEIQ.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                           & "LEFT JOIN (                                     " & vbNewLine _
                                           & "    SELECT                                      " & vbNewLine _
                                           & "        TAX.TAX_RATE   AS TAX_RATE              " & vbNewLine _
                                           & "      , TAX.START_DATE AS START_DATE            " & vbNewLine _
                                           & "    FROM                                        " & vbNewLine _
                                           & "       $LM_MST$..M_TAX TAX                      " & vbNewLine _
                                           & "    INNER JOIN (                                " & vbNewLine _
                                           & "        SELECT                                  " & vbNewLine _
                                           & "            KBN1.KBN_GROUP_CD                   " & vbNewLine _
                                           & "          , KBN1.KBN_CD                         " & vbNewLine _
                                           & "          , KBN1.KBN_NM3                        " & vbNewLine _
                                           & "          , TAX2.START_DATE                     " & vbNewLine _
                                           & "          , TAX2.TAX_CD                         " & vbNewLine _
                                           & "        FROM (                                  " & vbNewLine _
                                           & "            SELECT                              " & vbNewLine _
                                           & "                MAX(START_DATE) AS START_DATE   " & vbNewLine _
                                           & "              , TAX3.TAX_CD     AS TAX_CD       " & vbNewLine _
                                           & "            FROM                                " & vbNewLine _
                                           & "               $LM_MST$..M_TAX TAX3             " & vbNewLine _
                                           & "            WHERE                               " & vbNewLine _
                                           & "                TAX3.START_DATE <= @SEIQ_DATE   " & vbNewLine _
                                           & "            GROUP BY                            " & vbNewLine _
                                           & "                TAX3.TAX_CD) TAX2               " & vbNewLine _
                                           & "        INNER JOIN                              " & vbNewLine _
                                           & "           $LM_MST$..Z_KBN KBN1                 " & vbNewLine _
                                           & "         ON KBN1.KBN_GROUP_CD = 'Z001'          " & vbNewLine _
                                           & "        AND KBN1.KBN_CD = '01'                  " & vbNewLine _
                                           & "        AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1    " & vbNewLine _
                                           & "     ON TAX1.START_DATE = TAX.START_DATE        " & vbNewLine _
                                           & "    AND TAX1.KBN_NM3 = TAX.TAX_CD)TAX           " & vbNewLine _
                                           & "  ON TAX.START_DATE <= @SEIQ_DATE               " & vbNewLine _
                                           & "LEFT JOIN                                       " & vbNewLine _
                                           & "    $LM_MST$..Z_KBN KBN                         " & vbNewLine _
                                           & " ON KBN.KBN_GROUP_CD = 'D020'                   " & vbNewLine _
                                           & "AND HED.SEIQTO_CD = KBN.KBN_NM1                 " & vbNewLine _
                                           & "WHERE                                           " & vbNewLine _
                                           & "    HED.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                           & "AND HED.SEIQTO_CD = @SEIQTO_CD                  " & vbNewLine _
                                           & "AND HED.SKYU_DATE = @SEIQ_DATE                  " & vbNewLine _
                                           & "AND(HED.STATE_KB = '01'                         " & vbNewLine _
                                           & " OR HED.STATE_KB = '02'                         " & vbNewLine _
                                           & " OR HED.STATE_KB = '03')                        " & vbNewLine _
                                           & "AND HED.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                           & "GROUP BY                                        " & vbNewLine _
                                           & "    HED.NRS_BR_CD                               " & vbNewLine _
                                           & "--Del 2019/06/14 006223  , ZAIK.WH_CD                                  " & vbNewLine _
                                           & "  , HED.SKYU_NO                                 " & vbNewLine _
                                           & "  , HED.SKYU_DATE                               " & vbNewLine _
                                           & "  , HED.SEIQTO_CD                               " & vbNewLine _
                                           & "  , HED.SEIQTO_NM                               " & vbNewLine _
                                           & "  , KBN.KBN_NM2                                 " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE1                      " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE2                      " & vbNewLine _
                                           & "  , SEIQ.CUST_KAGAMI_TYPE3                      " & vbNewLine _
                                           & "  , DTL.GROUP_KB                                " & vbNewLine _
                                           & "  , DTL.SEIQKMK_CD                              " & vbNewLine _
                                           & "  , DTL.KEISAN_TLGK                             " & vbNewLine _
                                           & "  , DTL.SKYU_SUB_NO                             " & vbNewLine _
                                           & "  , TAX.TAX_RATE                                " & vbNewLine _
                                           & "ORDER BY                                        " & vbNewLine _
                                           & "    HED.SEIQTO_CD                               " & vbNewLine




    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DATA As String = "INSERT INTO                                    " & vbNewLine _
                                            & " $LM_TRN$..I_DIC_SEKY_MEISAI                   " & vbNewLine _
                                            & "(                                              " & vbNewLine _
                                            & "  NRS_BR_CD                                    " & vbNewLine _
                                            & ", WH_CD                                        " & vbNewLine _
                                            & ", SKYU_NO                                      " & vbNewLine _
                                            & ", SKYU_DATE                                    " & vbNewLine _
                                            & ", SEIQTO_CD                                    " & vbNewLine _
                                            & ", SEIQTO_NM                                    " & vbNewLine _
                                            & ", KAGAMI_KB                                    " & vbNewLine _
                                            & ", MEISAI_NO                                    " & vbNewLine _
                                            & ", TOKU_CD                                      " & vbNewLine _
                                            & ", HUTANKA                                      " & vbNewLine _
                                            & ", H_JOKEN                                      " & vbNewLine _
                                            & ", ZAN_QT                                       " & vbNewLine _
                                            & ", HOKAN                                        " & vbNewLine _
                                            & ", NIYAKU                                       " & vbNewLine _
                                            & ", HN_TTL                                       " & vbNewLine _
                                            & ", NIZUKURI                                     " & vbNewLine _
                                            & ", YOKOMOCHI                                    " & vbNewLine _
                                            & ", GAICHU                                       " & vbNewLine _
                                            & ", TTL                                          " & vbNewLine _
                                            & ", TAX_TTL                                      " & vbNewLine _
                                            & ", G_TTL                                        " & vbNewLine _
                                            & ", TAX                                          " & vbNewLine _
                                            & ", SYS_ENT_DATE                                 " & vbNewLine _
                                            & ", SYS_ENT_TIME                                 " & vbNewLine _
                                            & ", SYS_ENT_PGID                                 " & vbNewLine _
                                            & ", SYS_ENT_USER                                 " & vbNewLine _
                                            & ", SYS_UPD_DATE                                 " & vbNewLine _
                                            & ", SYS_UPD_TIME                                 " & vbNewLine _
                                            & ", SYS_UPD_PGID                                 " & vbNewLine _
                                            & ", SYS_UPD_USER                                 " & vbNewLine _
                                            & ", SYS_DEL_FLG                                  " & vbNewLine _
                                            & ")VALUES(                                       " & vbNewLine _
                                            & "  @NRS_BR_CD                                   " & vbNewLine _
                                            & ", @WH_CD                                       " & vbNewLine _
                                            & ", @SKYU_NO                                     " & vbNewLine _
                                            & ", @SKYU_DATE                                   " & vbNewLine _
                                            & ", @SEIQTO_CD                                   " & vbNewLine _
                                            & ", @SEIQTO_NM                                   " & vbNewLine _
                                            & ", @KAGAMI_KB                                   " & vbNewLine _
                                            & ", @MEISAI_NO                                   " & vbNewLine _
                                            & ", @TOKU_CD                                     " & vbNewLine _
                                            & ", @HUTANKA                                     " & vbNewLine _
                                            & ", @H_JOKEN                                     " & vbNewLine _
                                            & ", @ZAN_QT                                      " & vbNewLine _
                                            & ", @HOKAN                                       " & vbNewLine _
                                            & ", @NIYAKU                                      " & vbNewLine _
                                            & ", @HN_TTL                                      " & vbNewLine _
                                            & ", @NIZUKURI                                    " & vbNewLine _
                                            & ", @YOKOMOCHI                                   " & vbNewLine _
                                            & ", @GAICHU                                      " & vbNewLine _
                                            & ", @TTL                                         " & vbNewLine _
                                            & ", @TAX_TTL                                     " & vbNewLine _
                                            & ", @G_TTL                                       " & vbNewLine _
                                            & ", @TAX                                         " & vbNewLine _
                                            & ", @SYS_ENT_DATE                                " & vbNewLine _
                                            & ", @SYS_ENT_TIME                                " & vbNewLine _
                                            & ", @SYS_ENT_PGID                                " & vbNewLine _
                                            & ", @SYS_ENT_USER                                " & vbNewLine _
                                            & ", @SYS_UPD_DATE                                " & vbNewLine _
                                            & ", @SYS_UPD_TIME                                " & vbNewLine _
                                            & ", @SYS_UPD_PGID                                " & vbNewLine _
                                            & ", @SYS_UPD_USER                                " & vbNewLine _
                                            & ", @SYS_DEL_FLG                                 " & vbNewLine _
                                            & ")                                              " & vbNewLine

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

#Region "SQLメイン処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI320DAC.SQL_SELECT_KENSAKU)        'SQL構築(SELECT句)
        Me._StrSql.Append(LMI320DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(FROM句)
        Call Me.SetSQLSelectWhereKensaku()                     '条件設定(WHERE句)
        Me._StrSql.Append(LMI320DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI320DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("KAGAMI_KB", "KAGAMI_KB")
        map.Add("TOKU_CD", "TOKU_CD")
        map.Add("HUTANKA", "HUTANKA")
        map.Add("KIGYO_CD", "KIGYO_CD") 'notes1907 add

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI320OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI320DAC.SQL_SELECT_INS)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetDeleteParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI320DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("MEISAI_NO", "MEISAI_NO")
        map.Add("KAGAMI_KB", "KAGAMI_KB")
        map.Add("TOKU_CD", "TOKU_CD")
        map.Add("HUTANKA", "HUTANKA")
        map.Add("ZAN_QT", "ZAN_QT")
        map.Add("HOKAN", "HOKAN")
        map.Add("NIYAKU", "NIYAKU")
        map.Add("YOKOMOCHI", "YOKOMOCHI")
        map.Add("GAICHU", "GAICHU")
        map.Add("TAX", "TAX")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI320INS")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>物理削除SQLの構築・発行</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI320IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI320DAC.SQL_DELETE_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetDeleteParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI320DAC", "DeleteData", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>登録SQLの構築・発行</remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI320IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI320DAC.SQL_INSERT_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim dr As DataRow() = ds.Tables("LMI320INS").Select
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamSave(dr(i))
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI320DAC", "InsertData", cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectWhereKensaku()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.SEIQTO_CD LIKE @SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            Else
                andstr.Append("AND SEIQ.SEIQTO_CD LIKE '00010%'")
                andstr.Append(vbNewLine)
            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.SEIQTO_NM LIKE @SEIQTO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '鑑種別
            whereStr = .Item("KAGAMI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.CUST_KAGAMI_TYPE1 = @KAGAMI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAGAMI_KB", whereStr, DBDataType.CHAR))
            End If

            '得意先コード
            whereStr = .Item("TOKU_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.CUST_KAGAMI_TYPE2 LIKE @TOKU_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOKU_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '負担課
            whereStr = .Item("HUTANKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND SEIQ.CUST_KAGAMI_TYPE3 LIKE @HUTANKA")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HUTANKA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("SEIQ.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(物理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDeleteParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_DATE", .Item("SEIQ_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSave(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", .Item("SEIQTO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAGAMI_KB", .Item("KAGAMI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEISAI_NO", .Item("MEISAI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOKU_CD", .Item("TOKU_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HUTANKA", .Item("HUTANKA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H_JOKEN", .Item("H_JOKEN").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAN_QT", .Item("ZAN_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN", .Item("HOKAN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU", .Item("NIYAKU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HN_TTL", .Item("HN_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIZUKURI", .Item("NIZUKURI").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI", .Item("YOKOMOCHI").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GAICHU", .Item("GAICHU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TTL", .Item("TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_TTL", .Item("TAX_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@G_TTL", .Item("G_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX", .Item("TAX").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

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
