' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH589    : EDI納品書 日本合成化学
'  作  成  者       :  inoue
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMH589DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH589DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"


    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String _
        = " SELECT DISTINCT                                         " & vbNewLine _
        & "        DTL.NRS_BR_CD                      AS NRS_BR_CD  " & vbNewLine _
        & "      , 'BZ'                               AS PTN_ID     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR2.PTN_CD                             " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR1.PTN_CD                             " & vbNewLine _
        & "             ELSE MR3.PTN_CD                             " & vbNewLine _
        & "        END                                AS PTN_CD     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR2.RPT_ID                             " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR1.RPT_ID                             " & vbNewLine _
        & "             ELSE MR3.RPT_ID                             " & vbNewLine _
        & "        END                                AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_COMMON As String _
        = " SELECT                                                                                                                " & vbNewLine _
        & "      CASE WHEN MR2.PTN_CD IS NOT NULL                                                                                 " & vbNewLine _
        & "           THEN MR2.RPT_ID                                                                                             " & vbNewLine _
        & "           WHEN MR1.PTN_CD IS NOT NULL                                                                                 " & vbNewLine _
        & "           THEN MR1.RPT_ID                                                                                             " & vbNewLine _
        & "           ELSE MR3.RPT_ID                                                                                             " & vbNewLine _
        & "      END                                             AS RPT_ID                                                        " & vbNewLine _
        & "    , DTL.NRS_BR_CD                                   AS NRS_BR_CD                                                     " & vbNewLine _
        & "    , 1                                               AS PRINT_SORT                                                    " & vbNewLine _
        & "    , 0                                               AS PRINT_TYPE                                                    " & vbNewLine _
        & "    , DTL.CUST_CD_L                                   AS CUST_CD_L                                                     " & vbNewLine _
        & "    , DTL.CUST_CD_M                                   AS CUST_CD_M                                                     " & vbNewLine _
        & "    , DTL.EDI_CTL_NO                                  AS EDI_CTL_NO                                                    " & vbNewLine _
        & "    , DTL.EDI_CTL_NO_CHU                              AS EDI_CTL_NO_CHU                                                " & vbNewLine _
        & "    , DTL.OUTKA_CTL_NO                                AS OUTKA_NO_L                                                    " & vbNewLine _
        & "    , DTL.OUTKA_CTL_NO_CHU                            AS OUTKA_NO_M                                                    " & vbNewLine _
        & "    , SYUKKASAKI_CD                                   AS SYUKKASAKI_CD          -- 出荷先コード                        " & vbNewLine _
        & "    , SYUKKASAKI_ADD1 + SYUKKASAKI_ADD2               AS SYUKKASAKI_ADD_LINE1   -- 出荷先住所 1行目                    " & vbNewLine _
        & "    , SYUKKASAKI_ADD3                                 AS SYUKKASAKI_ADD_LINE2   -- 出荷先住所 2行目                    " & vbNewLine _
        & "    , SYUKKASAKI_TEL                                  AS SYUKKASAKI_TEL         -- 出荷先 電話番号                     " & vbNewLine _
        & "    , SYUKKASAKI_NM1                                  AS SYUKKASAKI_NM1         -- 出荷先名称1                         " & vbNewLine _
        & "    , SYUKKASAKI_NM2                                  AS SYUKKASAKI_NM2         -- 出荷先名称2                         " & vbNewLine _
        & "    , SYUKKASAKI_NM3                                  AS SYUKKASAKI_NM3         -- 出荷先名称3                         " & vbNewLine _
        & "    , SYUKKASAKI_NM4                                  AS SYUKKASAKI_NM4         -- 出荷先名称4                         " & vbNewLine _
        & "    , NOUKI_DATE                                      AS NOUKI_DATE             -- 納入日                              " & vbNewLine _
        & "--upd 20170412    , SYUKKA_DATE                                     AS SYUKKA_DATE            -- 出荷日                              " & vbNewLine _
        & "    , HOUTL.OUTKO_DATE                                AS SYUKKA_DATE            -- 出荷日                              " & vbNewLine _
        & "    , SENPO_ORDER_NO                                  AS SENPO_ORDER_NO         -- 御注文No                            " & vbNewLine _
        & "    , ITEM_RYAKUGO                                    AS ITEM_RYAKUGO           -- 品名略号                            " & vbNewLine _
        & "    , ITEM_GROUP                                      AS ITEM_GROUP             -- 品目グループ                        " & vbNewLine _
        & "    , ITEM_AISYO                                      AS ITEM_AISYO             -- 品目テキスト                        " & vbNewLine _
        & "    , GRADE1                                          AS GRADE1                 -- グレード1                           " & vbNewLine _
        & "    , GRADE2                                          AS GRADE2                 -- グレード2                           " & vbNewLine _
        & "    , ISNULL(CONVERT(varchar, CALC.YOURYOU), '')      AS YOURYOU                -- 容量                                " & vbNewLine _
        & "    , KOBETSU_NISUGATA_CD                             AS KOBETSU_NISUGATA_CD    -- 個別荷姿コード                      " & vbNewLine _
        & "    , NISUGATA_NM                                     AS NISUGATA_NM            -- 荷姿名称                            " & vbNewLine _
        & "    , ISNULL(CONVERT(varchar, CALC.LENGTH) , '')      AS ITEM_LENGTH            -- 長さ                                " & vbNewLine _
        & "    , ISNULL(CONVERT(varchar, CALC.WIDTH), '')        AS ITEM_WIDTH             -- 幅                                  " & vbNewLine _
        & "    , ISNULL(CONVERT(varchar, CALC.THICKNESS), '')    AS THICKNESS              -- 厚さ                                " & vbNewLine _
        & "    , CASE WHEN LEFT(GRADE1, 2) = '99'                                                                                 " & vbNewLine _
        & "           THEN '1'                                                                                                    " & vbNewLine _
        & "           ELSE ISNULL(CONVERT(varchar, CALC.CONCENTRATION), '')                                                       " & vbNewLine _
        & "      END                                             AS CONCENTRATION          -- 濃度                                " & vbNewLine _
        & "    , CASE DATA_ID_DETAIL                                                                                              " & vbNewLine _
        & "           WHEN 'A' THEN JYUCHU_DENP_NO                                                                                " & vbNewLine _
        & "           ELSE HACCHU_DENP_NO                                                                                         " & vbNewLine _
        & "      END                                             AS PRIOR_DENP_NO          -- 先行伝票番号                        " & vbNewLine _
        & "    , OUTKA_DENP_NO                                   AS OUTKA_DENP_NO          -- 出荷伝票番号                        " & vbNewLine _
        & "    , OUTKA_DENP_DTL_NO                               AS OUTKA_DENP_DTL_NO      -- 出荷伝票明細番号                    " & vbNewLine _
        & "    , SEIZO_LOT                                       AS SEIZO_LOT              -- ロット                              " & vbNewLine _
        & "    , FORMAT(SUURYO, '0.###')                         AS SUURYO                 -- 数量                                " & vbNewLine _
        & "    , FORMAT(KOSU, '0.###')                           AS KOSU                   -- 個数                                " & vbNewLine _
        & "    , ISNULL(HOKAN.KBN_NM1, '')                       AS WH_NM                  -- 保管場所                            " & vbNewLine _
        & "    , CASE ITEM_GROUP                                                                                                  " & vbNewLine _
        & "           WHEN '06'                                                                                                   " & vbNewLine _
        & "           THEN KIHON_SURYO_TANI                                                                                       " & vbNewLine _
        & "           ELSE ''                                                                                                     " & vbNewLine _
        & "      END                                             AS KIHON_SURYO_TANI       -- 基本数量単位                        " & vbNewLine _
        & "    , CASE ITEM_GROUP                                                                                                  " & vbNewLine _
        & "           WHEN '06'                                                                                                   " & vbNewLine _
        & "           THEN FORMAT(ROUND(((CALC.LENGTH * CALC.WIDTH / 1000) / 500) * 12.5, 4), '0.###')                            " & vbNewLine _
        & "           ELSE '0'                                                                                                    " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_KG        -- 換算数量(KG)                        " & vbNewLine _
        & "    , CASE ITEM_GROUP                                                                                                  " & vbNewLine _
        & "           WHEN '06' THEN 'KG'                                                                                         " & vbNewLine _
        & "           ELSE ''                                                                                                     " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_TANI_KG   -- 換算数量単位(KG)                    " & vbNewLine _
        & "    , CASE ITEM_GROUP                                                                                                  " & vbNewLine _
        & "           WHEN '06'                                                                                                   " & vbNewLine _
        & "           THEN FORMAT(ROUND((CALC.LENGTH * CALC.WIDTH / 1000) / 500, 4), '0.###')                                     " & vbNewLine _
        & "           ELSE '0'                                                                                                    " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_LEN       -- 換算数量(連)                        " & vbNewLine _
        & "    , CASE ITEM_GROUP                                                                                                  " & vbNewLine _
        & "           WHEN '06' THEN '連'                                                                                         " & vbNewLine _
        & "           ELSE ''                                                                                                     " & vbNewLine _
        & "      END                                             AS KANSAN_SURYO_TANI_LEN  -- 換算数量単位(連)                    " & vbNewLine _
        & "    , SEISEKISYO_HAKKOU_NM                            AS SEISEKISYO_HAKKOU_NM   -- 成績書発行要否                      " & vbNewLine _
        & "    , YUSO_COMP_NM                                    AS YUSO_COMP_NM           -- 配送会社                            " & vbNewLine _
        & "    , BIN_KBN_NM                                      AS BIN_KBN_NM             -- 便                                  " & vbNewLine _
        & "    , SOTO_BIKOU                                      AS SOTO_BIKOU             -- 外備考                              " & vbNewLine _
        & "    , UCHI_BIKOU                                      AS UCHI_BIKOU             -- 内備考                              " & vbNewLine _
        & "    , ISNULL(HOKAN.KBN_NM4, '')                       AS SHIP_NM                -- 発送元情報1                         " & vbNewLine _
        & "    , ISNULL(HOKAN.KBN_NM5, '')                       AS SHIP_AD                -- 発送元情報2                         " & vbNewLine _
        & "    , ISNULL(HOKAN.KBN_NM6, '')                       AS SHIP_TEL               -- 発送元情報3                         " & vbNewLine _
        & "    , ISNULL(PRT_STATUS.IS_EXISTS, '0')               AS IS_REPRINT             -- 再発行テキスト                      " & vbNewLine _
        & "    , 0                                               AS PAGE_NO                -- 出荷No.                             " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNSO As String _
        = "    , NOUNYU_JIKOKU_NM                                AS NOUNYU_JIKOKU_NM       -- 時間指定                            " & vbNewLine _
        & "    , SHIHARAININ_NM1L                                AS SHIHARAININ_NM1L       -- 契約先                              " & vbNewLine _
        & "    , JYUCHUSAKI_NM1L                                 AS JYUCHUSAKI_NM1L        -- 需要家                              " & vbNewLine _
        & "    , SHITEI_DENP_NM                                  AS SHITEI_DENP_NM         -- 指定伝票要否                        " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM1                            AS NOUNYUJI_JYOUKEN_NM1   -- 納入時条件1                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM2                            AS NOUNYUJI_JYOUKEN_NM2   -- 納入時条件2                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM3                            AS NOUNYUJI_JYOUKEN_NM3   -- 納入時条件3                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM4                            AS NOUNYUJI_JYOUKEN_NM4   -- 納入時条件4                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM5                            AS NOUNYUJI_JYOUKEN_NM5   -- 納入時条件5                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM6                            AS NOUNYUJI_JYOUKEN_NM6   -- 納入時条件6                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM7                            AS NOUNYUJI_JYOUKEN_NM7   -- 納入時条件7                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM8                            AS NOUNYUJI_JYOUKEN_NM8   -- 納入時条件8                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM9                            AS NOUNYUJI_JYOUKEN_NM9   -- 納入時条件9                         " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_NM10                           AS NOUNYUJI_JYOUKEN_NM10  -- 納入時条件10                        " & vbNewLine _
        & "    , NOUNYUJI_JYOUKEN_BIKOU                          AS NOUNYUJI_JYOUKEN_BIKOU -- 納入条件備考                        " & vbNewLine _
        & "    , OKURIJYO_HIHYOJI_KBN                            AS OKURIJYO_HIHYOJI_KBN   -- 送状非表示区分                      " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUTKA As String _
        = "    , ''                            AS NOUNYU_JIKOKU_NM       -- 時間指定                                               " & vbNewLine _
        & "    , ''                            AS SHIHARAININ_NM1L       -- 契約先                                                 " & vbNewLine _
        & "    , ''                            AS JYUCHUSAKI_NM1L        -- 需要家                                                 " & vbNewLine _
        & "    , ''                            AS SHITEI_DENP_NM         -- 指定伝票要否                                           " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM1   -- 納入時条件1                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM2   -- 納入時条件2                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM3   -- 納入時条件3                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM4   -- 納入時条件4                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM5   -- 納入時条件5                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM6   -- 納入時条件6                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM7   -- 納入時条件7                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM8   -- 納入時条件8                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM9   -- 納入時条件9                                            " & vbNewLine _
        & "    , ''                            AS NOUNYUJI_JYOUKEN_NM10  -- 納入時条件10                                           " & vbNewLine _
        & "    , NOUNYU_JYOUKEN_BIKOU          AS NOUNYUJI_JYOUKEN_BIKOU -- 納入条件備考                                           " & vbNewLine _
        & "    , '0000000000'                  AS OKURIJYO_HIHYOJI_KBN   -- 送状非表示区分                                         " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String _
        = "        FROM                                                                                                           " & vbNewLine _
        & "             $LM_TRN$..{0} AS DTL                                                                                      " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..Z_KBN AS HOKAN                                                                                  " & vbNewLine _
        & "          ON HOKAN.KBN_GROUP_CD = 'N026'                                                                               " & vbNewLine _
        & "         AND HOKAN.KBN_NM2      = DTL.HOKAN_BASYO                                                                      " & vbNewLine _
        & "        LEFT JOIN                                                     --ADD 20170412                                   " & vbNewLine _
        & "             $LM_TRN$..H_OUTKAEDI_L AS HOUTL                                                                           " & vbNewLine _
        & "          ON HOUTL.NRS_BR_CD       = DTL.NRS_BR_CD                                                                     " & vbNewLine _
        & "         AND HOUTL.EDI_CTL_NO      = DTL.EDI_CTL_NO                                                                    " & vbNewLine _
        & "         AND HOUTL.SYS_DEL_FLG     = '0'                                                                               " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..M_CUST_RPT AS CUST_RPT_1                                                                        " & vbNewLine _
        & "          ON CUST_RPT_1.PTN_ID      = 'BZ'                                                                             " & vbNewLine _
        & "         AND CUST_RPT_1.NRS_BR_CD   = DTL.NRS_BR_CD                                                                    " & vbNewLine _
        & "         AND CUST_RPT_1.CUST_CD_L   = DTL.CUST_CD_L                                                                    " & vbNewLine _
        & "         AND CUST_RPT_1.CUST_CD_M   = DTL.CUST_CD_M                                                                    " & vbNewLine _
        & "         AND CUST_RPT_1.CUST_CD_S   = '00'                                                                             " & vbNewLine _
        & "         AND CUST_RPT_1.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..M_RPT AS MR1                                                                                    " & vbNewLine _
        & "          ON MR1.NRS_BR_CD          = CUST_RPT_1.NRS_BR_CD                                                             " & vbNewLine _
        & "         AND MR1.PTN_ID             = CUST_RPT_1.PTN_ID                                                                " & vbNewLine _
        & "         AND MR1.PTN_CD             = CUST_RPT_1.PTN_CD                                                                " & vbNewLine _
        & "         AND MR1.SYS_DEL_FLG        = '0'                                                                              " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..M_CUST_RPT AS CUST_RPT_2                                                                        " & vbNewLine _
        & "          ON CUST_RPT_2.PTN_ID      = 'BZ'                                                                             " & vbNewLine _
        & "         AND CUST_RPT_2.NRS_BR_CD   = DTL.NRS_BR_CD                                                                    " & vbNewLine _
        & "         AND CUST_RPT_2.CUST_CD_L   = DTL.CUST_CD_L                                                                    " & vbNewLine _
        & "         AND CUST_RPT_2.CUST_CD_M   = DTL.CUST_CD_M                                                                    " & vbNewLine _
        & "         AND CUST_RPT_2.CUST_CD_S   = '00'                                                                             " & vbNewLine _
        & "         AND CUST_RPT_2.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..M_RPT AS MR2                                                                                    " & vbNewLine _
        & "          ON MR2.NRS_BR_CD          = CUST_RPT_1.NRS_BR_CD                                                             " & vbNewLine _
        & "         AND MR2.PTN_ID             = CUST_RPT_1.PTN_ID                                                                " & vbNewLine _
        & "         AND MR2.PTN_CD             = CUST_RPT_1.PTN_CD                                                                " & vbNewLine _
        & "         AND MR2.SYS_DEL_FLG        = '0'                                                                              " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "             $LM_MST$..M_RPT AS MR3                                                                                    " & vbNewLine _
        & "          ON MR3.NRS_BR_CD          = DTL.NRS_BR_CD                                                                    " & vbNewLine _
        & "         AND MR3.PTN_ID             = 'BZ'                                                                             " & vbNewLine _
        & "         AND MR3.STANDARD_FLAG      = '01'                                                                             " & vbNewLine _
        & "         AND MR3.SYS_DEL_FLG        = '0'                                                                              " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "         (SELECT                                                                                                       " & vbNewLine _
        & "                 CRT_DATE                          AS CRT_DATE                                                         " & vbNewLine _
        & "               , [FILE_NAME]                       AS [FILE_NAME]                                                      " & vbNewLine _
        & "               , REC_NO                            AS REC_NO                                                           " & vbNewLine _
        & "               , GYO                               AS GYO                                                              " & vbNewLine _
        & "               , FORMAT(TRY_PARSE(YOURYOU AS numeric(7, 2)), '0.00')                                                   " & vbNewLine _
        & "                                                   AS YOURYOU          -- 容量                                         " & vbNewLine _
        & "               , CASE ITEM_GROUP                                                                                       " & vbNewLine _
        & "                      WHEN '06' THEN ISNULL(TRY_PARSE(LEFT(GRADE2, 4) AS numeric), 0)                                  " & vbNewLine _
        & "                      WHEN '17' THEN ISNULL(TRY_PARSE(LEFT(GRADE2, 5) AS numeric), 0)                                  " & vbNewLine _
        & "                      ELSE NULL                                                                                        " & vbNewLine _
        & "                 END                               AS [LENGTH]         -- 長さ                                         " & vbNewLine _
        & "               , CASE ITEM_GROUP                                                                                       " & vbNewLine _
        & "                      WHEN '06' THEN ISNULL(TRY_PARSE(SUBSTRING(GRADE2, 6, 5) AS numeric), 0)                          " & vbNewLine _
        & "                      WHEN '17' THEN ISNULL(TRY_PARSE(LEFT(GRADE1, 4) AS numeric), 0)                                  " & vbNewLine _
        & "                      ELSE NULL                                                                                        " & vbNewLine _
        & "                 END                               AS WIDTH            -- 幅                                           " & vbNewLine _
        & "               , CASE ITEM_GROUP                                                                                       " & vbNewLine _
        & "                      WHEN '06' THEN ISNULL(TRY_PARSE(LEFT(GRADE1, 5) AS numeric), 0)                                  " & vbNewLine _
        & "                      ELSE NULL                                                                                        " & vbNewLine _
        & "                 END                               AS THICKNESS        -- 厚さ                                         " & vbNewLine _
        & "               , CASE ITEM_GROUP                                                                                       " & vbNewLine _
        & "                      WHEN '01' THEN FORMAT(ISNULL(TRY_PARSE(LEFT(GRADE1, 2) AS numeric(5, 2)), 0) / 100, '0.#####')   " & vbNewLine _
        & "                      ELSE NULL                                                                                        " & vbNewLine _
        & "                 END                               AS CONCENTRATION    -- 濃度                                         " & vbNewLine _
        & "            FROM                                                                                                       " & vbNewLine _
        & "                 $LM_TRN$..{0}                                                                                         " & vbNewLine _
        & "         ) AS CALC                                                                                                     " & vbNewLine _
        & "          ON CALC.CRT_DATE  = DTL.CRT_DATE                                                                             " & vbNewLine _
        & "         AND CALC.FILE_NAME = DTL.FILE_NAME                                                                            " & vbNewLine _
        & "         AND CALC.REC_NO    = DTL.REC_NO                                                                               " & vbNewLine _
        & "         AND CALC.GYO       = DTL.GYO                                                                                  " & vbNewLine _
        & "        LEFT JOIN                                                                                                      " & vbNewLine _
        & "         (SELECT                                                                                                       " & vbNewLine _
        & "                 PRT.EDI_CTL_NO        AS EDI_CTL_NO                                                                   " & vbNewLine _
        & "               , '1'                   AS IS_EXISTS                                                                    " & vbNewLine _
        & "           FROM $LM_TRN$..H_EDI_PRINT AS PRT                                                                           " & vbNewLine _
        & "          WHERE                                                                                                        " & vbNewLine _
        & "                PRT.DENPYO_NO   = @DENPYO_NO                                                                           " & vbNewLine _
        & "            AND PRT.CUST_CD_L   = @CUST_CD_L                                                                           " & vbNewLine _
        & "            AND PRT.CUST_CD_M   = @CUST_CD_M                                                                           " & vbNewLine _
        & "            AND PRT.NRS_BR_CD   = @NRS_BR_CD                                                                           " & vbNewLine _
        & "            AND PRT.INOUT_KB    = '0'                                                                                  " & vbNewLine _
        & "            AND PRINT_TP        = '13'                                                                                 " & vbNewLine _
        & "            AND PRT.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
        & "            GROUP BY EDI_CTL_NO                                                                                        " & vbNewLine _
        & "         ) AS PRT_STATUS                                                                                               " & vbNewLine _
        & "        ON PRT_STATUS.EDI_CTL_NO = DTL.EDI_CTL_NO                                                                      " & vbNewLine

    ''' <summary>
    ''' SQL_WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String _
        = "  WHERE DTL.EDI_CTL_NO     = @EDI_CTL_NO        " & vbNewLine _
        & "    AND DTL.NRS_BR_CD      = @NRS_BR_CD         " & vbNewLine _
        & "    AND DTL.SYS_DEL_FLG    = '0'                " & vbNewLine _
        & "    AND DTL.DEL_KB         = '0'                " & vbNewLine _
        & "                                                " & vbNewLine
    Private Const SQL_WHERE_EXISTS_PRT As String _
        = "   AND PRT_STATUS.IS_EXISTS = '1'               " & vbNewLine

    Private Const SQL_WHERE_NOT_EXISTS_PRT As String _
        = "   AND PRT_STATUS.IS_EXISTS IS NULL             " & vbNewLine


    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String _
        = " ORDER BY                   " & vbNewLine _
        & "       DTL.EDI_CTL_NO       " & vbNewLine _
        & "     , DTL.EDI_CTL_NO_CHU   " & vbNewLine


