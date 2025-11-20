' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC742    : 納品書印刷(日本合成)
'  作  成  者       :  inoue
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports System.Reflection
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC742DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC742DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"


    Class RPT_ID

        ''' <summary>
        ''' 日本合成化学
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NICHIGO As String = "LMC742"

    End Class


    Class TABLE_NAME

        Public Const OUT_TABLE As String = "LMC742OUT"

    End Class


    ''' <summary>
    ''' 印刷タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Class NICHIGO_PRINT_TYPE

        ''' <summary>
        ''' 出荷指示書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SHIPMENT_INSTRUCTION As Integer = 1

        ''' <summary>
        ''' 送り状
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INVOICE As Integer = 2


        ''' <summary>
        ''' 荷物送附依頼書(兼運賃請求書)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SENDING_REQUEST As Integer = 3

        ''' <summary>
        ''' 荷物受取証
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RECEIPT As Integer = 4


    End Class

    Class OUT_TABLE_COLUMNS
        Public Const RPT_ID As String = "RPT_ID"
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const PRINT_SORT As String = "PRINT_SORT"
        Public Const TYPE As String = "PRINT_TYPE"
        Public Const OUTKA_NO_L As String = "OUTKA_NO_L"
        Public Const SYUKKASAKI_CD As String = "SYUKKASAKI_CD"
        Public Const SYUKKASAKI_ADD_LINE1 As String = "SYUKKASAKI_ADD_LINE1"
        Public Const SYUKKASAKI_ADD_LINE2 As String = "SYUKKASAKI_ADD_LINE2"
        Public Const SYUKKASAKI_TEL As String = "SYUKKASAKI_TEL"
        Public Const SYUKKASAKI_NM1 As String = "SYUKKASAKI_NM1"
        Public Const SYUKKASAKI_NM2 As String = "SYUKKASAKI_NM2"
        Public Const SYUKKASAKI_NM3 As String = "SYUKKASAKI_NM3"
        Public Const NOUKI_DATE As String = "NOUKI_DATE"
        Public Const NOUNYU_JIKOKU_NM As String = "NOUNYU_JIKOKU_NM"
        Public Const SYUKKA_DATE As String = "SYUKKA_DATE"
        Public Const SHIHARAININ_NM1L As String = "SHIHARAININ_NM1L"
        Public Const JYUCHUSAKI_NM1L As String = "JYUCHUSAKI_NM1L"
        Public Const SENPO_ORDER_NO As String = "SENPO_ORDER_NO"
        Public Const ITEM_GROUP As String = "ITEM_GROUP"
        Public Const ITEM_AISYO As String = "ITEM_AISYO"
        Public Const YOURYOU As String = "YOURYOU"
        Public Const KOBETSU_NINUGATA_CD As String = "KOBETSU_NINUGATA_CD"
        Public Const NISUGATA_NM As String = "NISUGATA_NM"
        Public Const ITEM_LENGTH As String = "ITEM_LENGTH"
        Public Const ITEM_WIDTH As String = "ITEM_WIDTH"
        Public Const THICKNESS As String = "THICKNESS"
        Public Const CONCENTRATION As String = "CONCENTRATION"
        Public Const PRIOR_DENP_NO As String = "PRIOR_DENP_NO"
        Public Const OUTKA_DENP_NO As String = "OUTKA_DENP_NO"
        Public Const SEIZO_LOT As String = "SEIZO_LOT"
        Public Const SUURYO As String = "SUURYO"
        Public Const KOSU As String = "KOSU"
        Public Const WH_NM As String = "WH_NM"
        Public Const KIHON_SURYO_TANI As String = "KIHON_SURYO_TANI"
        Public Const KANSAN_SURYO_KG As String = "KANSAN_SURYO_KG"
        Public Const KANSAN_SURYO_TANI_KG As String = "KANSAN_SURYO_TANI_KG"
        Public Const KANSAN_SURYO_LEN As String = "KANSAN_SURYO_LEN"
        Public Const KANSAN_SURYO_TANI_LEN As String = "KANSAN_SURYO_TANI_LEN"
        Public Const SEISEKISYO_HAKKOU_NM As String = "SEISEKISYO_HAKKOU_NM"
        Public Const SHITEI_DENP_NM As String = "SHITEI_DENP_NM"
        Public Const NOUNYUJI_JYOUKEN_NM1 As String = "NOUNYUJI_JYOUKEN_NM1"
        Public Const NOUNYUJI_JYOUKEN_NM2 As String = "NOUNYUJI_JYOUKEN_NM2"
        Public Const NOUNYUJI_JYOUKEN_NM3 As String = "NOUNYUJI_JYOUKEN_NM3"
        Public Const NOUNYUJI_JYOUKEN_NM4 As String = "NOUNYUJI_JYOUKEN_NM4"
        Public Const NOUNYUJI_JYOUKEN_NM5 As String = "NOUNYUJI_JYOUKEN_NM5"
        Public Const NOUNYUJI_JYOUKEN_BIKOU As String = "NOUNYUJI_JYOUKEN_BIKOU"
        Public Const YUSO_COMP_NM As String = "YUSO_COMP_NM"
        Public Const BIN_KBN_NM As String = "BIN_KBN_NM"
        Public Const SOTO_BIKOU As String = "SOTO_BIKOU"
        Public Const UCHI_BIKOU As String = "UCHI_BIKOU"
        Public Const SHIP_NM As String = "SHIP_NM"
        Public Const SHIP_AD As String = "SHIP_AD"
        Public Const SHIP_TEL As String = "SHIP_TEL"
        Public Const IS_REPRINT As String = "IS_REPRINT"
        Public Const PAGE_NO As String = "PAGE_NO"
    End Class