#End Region

#Region "データセット名"

    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TableNames

        ''' <summary>
        ''' 帳票テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RPT As String = "M_RPT"

        ''' <summary>
        ''' INテーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_TABLE As String = "LMH589IN"


        ''' <summary>
        ''' OUTテーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_TABLE As String = "LMH589OUT"


        ''' <summary>
        ''' EDI印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI_PRINT As String = "H_EDI_PRINT"

    End Class

    ''' <summary>
    ''' メソッド名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Functions

        ''' <summary>
        ''' 帳票パターン
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SELECT_MPRT As String = "SelectMPrt"

        ''' <summary>
        ''' 印刷データ取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SELECT_PRT_DATA As String = "SelectPrintData"

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


    ''' <summary>
    ''' 入力テーブルカラム
    ''' </summary>
    ''' <remarks></remarks>
    Class IN_TABLE_COLUMNS
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const WH_CD As String = "WH_CD"
        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_CD_M As String = "CUST_CD_M"
        Public Const INOUT_KB As String = "INOUT_KB"
        Public Const OUTKA_PLAN_DATE_FROM As String = "OUTKA_PLAN_DATE_FROM"
        Public Const OUTKA_PLAN_DATE_TO As String = "OUTKA_PLAN_DATE_TO"
        Public Const PRTFLG As String = "PRTFLG"
        Public Const EDI_CUST_INDEX As String = "EDI_CUST_INDEX"
        Public Const RCV_NM_HED As String = "RCV_NM_HED"
        Public Const RCV_NM_DTL As String = "RCV_NM_DTL"
        Public Const INOUT_UMU_KB As String = "INOUT_UMU_KB"
        Public Const EDI_CTL_NO As String = "EDI_CTL_NO"
        Public Const ROW_NO As String = "ROW_NO"
        Public Const OUTPUT_SHUBETU As String = "OUTPUT_SHUBETU"
        Public Const DENPYO_NO As String = "DENPYO_NO"
        Public Const UNSO_TEHAI_KB As String = "UNSO_TEHAI_KB"

    End Class


    ''' <summary>
    ''' 出力テーブル カラム名
    ''' </summary>
    ''' <remarks></remarks>
    Class OUT_TABLE_COLUMNS
        Public Const RPT_ID As String = "RPT_ID"
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const PRINT_SORT As String = "PRINT_SORT"
        Public Const TYPE As String = "PRINT_TYPE"
        Public Const EDI_CTL_NO As String = "EDI_CTL_NO"
        Public Const EDI_CTL_NO_CHU As String = "EDI_CTL_NO_CHU"
        Public Const OUTKA_NO_L As String = "OUTKA_NO_L"
        Public Const OUTKA_NO_M As String = "OUTKA_NO_M"
        Public Const SYUKKASAKI_CD As String = "SYUKKASAKI_CD"
        Public Const SYUKKASAKI_ADD_LINE1 As String = "SYUKKASAKI_ADD_LINE1"
        Public Const SYUKKASAKI_ADD_LINE2 As String = "SYUKKASAKI_ADD_LINE2"
        Public Const SYUKKASAKI_TEL As String = "SYUKKASAKI_TEL"
        Public Const SYUKKASAKI_NM1 As String = "SYUKKASAKI_NM1"
        Public Const SYUKKASAKI_NM2 As String = "SYUKKASAKI_NM2"
        Public Const SYUKKASAKI_NM3 As String = "SYUKKASAKI_NM3"
        Public Const SYUKKASAKI_NM4 As String = "SYUKKASAKI_NM4"
        Public Const NOUKI_DATE As String = "NOUKI_DATE"
        Public Const NOUNYU_JIKOKU_NM As String = "NOUNYU_JIKOKU_NM"
        Public Const SYUKKA_DATE As String = "SYUKKA_DATE"
        Public Const SHIHARAININ_NM1L As String = "SHIHARAININ_NM1L"
        Public Const JYUCHUSAKI_NM1L As String = "JYUCHUSAKI_NM1L"
        Public Const SENPO_ORDER_NO As String = "SENPO_ORDER_NO"
        Public Const ITEM_RYAKUGO As String = "ITEM_RYAKUGO"
        Public Const ITEM_GROUP As String = "ITEM_GROUP"
        Public Const ITEM_AISYO As String = "ITEM_AISYO"
        Public Const GRADE1 As String = "GRADE1"
        Public Const GRADE2 As String = "GRADE2"
        Public Const YOURYOU As String = "YOURYOU"
        Public Const KOBETSU_NISUGATA_CD As String = "KOBETSU_NISUGATA_CD"
        Public Const NISUGATA_NM As String = "NISUGATA_NM"
        Public Const ITEM_LENGTH As String = "ITEM_LENGTH"
        Public Const ITEM_WIDTH As String = "ITEM_WIDTH"
        Public Const THICKNESS As String = "THICKNESS"
        Public Const CONCENTRATION As String = "CONCENTRATION"
        Public Const PRIOR_DENP_NO As String = "PRIOR_DENP_NO"
        Public Const OUTKA_DENP_NO As String = "OUTKA_DENP_NO"
        Public Const OUTKA_DENP_DTL_NO As String = "OUTKA_DENP_DTL_NO"
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
        Public Const NOUNYUJI_JYOUKEN_NM6 As String = "NOUNYUJI_JYOUKEN_NM6"
        Public Const NOUNYUJI_JYOUKEN_NM7 As String = "NOUNYUJI_JYOUKEN_NM7"
        Public Const NOUNYUJI_JYOUKEN_NM8 As String = "NOUNYUJI_JYOUKEN_NM8"
        Public Const NOUNYUJI_JYOUKEN_NM9 As String = "NOUNYUJI_JYOUKEN_NM9"
        Public Const NOUNYUJI_JYOUKEN_NM10 As String = "NOUNYUJI_JYOUKEN_NM10"
        Public Const NOUNYUJI_JYOUKEN_BIKOU As String = "NOUNYUJI_JYOUKEN_BIKOU"
        Public Const YUSO_COMP_NM As String = "YUSO_COMP_NM"
        Public Const BIN_KBN_NM As String = "BIN_KBN_NM"
        Public Const SOTO_BIKOU As String = "SOTO_BIKOU"
        Public Const UCHI_BIKOU As String = "UCHI_BIKOU"
        Public Const OKURIJYO_HIHYOJI_KBN As String = "OKURIJYO_HIHYOJI_KBN"
        Public Const SHIP_NM As String = "SHIP_NM"
        Public Const SHIP_AD As String = "SHIP_AD"
        Public Const SHIP_TEL As String = "SHIP_TEL"
        Public Const IS_REPRINT As String = "IS_REPRINT"
        Public Const PAGE_NO As String = "PAGE_NO"

    End Class

    ''' <summary>
    ''' 運送元区分
    ''' </summary>
    ''' <remarks></remarks>
    Class UNSO_MOTO_KB

        ''' <summary>
        ''' 日陸手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS As String = "10"

        ''' <summary>
        ''' 先方手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OTHER_PARTY As String = "20"

        ''' <summary>
        ''' 未定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNFIXED As String = "90"

    End Class


    Class SELECT_TABLE

        ''' <summary>
        ''' 出荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "H_OUTKAEDI_DTL_NCGO_NEW"

        ''' <summary>
        ''' 運送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO As String = "H_UNSOEDI_DTL_NCGO"

    End Class

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow = Nothing

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder = Nothing

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = Nothing

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TableNames.IN_TABLE)

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        Dim sql As String = Me.CreateSelectPrintSql(True)

        ' SQL文のコンパイル 
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectParameter()

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        ' 取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TableNames.RPT)

        Return ds

    End Function


    ''' <summary>
    ''' 納品送状対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品送状出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TableNames.IN_TABLE)

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        Dim sql As String = Me.CreateSelectPrintSql(False)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータ設定
        _SqlPrmList = New ArrayList()
        Me.SetSelectParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add(OUT_TABLE_COLUMNS.RPT_ID, OUT_TABLE_COLUMNS.RPT_ID)
        map.Add(OUT_TABLE_COLUMNS.NRS_BR_CD, OUT_TABLE_COLUMNS.NRS_BR_CD)
        map.Add(OUT_TABLE_COLUMNS.PRINT_SORT, OUT_TABLE_COLUMNS.PRINT_SORT)
        map.Add(OUT_TABLE_COLUMNS.TYPE, OUT_TABLE_COLUMNS.TYPE)
        map.Add(OUT_TABLE_COLUMNS.EDI_CTL_NO, OUT_TABLE_COLUMNS.EDI_CTL_NO)
        map.Add(OUT_TABLE_COLUMNS.EDI_CTL_NO_CHU, OUT_TABLE_COLUMNS.EDI_CTL_NO_CHU)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_NO_L, OUT_TABLE_COLUMNS.OUTKA_NO_L)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_NO_M, OUT_TABLE_COLUMNS.OUTKA_NO_M)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_CD, OUT_TABLE_COLUMNS.SYUKKASAKI_CD)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE1, OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE1)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE2, OUT_TABLE_COLUMNS.SYUKKASAKI_ADD_LINE2)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_TEL, OUT_TABLE_COLUMNS.SYUKKASAKI_TEL)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM1, OUT_TABLE_COLUMNS.SYUKKASAKI_NM1)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM2, OUT_TABLE_COLUMNS.SYUKKASAKI_NM2)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM3, OUT_TABLE_COLUMNS.SYUKKASAKI_NM3)
        map.Add(OUT_TABLE_COLUMNS.SYUKKASAKI_NM4, OUT_TABLE_COLUMNS.SYUKKASAKI_NM4)
        map.Add(OUT_TABLE_COLUMNS.NOUKI_DATE, OUT_TABLE_COLUMNS.NOUKI_DATE)
        map.Add(OUT_TABLE_COLUMNS.NOUNYU_JIKOKU_NM, OUT_TABLE_COLUMNS.NOUNYU_JIKOKU_NM)
        map.Add(OUT_TABLE_COLUMNS.SYUKKA_DATE, OUT_TABLE_COLUMNS.SYUKKA_DATE)
        map.Add(OUT_TABLE_COLUMNS.SHIHARAININ_NM1L, OUT_TABLE_COLUMNS.SHIHARAININ_NM1L)
        map.Add(OUT_TABLE_COLUMNS.JYUCHUSAKI_NM1L, OUT_TABLE_COLUMNS.JYUCHUSAKI_NM1L)
        map.Add(OUT_TABLE_COLUMNS.SENPO_ORDER_NO, OUT_TABLE_COLUMNS.SENPO_ORDER_NO)
        map.Add(OUT_TABLE_COLUMNS.ITEM_RYAKUGO, OUT_TABLE_COLUMNS.ITEM_RYAKUGO)
        map.Add(OUT_TABLE_COLUMNS.ITEM_GROUP, OUT_TABLE_COLUMNS.ITEM_GROUP)
        map.Add(OUT_TABLE_COLUMNS.ITEM_AISYO, OUT_TABLE_COLUMNS.ITEM_AISYO)
        map.Add(OUT_TABLE_COLUMNS.GRADE1, OUT_TABLE_COLUMNS.GRADE1)
        map.Add(OUT_TABLE_COLUMNS.GRADE2, OUT_TABLE_COLUMNS.GRADE2)
        map.Add(OUT_TABLE_COLUMNS.YOURYOU, OUT_TABLE_COLUMNS.YOURYOU)
        map.Add(OUT_TABLE_COLUMNS.KOBETSU_NISUGATA_CD, OUT_TABLE_COLUMNS.KOBETSU_NISUGATA_CD)
        map.Add(OUT_TABLE_COLUMNS.NISUGATA_NM, OUT_TABLE_COLUMNS.NISUGATA_NM)
        map.Add(OUT_TABLE_COLUMNS.ITEM_LENGTH, OUT_TABLE_COLUMNS.ITEM_LENGTH)
        map.Add(OUT_TABLE_COLUMNS.ITEM_WIDTH, OUT_TABLE_COLUMNS.ITEM_WIDTH)
        map.Add(OUT_TABLE_COLUMNS.THICKNESS, OUT_TABLE_COLUMNS.THICKNESS)
        map.Add(OUT_TABLE_COLUMNS.CONCENTRATION, OUT_TABLE_COLUMNS.CONCENTRATION)
        map.Add(OUT_TABLE_COLUMNS.PRIOR_DENP_NO, OUT_TABLE_COLUMNS.PRIOR_DENP_NO)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_DENP_NO, OUT_TABLE_COLUMNS.OUTKA_DENP_NO)
        map.Add(OUT_TABLE_COLUMNS.OUTKA_DENP_DTL_NO, OUT_TABLE_COLUMNS.OUTKA_DENP_DTL_NO)
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
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM6, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM6)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM7, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM7)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM8, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM8)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM9, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM9)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM10, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_NM10)
        map.Add(OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_BIKOU, OUT_TABLE_COLUMNS.NOUNYUJI_JYOUKEN_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.YUSO_COMP_NM, OUT_TABLE_COLUMNS.YUSO_COMP_NM)
        map.Add(OUT_TABLE_COLUMNS.BIN_KBN_NM, OUT_TABLE_COLUMNS.BIN_KBN_NM)
        map.Add(OUT_TABLE_COLUMNS.SOTO_BIKOU, OUT_TABLE_COLUMNS.SOTO_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.UCHI_BIKOU, OUT_TABLE_COLUMNS.UCHI_BIKOU)
        map.Add(OUT_TABLE_COLUMNS.OKURIJYO_HIHYOJI_KBN, OUT_TABLE_COLUMNS.OKURIJYO_HIHYOJI_KBN)
        map.Add(OUT_TABLE_COLUMNS.SHIP_NM, OUT_TABLE_COLUMNS.SHIP_NM)
        map.Add(OUT_TABLE_COLUMNS.SHIP_AD, OUT_TABLE_COLUMNS.SHIP_AD)
        map.Add(OUT_TABLE_COLUMNS.SHIP_TEL, OUT_TABLE_COLUMNS.SHIP_TEL)
        map.Add(OUT_TABLE_COLUMNS.IS_REPRINT, OUT_TABLE_COLUMNS.IS_REPRINT)
        map.Add(OUT_TABLE_COLUMNS.PAGE_NO, OUT_TABLE_COLUMNS.PAGE_NO)

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TableNames.OUT_TABLE)

        Return ds

    End Function


    ''' <summary>
    ''' 検索SQL生成
    ''' </summary>
    ''' <param name="IsSelectMPrt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSelectPrintSql(ByVal IsSelectMPrt As Boolean) As String

        ' 検索テーブル判定
        Dim selectTable As String = SELECT_TABLE.UNSO
        Dim addSelectData As String = LMH589DAC.SQL_SELECT_DATA_UNSO

        If (UNSO_MOTO_KB.OTHER_PARTY _
                .Equals(_Row.Item(IN_TABLE_COLUMNS.UNSO_TEHAI_KB))) Then
            selectTable = SELECT_TABLE.OUTKA
            addSelectData = LMH589DAC.SQL_SELECT_DATA_OUTKA
        End If

        ' SQL格納変数の初期化
        _StrSql = New StringBuilder()

        If (IsSelectMPrt) Then
            _StrSql.Append(LMH589DAC.SQL_SELECT_MPrt)
        Else
            _StrSql.Append(LMH589DAC.SQL_SELECT_DATA_COMMON)
            _StrSql.Append(addSelectData)
        End If

        _StrSql.Append(String.Format(LMH589DAC.SQL_FROM, selectTable))
        _StrSql.Append(LMH589DAC.SQL_WHERE)

        ' 
        If (LMConst.FLG.ON.Equals(_Row.Item(IN_TABLE_COLUMNS.PRTFLG))) Then
            ' 出力済
            _StrSql.Append(LMH589DAC.SQL_WHERE_EXISTS_PRT)
        ElseIf (LMConst.FLG.OFF.Equals(_Row.Item(IN_TABLE_COLUMNS.PRTFLG))) Then
            ' 未出力
            _StrSql.Append(LMH589DAC.SQL_WHERE_NOT_EXISTS_PRT)
        End If

        If (IsSelectMPrt = False) Then
            _StrSql.Append(LMH589DAC.SQL_ORDER_BY)
        End If


        'スキーマ名設定
        Return Me.SetSchemaNm(_StrSql.ToString(), _Row.Item(IN_TABLE_COLUMNS.NRS_BR_CD).ToString())

    End Function

#End Region

#Region "設定処理"

#Region "パラメータ設定"


    ''' <summary>
    ''' 検索用パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectParameter()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))




        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", Me._Row.Item("DENPYO_NO").ToString(), DBDataType.CHAR))


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

#End Region

End Class