#Region "検索処理 SQL"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String _
        = " SELECT                                               " & vbNewLine _
        & "      'LMC742'                                        AS RPT_ID                                               " & vbNewLine _
        & "    , NRS_BR_CD                                       AS NRS_BR_CD                                               " & vbNewLine _
        & "    , 1                                               AS PRINT_SORT                                               " & vbNewLine _
        & "    , 0                                               AS PRINT_TYPE                                               " & vbNewLine _
        & "    , EDI_CTL_NO                                      AS OUTKA_NO_L                                               " & vbNewLine _
        & "    , SYUKKASAKI_CD                                   AS SYUKKASAKI_CD          -- 出荷先コード                                               " & vbNewLine _
        & "    , SYUKKASAKI_ADD1 + SYUKKASAKI_ADD2               AS SYUKKASAKI_ADD_LINE1   -- 出荷先住所 1行目                                               " & vbNewLine _
        & "    , SYUKKASAKI_ADD3                                 AS SYUKKASAKI_ADD_LINE2   -- 出荷先住所 2行目                                               " & vbNewLine _
        & "    , SYUKKASAKI_TEL                                  AS SYUKKASAKI_TEL         -- 出荷先 電話番号                                               " & vbNewLine _
        & "    , SYUKKASAKI_NM1                                  AS SYUKKASAKI_NM1         -- 出荷先名称1                                               " & vbNewLine _
        & "    , SYUKKASAKI_NM2                                  AS SYUKKASAKI_NM2         -- 出荷先名称2                                               " & vbNewLine _
        & "    , SYUKKASAKI_NM3                                  AS SYUKKASAKI_NM3         -- 出荷先名称3                                               " & vbNewLine _
        & "    , NOUKI_DATE                                      AS NOUKI_DATE             -- 納入日                                               " & vbNewLine _
        & "    , NOUNYU_JIKOKU_NM                                AS NOUNYU_JIKOKU_NM       -- 時間指定                                               " & vbNewLine _
        & "    , SYUKKA_DATE                                     AS SYUKKA_DATE            -- 出荷日                                               " & vbNewLine _
        & "    , SHIHARAININ_NM1L                                AS SHIHARAININ_NM1L       -- 契約先                                               " & vbNewLine _
        & "    , JYUCHUSAKI_NM1L                                 AS JYUCHUSAKI_NM1L        -- 需要家                                               " & vbNewLine _
        & "    , SENPO_ORDER_NO                                  AS SENPO_ORDER_NO         -- 御注文No                                               " & vbNewLine _
        & "    , ITEM_GROUP                                      AS ITEM_GROUP             -- 品目グループ                                               " & vbNewLine _
        & "    , ITEM_AISYO                                      AS ITEM_AISYO             -- 品目テキスト                                               " & vbNewLine _
        & "    , ISNULL(CONVERT(varchar, CALC.YOURYOU), '')  AS YOURYOU                -- 容量                                               " & vbNewLine _
        & "    , KOBETSU_NINUGATA_CD                             AS KOBETSU_NINUGATA_CD    -- 個別荷姿コード                                               " & vbNewLine _
        & "    , NISUGATA_NM                                     AS NISUGATA_NM            -- 荷姿名称                                               " & vbNewLine _
        & "    , ISNULL(STR(CALC.LENGTH) , '')                   AS ITEM_LENGTH            -- 長さ                                               " & vbNewLine _
        & "    , ISNULL(STR(CALC.WIDTH), '')                     AS ITEM_WIDTH             -- 幅                                               " & vbNewLine _
        & "    , ISNULL(STR(CALC.THICKNESS), '')                 AS THICKNESS              -- 厚さ                                               " & vbNewLine _
        & "    , CASE WHEN LEFT(GRADE1, 2) = '99'                                               " & vbNewLine _
        & "           THEN '1'                                               " & vbNewLine _
        & "           ELSE ISNULL(CONVERT(varchar, CALC.CONCENTRATION), '')                                                " & vbNewLine _
        & "      END                                             AS CONCENTRATION          -- 濃度                                               " & vbNewLine _
        & "    , CASE DENPYO_TYPE                                                " & vbNewLine _
        & "           WHEN 'ZOR1' THEN JYUCHU_DENP_NO                                               " & vbNewLine _
        & "           WHEN 'ZOR1' THEN HACCHU_DENP_NO                                               " & vbNewLine _
        & "           ELSE ''                                               " & vbNewLine _
        & "      END                                             AS PRIOR_DENP_NO          -- 先行伝票番号                                               " & vbNewLine _
        & "    , OUTKA_DENP_NO                                   AS OUTKA_DENP_NO          -- 出荷伝票番号                                               " & vbNewLine _
        & "    , SEIZO_LOT                                       AS SEIZO_LOT              -- ロット                                               " & vbNewLine _
        & "    , FORMAT(SUURYO, '0.###')                         AS SUURYO                 -- 数量                                               " & vbNewLine _
        & "    , FORMAT(KOSU, '0.###')                           AS KOSU                   -- 個数                                               " & vbNewLine _
        & "    , 'GNS NRS'                                       AS WH_NM                  -- 保管場所" & vbNewLine _
        & "    , KIHON_SURYO_TANI                                AS KIHON_SURYO_TANI       -- 基本数量単位                                               " & vbNewLine _
        & "    , CASE ITEM_GROUP                                               " & vbNewLine _
        & "           WHEN '06'                                                " & vbNewLine _
        & "           THEN FORMAT(ROUND(((CALC.LENGTH * CALC.WIDTH / 1000) / 500) * 12.5, 4), '#,0.###')                                               " & vbNewLine _
        & "           ELSE 0                                                    " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_KG        -- 換算数量(KG)                                               " & vbNewLine _
        & "    , CASE ITEM_GROUP                                               " & vbNewLine _
        & "           WHEN '06' THEN 'KG'                                                                    " & vbNewLine _
        & "           ELSE ''                                               " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_TANI_KG   -- 換算数量単位(KG)                                               " & vbNewLine _
        & "    , CASE ITEM_GROUP                                               " & vbNewLine _
        & "           WHEN '06'                                                " & vbNewLine _
        & "           THEN FORMAT(ROUND((CALC.LENGTH * CALC.WIDTH / 1000) / 500, 4), '#,0.###')                                                 " & vbNewLine _
        & "           ELSE 0                                                              " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_LEN       -- 換算数量(連)                                               " & vbNewLine _
        & "    , CASE ITEM_GROUP                                               " & vbNewLine _
        & "           WHEN '06' THEN '連'                                                                    " & vbNewLine _
        & "           ELSE ''                                               " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_TANI_LEN  -- 換算数量単位(連)                                               " & vbNewLine _
        & "    , SEISEKISYO_HAKKOU_NM                            AS SEISEKISYO_HAKKOU_NM   -- 成績書発行要否                                               " & vbNewLine _
        & "    , SHITEI_DENP_NM                                  AS SHITEI_DENP_NM         -- 指定伝票要否                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM1                            AS NOUNYUJI_JYOUKEN_NM1   -- 納入時条件1                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM2                            AS NOUNYUJI_JYOUKEN_NM2   -- 納入時条件2                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM3                            AS NOUNYUJI_JYOUKEN_NM3   -- 納入時条件3                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM4                            AS NOUNYUJI_JYOUKEN_NM4   -- 納入時条件4                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM5                            AS NOUNYUJI_JYOUKEN_NM5   -- 納入時条件5                                               " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_BIKOU                          AS NOUNYUJI_JYOUKEN_BIKOU -- 納入条件備考                                               " & vbNewLine _
        & "    , YUSO_COMP_NM                                    AS YUSO_COMP_NM           -- 配送会社                                               " & vbNewLine _
        & "    , BIN_KBN_NM                                      AS BIN_KBN_NM             -- 便                                               " & vbNewLine _
        & "    , SOTO_BIKOU                                      AS SOTO_BIKOU             -- 外備考                                               " & vbNewLine _
        & "    , UCHI_BIKOU                                      AS UCHI_BIKOU             -- 内備考                                               " & vbNewLine _
        & "    , ''                                              AS SHIP_NM                -- 発送元情報1                                               " & vbNewLine _
        & "    , ''                                              AS SHIP_AD                -- 発送元情報2                                               " & vbNewLine _
        & "    , ''                                              AS SHIP_TEL               -- 発送元情報3                                               " & vbNewLine _
        & "    , 0                                               AS IS_REPRINT             -- 再発行テキスト                                                   " & vbNewLine _
        & "    , 0                                               AS PAGE_NO                -- 出荷No.                                               " & vbNewLine _
        & "   FROM [LM_TRN].[dbo].[H_UNSOEDI_DTL_NCGO] AS BASE                                               " & vbNewLine _
        & "   LEFT JOIN                                                " & vbNewLine _
        & "    (SELECT                                                " & vbNewLine _
        & "            CRT_DATE                          AS CRT_DATE                                               " & vbNewLine _
        & "          , [FILE_NAME]                       AS [FILE_NAME]                                               " & vbNewLine _
        & "          , REC_NO                            AS REC_NO                                               " & vbNewLine _
        & "          , GYO                               AS GYO                                               " & vbNewLine _
        & "          , CASE ITEM_GROUP                                               " & vbNewLine _
        & "                 WHEN '01' THEN FORMAT(ISNULL(CONVERT(numeric(7, 2), YOURYOU), 0), '0.##')                                               " & vbNewLine _
        & "                 ELSE NULL                                               " & vbNewLine _
        & "            END                               AS YOURYOU          -- 容量                                                 " & vbNewLine _
        & "          , CASE ITEM_GROUP                                               " & vbNewLine _
        & "                 WHEN '06' THEN ISNULL(CONVERT(numeric, LEFT(GRADE2, 4)), 0)                                               " & vbNewLine _
        & "                 WHEN '17' THEN ISNULL(CONVERT(numeric, LEFT(GRADE2, 5)), 0)                                               " & vbNewLine _
        & "                 ELSE NULL                                               " & vbNewLine _
        & "            END                               AS [LENGTH]         -- 長さ                                               " & vbNewLine _
        & "          , CASE ITEM_GROUP                                               " & vbNewLine _
        & "                 WHEN '06' THEN ISNULL(CONVERT(numeric, SUBSTRING(GRADE2, 6, 5)), 0)                                               " & vbNewLine _
        & "                 WHEN '17' THEN ISNULL(CONVERT(numeric, LEFT(GRADE1, 4)), 0)                                               " & vbNewLine _
        & "                 ELSE NULL                                               " & vbNewLine _
        & "            END                               AS WIDTH            -- 幅                                               " & vbNewLine _
        & "          , CASE ITEM_GROUP                                               " & vbNewLine _
        & "                 WHEN '06' THEN ISNULL(CONVERT(numeric, LEFT(GRADE1, 5)), 0)                                                           " & vbNewLine _
        & "                 ELSE NULL                                               " & vbNewLine _
        & "            END                               AS THICKNESS        -- 厚さ                                               " & vbNewLine _
        & "          , CASE ITEM_GROUP                                               " & vbNewLine _
        & "                 WHEN '01' THEN FORMAT(ISNULL(CONVERT(numeric(5, 2), LEFT(GRADE1, 2)), 0) / 100, '0.#####')                                                               " & vbNewLine _
        & "                 ELSE NULL                                               " & vbNewLine _
        & "            END                               AS CONCENTRATION    -- 濃度                                                  " & vbNewLine _
        & "       FROM [LM_TRN].[dbo].[H_UNSOEDI_DTL_NCGO]                                               " & vbNewLine _
        & "    ) AS CALC                                               " & vbNewLine _
        & "   ON CALC.CRT_DATE  = BASE.CRT_DATE                                               " & vbNewLine _
        & "  AND CALC.FILE_NAME = BASE.FILE_NAME                                               " & vbNewLine _
        & "  AND CALC.REC_NO    = BASE.REC_NO                                               " & vbNewLine _
        & "  AND CALC.GYO       = BASE.GYO                                               " & vbNewLine _
        & "  WHERE EDI_CTL_NO = @OUTKA_NO_L                                               " & vbNewLine _
        & "    AND NRS_BR_CD  = @NRS_BR_CD                                               " & vbNewLine _
        & "                                                " & vbNewLine

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

    ''' <summary>
    ''' 納品書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC500IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '種別フラグを取得('0':出荷、'1':運送)
        Dim ptnFlag As String = Me._Row.Item("PTN_FLAG").ToString()

        '営業所CDを取得
        Dim nrs_br_cd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder(LMC742DAC.SQL_SELECT)

        ' SQL構築(条件設定)
        Me.SetConditionMasterSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        'cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod().Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '通常 納品書
        map.Add(OUT_TABLE_COLUMNS.RPT_ID, OUT_TABLE_COLUMNS.RPT_ID)
        map.Add(OUT_TABLE_COLUMNS.NRS_BR_CD, OUT_TABLE_COLUMNS.NRS_BR_CD)
        map.Add(OUT_TABLE_COLUMNS.PRINT_SORT, OUT_TABLE_COLUMNS.PRINT_SORT)
        map.Add(OUT_TABLE_COLUMNS.TYPE, OUT_TABLE_COLUMNS.TYPE)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_NO_L, OUT_TABLE_COLUMNS.OUTKA_NO_L)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_CD, OUT_TABLE_COLUMNS.SYUKKASAKI_CD)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE1, OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE1)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE2, OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE2)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_TEL, OUT_TABLE_COLUMNS.SYUKKASAKI_TEL)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM1, OUT_TABLE_COLUMNS.SYUKKASAKI_NM1)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM2, OUT_TABLE_COLUMNS.SYUKKASAKI_NM2)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM3, OUT_TABLE_COLUMNS.SYUKKASAKI_NM3)
        map.Add(OUT_TABLE_COLUMNS.NOUKI_DATE, OUT_TABLE_COLUMNS.NOUKI_DATE)
        map.Add(OUT_TABLE_COLUMNS.NOUNYU_JIKOKU_NM, OUT_TABLE_COLUMNS.NOUNYU_JIKOKU_NM)
        map.Add(OUT_TABLE_COLUMNS.SYUKKA_DATE, OUT_TABLE_COLUMNS.SYUKKA_DATE)
        map.Add(OUT_TABLE_COLUMNS.SHIHARAININ_NM1L, OUT_TABLE_COLUMNS.SHIHARAININ_NM1L)
        map.Add(OUT_TABLE_COLUMNS.JYUCHUSAKI_NM1L, OUT_TABLE_COLUMNS.JYUCHUSAKI_NM1L)
        map.Add(OUT_TABLE_COLUMNS.SENPO_ORDER_NO, OUT_TABLE_COLUMNS.SENPO_ORDER_NO)
        map.Add(OUT_TABLE_COLUMNS.ITEM_GROUP, OUT_TABLE_COLUMNS.ITEM_GROUP)
        map.Add(OUT_TABLE_COLUMNS.ITEM_AISYO, OUT_TABLE_COLUMNS.ITEM_AISYO)
        map.Add(OUT_TABLE_COLUMNS.YOURYOU, OUT_TABLE_COLUMNS.YOURYOU)
        map.Add(OUT_TABLE_COLUMNS.KOBETSU_NINUGATA_CD, OUT_TABLE_COLUMNS.KOBETSU_NINUGATA_CD)
        map.Add(OUT_TABLE_COLUMNS.NISUGATA_NM, OUT_TABLE_COLUMNS.NISUGATA_NM)
        map.Add(OUT_TABLE_COLUMNS.ITEM_LENGTH, OUT_TABLE_COLUMNS.ITEM_LENGTH)
        map.Add(OUT_TABLE_COLUMNS.ITEM_WIDTH, OUT_TABLE_COLUMNS.ITEM_WIDTH)
        map.Add(OUT_TABLE_COLUMNS.THICKNESS, OUT_TABLE_COLUMNS.THICKNESS)
        map.Add(OUT_TABLE_COLUMNS.CONCENTRATION, OUT_TABLE_COLUMNS.CONCENTRATION)
        map.Add(OUT_TABLE_COLUMNS.PRIOR_DENP_NO, OUT_TABLE_COLUMNS.PRIOR_DENP_NO)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_DENP_NO, OUT_TABLE_COLUMNS.OUTKA_DENP_NO)
        map.Add(OUT_TABLE_COLUMNS.SEIZO_LOT, OUT_TABLE_COLUMNS.SEIZO_LOT)
        map.Add(OUT_TABLE_COLUMNS.SUURYO, OUT_TABLE_COLUMNS.SUURYO)
        map.Add(OUT_TABLE_COLUMNS.KOSU, OUT_TABLE_COLUMNS.KOSU)
        map.Add(OUT_TABLE_COLUMNS.WH_NM, OUT_TABLE_COLUMNS.WH_NM)
        map.Add(OUT_TABLE_COLUMNS.KIHON_SURYO_TANI, OUT_TABLE_COLUMNS.KIHON_SURYO_TANI)
        map.Add(OUT_TABLE_COLUMNS.KANSAN_SURYO_KG, OUT_TABLE_COLUMNS.KANSAN_SURYO_KG)
        map.Add(OUT_TABLE_COLUMNS.KANSAN_SURYO_TANI_KG, OUT_TABLE_COLUMNS.KANSAN_SURYO_TANI_KG)
        map.Add(OUT_TABLE_COLUMNS.KANSAN_SURYO_LEN, OUT_TABLE_COLUMNS.KANSAN_SURYO_LEN)
        map.Add(OUT_TABLE_COLUMNS.KANSAN_SURYO_TANI_LEN, OUT_TABLE_COLUMNS.KANSAN_SURYO_TANI_LEN)
        map.Add(OUT_TABLE_COLUMNS.SEISEKISYO_HAKKOU_NM, OUT_TABLE_COLUMNS.SEISEKISYO_HAKKOU_NM)
        map.Add(OUT_TABLE_COLUMNS.SHITEI_DENP_NM, OUT_TABLE_COLUMNS.SHITEI_DENP_NM)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM1, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM1)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM2, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM2)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM3, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM3)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM4, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM4)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM5, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM5)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_BIKOU, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.YUSO_COMP_NM, OUT_TABLE_COLUMNS.YUSO_COMP_NM)
        map.Add(OUT_TABLE_COLUMNS.BIN_KBN_NM, OUT_TABLE_COLUMNS.BIN_KBN_NM)
        map.Add(OUT_TABLE_COLUMNS.SOTO_BIKOU, OUT_TABLE_COLUMNS.SOTO_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.UCHI_BIKOU, OUT_TABLE_COLUMNS.UCHI_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.SHIP_NM, OUT_TABLE_COLUMNS.SHIP_NM)
        map.Add(OUT_TABLE_COLUMNS.SHIP_AD, OUT_TABLE_COLUMNS.SHIP_AD)
        map.Add(OUT_TABLE_COLUMNS.SHIP_TEL, OUT_TABLE_COLUMNS.SHIP_TEL)
        map.Add(OUT_TABLE_COLUMNS.IS_REPRINT, OUT_TABLE_COLUMNS.IS_REPRINT)
        map.Add(OUT_TABLE_COLUMNS.PAGE_NO, OUT_TABLE_COLUMNS.PAGE_NO)

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUT_TABLE)

    End Function



    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))


            '管理番号
            whereStr = "N34529361"
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", whereStr, DBDataType.CHAR))

            'パターンフラグ('0':出荷、'1':運送)
            whereStr = .Item("PTN_FLAG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_FLAG", whereStr, DBDataType.CHAR))

            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))


        End With

    End Sub

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
