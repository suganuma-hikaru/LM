' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG040DAC : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(HED.SKYU_NO)	   AS SELECT_CNT   " & vbNewLine _
                                             & "FROM                                                                                               " & vbNewLine _
                                             & "                $LM_TRN$..G_KAGAMI_HED    HED                                                      " & vbNewLine _
                                             & "WHERE                                                                                              " & vbNewLine _
                                             & "       HED.SYS_DEL_FLG        =    '0'                                                             " & vbNewLine
    ''' <summary>
    ''' 請求鑑ヘッダ検索(削除済み経理戻し(黒)検索処理(データ取得)用処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT2 As String = " SELECT COUNT(HED.SKYU_NO)	   AS SELECT_CNT   " & vbNewLine _
                                            & "FROM                                                                                               " & vbNewLine _
                                            & "                $LM_TRN$..G_KAGAMI_HED    HED                                                      " & vbNewLine _
                                            & "--請求年月・請求先での一番大きい請求番号を取得する                        " & vbNewLine _
                                            & "   LEFT JOIN                                                              " & vbNewLine _
                                            & "   ( SELECT MAX(HED.SKYU_NO) AS SKYU_NO                                   " & vbNewLine _
                                            & "            FROM  $LM_TRN$..G_KAGAMI_HED HED                             " & vbNewLine _
                                            & "            WHERE                                                         " & vbNewLine _
                                            & "--                     HED.SYS_DEL_FLG     = '1'       --経理戻用           " & vbNewLine _
                                            & "--                 AND HED.RB_FLG          = '00'     --赤黒区分  00:黒     " & vbNewLine _
                                            & "--                AND HED.STATE_KB         < '03'     --03:経理取込済       " & vbNewLine _
                                            & "--                AND HED.SKYU_NO_RELATED  <> ''      --請求書番号（赤黒）  " & vbNewLine _
                                            & "                    HED.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                            & "                AND HED.SKYU_DATE  LIKE @SKYU_MONTH                       " & vbNewLine _
                                            & "                AND HED.SEIQTO_CD LIKE @SEIQTO_CD                        " & vbNewLine _
                                            & "           GROUP BY HED.SKYU_DATE, HED.SEIQTO_CD)  HED_MAX                " & vbNewLine _
                                            & "            ON  --HED.SYS_DEL_FLG     = '1'                                 " & vbNewLine _
                                            & "                HED.NRS_BR_CD  = @NRS_BR_CD                               " & vbNewLine _
                                            & "            AND HED.SKYU_DATE LIKE @SKYU_MONTH                            " & vbNewLine _
                                            & "            AND HED.SEIQTO_CD LIKE @SEIQTO_CD                             " & vbNewLine _
                                            & "    WHERE                                                                 " & vbNewLine _
                                            & "          HED_MAX.SKYU_NO = HED.SKYU_NO            　　                  " & vbNewLine _
                                            & "      AND HED.SYS_DEL_FLG     = '1'       --経理戻用　　                  " & vbNewLine _
                                            & "	     AND HED.RB_FLG          = '00'     --赤黒区分  00:黒                " & vbNewLine _
                                            & "	     AND HED.STATE_KB        < '03'     --03:経理取込済                  " & vbNewLine _
                                            & "	     AND HED.SKYU_NO_RELATED <> ''      --請求書番号（赤黒）             " & vbNewLine


    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                    " & vbNewLine _
                                            & "     MAIN.SKYU_NO                        AS   SKYU_NO                     " & vbNewLine _
                                            & "    ,MAIN.SEIQTO_CD                      AS   SEIQTO_CD                   " & vbNewLine _
                                            & "    ,MAIN.SEIQTO_NM                      AS   SEIQTO_NM                   " & vbNewLine _
                                            & "    ,MAIN.SKYU_DATE                      AS   SKYU_DATE                   " & vbNewLine _
                                            & "    ,MAIN.UNCHIN_IMP_FROM_DATE           AS   UNCHIN_IMP_FROM_DATE        " & vbNewLine _
                                            & "    ,MAIN.YOKOMOCHI_IMP_FROM_DATE        AS   YOKOMOCHI_IMP_FROM_DATE     " & vbNewLine _
                                            & "    ,MAIN.SAGYO_IMP_FROM_DATE            AS   SAGYO_IMP_FROM_DATE         " & vbNewLine _
                                            & "    ,MAIN.KAKUTEI_FLG                    AS   KAKUTEI_FLG                 " & vbNewLine _
                                            & "    ,MAIN.PRINT_FLG                      AS   PRINT_FLG                   " & vbNewLine _
                                            & "    ,MAIN.KEIRITORIKOMI_FLG              AS   KEIRITORIKOMI_FLG           " & vbNewLine _
                                            & "    ,MAIN.TAISHOGAI_FLG                  AS   TAISHOGAI_FLG               " & vbNewLine _
                                            & "    ,MAIN.SKYU_CSV_FLG                   AS   SKYU_CSV_FLG                " & vbNewLine _
                                            & "    ,MAIN.CRT_KB                         AS   CRT_KB                      " & vbNewLine _
                                            & "    ,MAIN.CRT_KB_NM                      AS   CRT_KB_NM                   " & vbNewLine _
                                            & "    ,MAIN.RB_FLG                         AS   RB_FLG                      " & vbNewLine _
                                            & "    ,MAIN.RB_FLG_NM                      AS   RB_FLG_NM                   " & vbNewLine _
                                            & "    ,MAIN.SKYU_NO_RELATED                AS   SKYU_NO_RELATED             " & vbNewLine _
                                            & "    ,MAIN.NRS_BR_CD                      AS   NRS_BR_CD                   " & vbNewLine _
                                            & "    ,MAIN.STATE_KB                       AS   STATE_KB                    " & vbNewLine _
                                            & "    ,MAIN.SYS_UPD_DATE                   AS   SYS_UPD_DATE                " & vbNewLine _
                                            & "    ,MAIN.SYS_UPD_TIME                   AS   SYS_UPD_TIME                " & vbNewLine _
                                            & "    ,MAIN.KAZEI_KIN + MAIN.MENZEI_KIN + MAIN.HIKA_UCHIZEI_KIN + MAIN.TAX_GK1 + MAIN.TAX_HASU_GK1 - (FLOOR(MAIN.KAZEI_KIN*MAIN.NEBIKI_RT1/100) + MAIN.NEBIKI_GK1 + FLOOR(MAIN.MENZEI_KIN*MAIN.NEBIKI_RT2/100) + MAIN.NEBIKI_GK2)  AS SEIQTO_SOGAKU " & vbNewLine _
                                            & "    ,MAIN.SYS_DEL_FLG                    AS   SYS_DEL_FLG                 " & vbNewLine _
                                            & "    ,MAIN.SAP_NO                         AS   SAP_NO                      " & vbNewLine _
                                            & "    ,MAIN.SAP_OUT_USER                   AS   SAP_OUT_USER                " & vbNewLine _
                                            & "    ,MAIN.SAP_OUT_USER_NM                AS   SAP_OUT_USER_NM             " & vbNewLine _
                                            & "FROM                                                                      " & vbNewLine _
                                            & "    (                                                                     " & vbNewLine _
                                            & "    SELECT                                                                " & vbNewLine _
                                            & "         HED.SKYU_NO                 AS    SKYU_NO                        " & vbNewLine _
                                            & "       , HED.SEIQTO_CD               AS    SEIQTO_CD                      " & vbNewLine _
                                            & "       , HED.SEIQTO_NM               AS    SEIQTO_NM                      " & vbNewLine _
                                            & "       , HED.SKYU_DATE               AS    SKYU_DATE                      " & vbNewLine _
                                            & "       , HED.UNCHIN_IMP_FROM_DATE    AS    UNCHIN_IMP_FROM_DATE           " & vbNewLine _
                                            & "       , HED.YOKOMOCHI_IMP_FROM_DATE AS    YOKOMOCHI_IMP_FROM_DATE        " & vbNewLine _
                                            & "       , HED.SAGYO_IMP_FROM_DATE     AS    SAGYO_IMP_FROM_DATE            " & vbNewLine _
                                            & "       , CASE                                                             " & vbNewLine _
                                            & "         WHEN HED.STATE_KB >='01'         THEN    '1'                     " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    KAKUTEI_FLG                     " & vbNewLine _
                                            & "       , CASE                                                             " & vbNewLine _
                                            & "         WHEN HED.STATE_KB >='02'          THEN    '1'                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    PRINT_FLG                       " & vbNewLine _
                                            & "       , CASE HED.STATE_KB                                                " & vbNewLine _
                                            & "         WHEN '03'         THEN    '1'                                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    KEIRITORIKOMI_FLG               " & vbNewLine _
                                            & "       , CASE HED.STATE_KB                                                " & vbNewLine _
                                            & "         WHEN '04'         THEN    '1'                                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    TAISHOGAI_FLG                   " & vbNewLine _
                                            & "       , HED.SKYU_CSV_FLG           AS    SKYU_CSV_FLG                    " & vbNewLine _
                                            & "       , HED.CRT_KB                 AS    CRT_KB                          " & vbNewLine _
                                            & "       , KBN1.#KBN#                 AS    CRT_KB_NM                       " & vbNewLine _
                                            & "       , HED.RB_FLG                 AS    RB_FLG                          " & vbNewLine _
                                            & "       , KBN2.#KBN#                 AS    RB_FLG_NM                       " & vbNewLine _
                                            & "       , HED.SKYU_NO_RELATED        AS    SKYU_NO_RELATED                 " & vbNewLine _
                                            & "       , HED.NRS_BR_CD              AS    NRS_BR_CD                       " & vbNewLine _
                                            & "       , HED.STATE_KB               AS    STATE_KB                        " & vbNewLine _
                                            & "       , HED.SYS_UPD_DATE           AS    SYS_UPD_DATE                    " & vbNewLine _
                                            & "       , HED.SYS_UPD_TIME           AS    SYS_UPD_TIME                    " & vbNewLine _
                                            & "       , HED.NEBIKI_RT1             AS    NEBIKI_RT1                      " & vbNewLine _
                                            & "       , HED.NEBIKI_GK1             AS    NEBIKI_GK1                      " & vbNewLine _
                                            & "       , HED.TAX_GK1                AS    TAX_GK1                         " & vbNewLine _
                                            & "       , HED.TAX_HASU_GK1           AS    TAX_HASU_GK1                    " & vbNewLine _
                                            & "       , HED.NEBIKI_RT2             AS    NEBIKI_RT2                      " & vbNewLine _
                                            & "       , HED.NEBIKI_GK2             AS    NEBIKI_GK2                      " & vbNewLine _
                                            & "       ,(SELECT                                                           " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & "        AND    DTL.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN    ('01','13')                " & vbNewLine _
                                            & "            )  AS  KAZEI_KIN                                              " & vbNewLine _
                                            & "    ,(SELECT                                                              " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & "        AND    DTL.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN    ('02','14')                " & vbNewLine _
                                            & "            )  AS  MENZEI_KIN                                             " & vbNewLine _
                                            & "    ,(SELECT                                                              " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & "        AND    DTL.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & " --UPD 2020/01/29 009561       AND       KMK.TAX_KB             IN   ('03','04','15')            " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN   ('03','04','15','16','17')            " & vbNewLine _
                                            & "            )  AS  HIKA_UCHIZEI_KIN                                       " & vbNewLine _
                                            & "       , HED.SYS_DEL_FLG                AS    SYS_DEL_FLG                 " & vbNewLine _
                                            & "       , HED.SAP_NO                     AS    SAP_NO                      " & vbNewLine _
                                            & "       , HED.SAP_OUT_USER               AS    SAP_OUT_USER                " & vbNewLine _
                                            & "       , USR.USER_NM                    AS    SAP_OUT_USER_NM             " & vbNewLine _
                                            & "    FROM                                                                  " & vbNewLine _
                                            & "                    $LM_TRN$..G_KAGAMI_HED    HED                           " & vbNewLine _
                                            & "                                                                          " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..Z_KBN                KBN1                     " & vbNewLine _
                                            & "    ON    KBN1.KBN_GROUP_CD       =    'K019'                             " & vbNewLine _
                                            & "    AND    KBN1.KBN_CD            =    HED.CRT_KB                         " & vbNewLine _
                                            & "    AND    KBN1.SYS_DEL_FLG       =    '0'                                " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..Z_KBN                KBN2                     " & vbNewLine _
                                            & "    ON    KBN2.KBN_GROUP_CD       =    'A001'                             " & vbNewLine _
                                            & "    AND    KBN2.KBN_CD            =    HED.RB_FLG                         " & vbNewLine _
                                            & "    AND    KBN2.SYS_DEL_FLG       =    '0'                                " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..S_USER               USR                    " & vbNewLine _
                                            & "    ON    USR.USER_CD             =    HED.SAP_OUT_USER                   " & vbNewLine _
                                            & "    WHERE                                                                 " & vbNewLine _
                                            & "           HED.SYS_DEL_FLG        =    '0'                                " & vbNewLine

    ''' <summary>
    ''' 請求鑑ヘッダ削除済み経理戻し(黒)検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = "SELECT                                                                    " & vbNewLine _
                                            & "     MAIN.SKYU_NO                        AS   SKYU_NO                     " & vbNewLine _
                                            & "    ,MAIN.SEIQTO_CD                      AS   SEIQTO_CD                   " & vbNewLine _
                                            & "    ,MAIN.SEIQTO_NM                      AS   SEIQTO_NM                   " & vbNewLine _
                                            & "    ,MAIN.SKYU_DATE                      AS   SKYU_DATE                   " & vbNewLine _
                                            & "    ,MAIN.UNCHIN_IMP_FROM_DATE           AS   UNCHIN_IMP_FROM_DATE        " & vbNewLine _
                                            & "    ,MAIN.YOKOMOCHI_IMP_FROM_DATE        AS   YOKOMOCHI_IMP_FROM_DATE     " & vbNewLine _
                                            & "    ,MAIN.SAGYO_IMP_FROM_DATE            AS   SAGYO_IMP_FROM_DATE         " & vbNewLine _
                                            & "    ,MAIN.KAKUTEI_FLG                    AS   KAKUTEI_FLG                 " & vbNewLine _
                                            & "    ,MAIN.PRINT_FLG                      AS   PRINT_FLG                   " & vbNewLine _
                                            & "    ,MAIN.KEIRITORIKOMI_FLG              AS   KEIRITORIKOMI_FLG           " & vbNewLine _
                                            & "    ,MAIN.TAISHOGAI_FLG                  AS   TAISHOGAI_FLG               " & vbNewLine _
                                            & "    ,MAIN.SKYU_CSV_FLG                   AS   SKYU_CSV_FLG                " & vbNewLine _
                                            & "    ,MAIN.CRT_KB                         AS   CRT_KB                      " & vbNewLine _
                                            & "    ,MAIN.CRT_KB_NM                      AS   CRT_KB_NM                   " & vbNewLine _
                                            & "    ,MAIN.RB_FLG                         AS   RB_FLG                      " & vbNewLine _
                                            & "    ,MAIN.RB_FLG_NM                      AS   RB_FLG_NM                   " & vbNewLine _
                                            & "    ,MAIN.SKYU_NO_RELATED                AS   SKYU_NO_RELATED             " & vbNewLine _
                                            & "    ,MAIN.NRS_BR_CD                      AS   NRS_BR_CD                   " & vbNewLine _
                                            & "    ,MAIN.STATE_KB                       AS   STATE_KB                    " & vbNewLine _
                                            & "    ,MAIN.SYS_UPD_DATE                   AS   SYS_UPD_DATE                " & vbNewLine _
                                            & "    ,MAIN.SYS_UPD_TIME                   AS   SYS_UPD_TIME                " & vbNewLine _
                                            & "    ,MAIN.KAZEI_KIN + MAIN.MENZEI_KIN + MAIN.HIKA_UCHIZEI_KIN + MAIN.TAX_GK1 + MAIN.TAX_HASU_GK1 - (FLOOR(MAIN.KAZEI_KIN*MAIN.NEBIKI_RT1/100) + MAIN.NEBIKI_GK1 + FLOOR(MAIN.MENZEI_KIN*MAIN.NEBIKI_RT2/100) + MAIN.NEBIKI_GK2)  AS SEIQTO_SOGAKU " & vbNewLine _
                                            & "    ,MAIN.SYS_DEL_FLG                    AS   SYS_DEL_FLG                 " & vbNewLine _
                                            & "    ,MAIN.SAP_NO                         AS   SAP_NO                      " & vbNewLine _
                                            & "    ,MAIN.SAP_OUT_USER                   AS   SAP_OUT_USER                " & vbNewLine _
                                            & "    ,MAIN.SAP_OUT_USER_NM                AS   SAP_OUT_USER_NM             " & vbNewLine _
                                            & "FROM                                                                      " & vbNewLine _
                                            & "    (                                                                     " & vbNewLine _
                                            & "    SELECT                                                                " & vbNewLine _
                                            & "         HED.SKYU_NO                 AS    SKYU_NO                        " & vbNewLine _
                                            & "       , HED.SEIQTO_CD               AS    SEIQTO_CD                      " & vbNewLine _
                                            & "       , HED.SEIQTO_NM               AS    SEIQTO_NM                      " & vbNewLine _
                                            & "       , HED.SKYU_DATE               AS    SKYU_DATE                      " & vbNewLine _
                                            & "       , HED.UNCHIN_IMP_FROM_DATE    AS    UNCHIN_IMP_FROM_DATE           " & vbNewLine _
                                            & "       , HED.YOKOMOCHI_IMP_FROM_DATE AS    YOKOMOCHI_IMP_FROM_DATE        " & vbNewLine _
                                            & "       , HED.SAGYO_IMP_FROM_DATE     AS    SAGYO_IMP_FROM_DATE            " & vbNewLine _
                                            & "       , CASE                                                             " & vbNewLine _
                                            & "         WHEN HED.STATE_KB >='01'         THEN    '1'                     " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    KAKUTEI_FLG                     " & vbNewLine _
                                            & "       , CASE                                                             " & vbNewLine _
                                            & "         WHEN HED.STATE_KB >='02'          THEN    '1'                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    PRINT_FLG                       " & vbNewLine _
                                            & "       , CASE HED.STATE_KB                                                " & vbNewLine _
                                            & "         WHEN '03'         THEN    '1'                                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    KEIRITORIKOMI_FLG               " & vbNewLine _
                                            & "       , CASE HED.STATE_KB                                                " & vbNewLine _
                                            & "         WHEN '04'         THEN    '1'                                    " & vbNewLine _
                                            & "         ELSE '0'                                                         " & vbNewLine _
                                            & "         END                        AS    TAISHOGAI_FLG                   " & vbNewLine _
                                            & "       , HED.SKYU_CSV_FLG           AS    SKYU_CSV_FLG                    " & vbNewLine _
                                            & "       , HED.CRT_KB                 AS    CRT_KB                          " & vbNewLine _
                                            & "       , KBN1.#KBN#                 AS    CRT_KB_NM                       " & vbNewLine _
                                            & "       , HED.RB_FLG                 AS    RB_FLG                          " & vbNewLine _
                                            & "       , KBN2.#KBN#                 AS    RB_FLG_NM                       " & vbNewLine _
                                            & "       , HED.SKYU_NO_RELATED        AS    SKYU_NO_RELATED                 " & vbNewLine _
                                            & "       , HED.NRS_BR_CD              AS    NRS_BR_CD                       " & vbNewLine _
                                            & "       , HED.STATE_KB               AS    STATE_KB                        " & vbNewLine _
                                            & "       , HED.SYS_UPD_DATE           AS    SYS_UPD_DATE                    " & vbNewLine _
                                            & "       , HED.SYS_UPD_TIME           AS    SYS_UPD_TIME                    " & vbNewLine _
                                            & "       , HED.NEBIKI_RT1             AS    NEBIKI_RT1                      " & vbNewLine _
                                            & "       , HED.NEBIKI_GK1             AS    NEBIKI_GK1                      " & vbNewLine _
                                            & "       , HED.TAX_GK1                AS    TAX_GK1                         " & vbNewLine _
                                            & "       , HED.TAX_HASU_GK1           AS    TAX_HASU_GK1                    " & vbNewLine _
                                            & "       , HED.NEBIKI_RT2             AS    NEBIKI_RT2                      " & vbNewLine _
                                            & "       , HED.NEBIKI_GK2             AS    NEBIKI_GK2                      " & vbNewLine _
                                            & "       , HED.SYS_DEL_FLG            AS    SYS_DEL_FLG                     " & vbNewLine _
                                            & "       , HED.SAP_NO                 AS    SAP_NO                          " & vbNewLine _
                                            & "       , HED.SAP_OUT_USER           AS    SAP_OUT_USER                    " & vbNewLine _
                                            & "       , USR.USER_NM                AS    SAP_OUT_USER_NM                 " & vbNewLine _
                                            & "       ,(SELECT                                                           " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & "        AND    DTL.SYS_UPD_DATE       =    HED.SYS_UPD_DATE                    " & vbNewLine _
                                            & "        AND    DTL.SYS_UPD_TIME       =    HED.SYS_UPD_TIME                    " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN    ('01','13')                " & vbNewLine _
                                            & "            )  AS  KAZEI_KIN                                              " & vbNewLine _
                                            & "    ,(SELECT                                                              " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & " --       AND    DTL.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN    ('02','14')                " & vbNewLine _
                                            & "            )  AS  MENZEI_KIN                                             " & vbNewLine _
                                            & "    ,(SELECT                                                              " & vbNewLine _
                                            & "            ISNULL(SUM(ISNULL(DTL.KEISAN_TLGK,0) - (ISNULL(DTL.NEBIKI_RTGK,0) + ISNULL(DTL.NEBIKI_GK,0))),0)    " & vbNewLine _
                                            & "        FROM                                                              " & vbNewLine _
                                            & "            $LM_TRN$..G_KAGAMI_DTL    DTL                                 " & vbNewLine _
                                            & "        LEFT JOIN   $LM_MST$..M_SEIQKMK       KMK                         " & vbNewLine _
                                            & "        ON     KMK.GROUP_KB           =    DTL.GROUP_KB                   " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD         =    DTL.SEIQKMK_CD                 " & vbNewLine _
                                            & "        AND    KMK.SEIQKMK_CD_S       =    DTL.SEIQKMK_CD_S               " & vbNewLine _
                                            & "        AND    KMK.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "        WHERE                                                             " & vbNewLine _
                                            & "             DTL.SKYU_NO              =    HED.SKYU_NO                    " & vbNewLine _
                                            & "  --      AND    DTL.SYS_DEL_FLG        =    '0'                            " & vbNewLine _
                                            & "--UPD 2020/01/29 009561        AND       KMK.TAX_KB             IN   ('03','04','15')            " & vbNewLine _
                                            & "        AND       KMK.TAX_KB             IN   ('03','04','15','16','17')            " & vbNewLine _
                                            & "            )  AS  HIKA_UCHIZEI_KIN                                       " & vbNewLine _
                                            & "    FROM                                                                  " & vbNewLine _
                                            & "                    $LM_TRN$..G_KAGAMI_HED    HED                           " & vbNewLine _
                                            & "                                                                          " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..Z_KBN                KBN1                     " & vbNewLine _
                                            & "    ON    KBN1.KBN_GROUP_CD       =    'K019'                             " & vbNewLine _
                                            & "    AND    KBN1.KBN_CD            =    HED.CRT_KB                         " & vbNewLine _
                                            & "    AND    KBN1.SYS_DEL_FLG       =    '0'                                " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..Z_KBN                KBN2                   " & vbNewLine _
                                            & "    ON    KBN2.KBN_GROUP_CD       =    'A001'                             " & vbNewLine _
                                            & "    AND    KBN2.KBN_CD            =    HED.RB_FLG                         " & vbNewLine _
                                            & "    AND    KBN2.SYS_DEL_FLG       =    '0'                                " & vbNewLine _
                                            & "    LEFT JOIN       $LM_MST$..S_USER               USR                    " & vbNewLine _
                                            & "    ON    USR.USER_CD             =    HED.SAP_OUT_USER                   " & vbNewLine _
                                            & "--請求年月・請求先での一番大きい請求番号を取得する                        " & vbNewLine _
                                            & "   LEFT JOIN                                                              " & vbNewLine _
                                            & "   ( SELECT MAX(HED.SKYU_NO) AS SKYU_NO                                   " & vbNewLine _
                                            & "            FROM  $LM_TRN$..G_KAGAMI_HED HED                             " & vbNewLine _
                                            & "            WHERE                                                         " & vbNewLine _
                                            & "--                     HED.SYS_DEL_FLG     = '1'       --経理戻用           " & vbNewLine _
                                            & "--                 AND HED.RB_FLG          = '00'     --赤黒区分  00:黒     " & vbNewLine _
                                            & "--                AND HED.STATE_KB         < '03'     --03:経理取込済       " & vbNewLine _
                                            & "--                AND HED.SKYU_NO_RELATED  <> ''      --請求書番号（赤黒）  " & vbNewLine _
                                            & "                    HED.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                            & "                AND HED.SKYU_DATE  LIKE @SKYU_MONTH                       " & vbNewLine _
                                            & "                AND HED.SEIQTO_CD LIKE @SEIQTO_CD                        " & vbNewLine _
                                            & "           GROUP BY HED.SKYU_DATE, HED.SEIQTO_CD)  HED_MAX                " & vbNewLine _
                                            & "            ON  --HED.SYS_DEL_FLG     = '1'                                 " & vbNewLine _
                                            & "                HED.NRS_BR_CD  = @NRS_BR_CD                               " & vbNewLine _
                                            & "            AND HED.SKYU_DATE LIKE @SKYU_MONTH                            " & vbNewLine _
                                            & "            AND HED.SEIQTO_CD LIKE @SEIQTO_CD                             " & vbNewLine _
                                            & "    WHERE                                                                 " & vbNewLine _
                                            & "            HED_MAX.SKYU_NO = HED.SKYU_NO            　　                  " & vbNewLine _
                                            & "        AND HED.SYS_DEL_FLG     = '1'       --経理戻用　　                  " & vbNewLine _
                                            & "	       AND HED.RB_FLG          = '00'     --赤黒区分  00:黒                " & vbNewLine _
                                            & "	       AND HED.STATE_KB        < '03'     --03:経理取込済                  " & vbNewLine _
                                            & "	       AND HED.SKYU_NO_RELATED <> ''      --請求書番号（赤黒）             " & vbNewLine


    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = ")  MAIN                    " & vbNewLine _
                                         & "ORDER BY     SKYU_NO DESC  " & vbNewLine

    Private Const SQL_ORDER_BY2 As String = ")  MAIN                    " & vbNewLine


#End Region

#Region "確定処理 SQL"

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE                                   " & vbNewLine _
                                       & "	   $LM_TRN$..G_KAGAMI_HED              " & vbNewLine _
                                       & "SET                                      " & vbNewLine _
                                       & "         STATE_KB     = '01'             " & vbNewLine _
                                       & "        ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                       & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                       & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                       & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                       & "WHERE                                    " & vbNewLine _
                                       & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                       & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                       & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine
#End Region

#Region "削除処理 SQL"

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 更新SQL(鑑ヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_HED As String = "UPDATE                                   " & vbNewLine _
                                                & "	   $LM_TRN$..G_KAGAMI_HED              " & vbNewLine _
                                                & "SET                                     " & vbNewLine _
                                                & "         SYS_DEL_FLG = '1'              " & vbNewLine _
                                                & "        ,SYS_UPD_DATE = @SYS_UPD_DATE   " & vbNewLine _
                                                & "        ,SYS_UPD_TIME = @SYS_UPD_TIME   " & vbNewLine _
                                                & "        ,SYS_UPD_PGID = @SYS_UPD_PGID   " & vbNewLine _
                                                & "        ,SYS_UPD_USER = @SYS_UPD_USER   " & vbNewLine _
                                                & "WHERE                                   " & vbNewLine _
                                                & "        SKYU_NO       = @SKYU_NO        " & vbNewLine _
                                                & "AND     SYS_UPD_DATE  = @HAITA_DATE     " & vbNewLine _
                                                & "AND     SYS_UPD_TIME  = @HAITA_TIME     " & vbNewLine

    ''' <summary>
    ''' 更新SQL(鑑明細
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_DTL As String = "UPDATE                                   " & vbNewLine _
                                                & "	   $LM_TRN$..G_KAGAMI_DTL              " & vbNewLine _
                                                & "SET                                     " & vbNewLine _
                                                & "         SYS_DEL_FLG = '1'              " & vbNewLine _
                                                & "        ,SYS_UPD_DATE = @SYS_UPD_DATE   " & vbNewLine _
                                                & "        ,SYS_UPD_TIME = @SYS_UPD_TIME   " & vbNewLine _
                                                & "        ,SYS_UPD_PGID = @SYS_UPD_PGID   " & vbNewLine _
                                                & "        ,SYS_UPD_USER = @SYS_UPD_USER   " & vbNewLine _
                                                & "WHERE                                   " & vbNewLine _
                                                & "        SKYU_NO       = @SKYU_NO        " & vbNewLine _
                                                & "    AND SYS_DEL_FLG   = '0'   --ADD 2018/08/23 依頼番号 : 002136 " & vbNewLine


    ''' <summary>
    ''' 更新SQL(鑑ヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CLEAR_STATEKB As String = "UPDATE                                   " & vbNewLine _
                                                     & "	   $LM_TRN$..G_KAGAMI_HED            " & vbNewLine _
                                                     & "SET                                      " & vbNewLine _
                                                     & "         STATE_KB = '00'                 " & vbNewLine _
                                                     & "        ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                                     & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                                     & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                                     & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                                     & "WHERE                                    " & vbNewLine _
                                                     & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                                     & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                                     & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(初期化対象データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CLEAR_HED As String = "SELECT                                                       " & vbNewLine _
                                                 & "     HED.SKYU_NO                  AS SKYU_NO                 " & vbNewLine _
                                                 & "    ,HED.STATE_KB                 AS STATE_KB                " & vbNewLine _
                                                 & "    ,HED.SKYU_DATE                AS SKYU_DATE               " & vbNewLine _
                                                 & "    ,HED.NRS_BR_CD                AS NRS_BR_CD               " & vbNewLine _
                                                 & "    ,HED.SEIQTO_CD                AS SEIQTO_CD               " & vbNewLine _
                                                 & "    ,HED.SEIQTO_PIC               AS SEIQTO_PIC              " & vbNewLine _
                                                 & "    ,HED.SEIQTO_NM                AS SEIQTO_NM               " & vbNewLine _
                                                 & "    ,HED.NEBIKI_RT1               AS NEBIKI_RT1              " & vbNewLine _
                                                 & "    ,HED.NEBIKI_GK1               AS NEBIKI_GK1              " & vbNewLine _
                                                 & "    ,HED.TAX_GK1                  AS TAX_GK1                 " & vbNewLine _
                                                 & "    ,HED.TAX_HASU_GK1             AS TAX_HASU_GK1            " & vbNewLine _
                                                 & "    ,HED.NEBIKI_RT2               AS NEBIKI_RT2              " & vbNewLine _
                                                 & "    ,HED.NEBIKI_GK2               AS NEBIKI_GK2              " & vbNewLine _
                                                 & "    ,HED.STORAGE_KB               AS STORAGE_KB              " & vbNewLine _
                                                 & "    ,HED.HANDLING_KB              AS HANDLING_KB             " & vbNewLine _
                                                 & "    ,HED.UNCHIN_KB                AS UNCHIN_KB               " & vbNewLine _
                                                 & "    ,HED.SAGYO_KB                 AS SAGYO_KB                " & vbNewLine _
                                                 & "    ,HED.YOKOMOCHI_KB             AS YOKOMOCHI_KB            " & vbNewLine _
                                                 & "    ,HED.CRT_KB                   AS CRT_KB                  " & vbNewLine _
                                                 & "    ,HED.UNCHIN_IMP_FROM_DATE     AS UNCHIN_IMP_FROM_DATE    " & vbNewLine _
                                                 & "    ,HED.SAGYO_IMP_FROM_DATE      AS SAGYO_IMP_FROM_DATE     " & vbNewLine _
                                                 & "    ,HED.YOKOMOCHI_IMP_FROM_DATE  AS YOKOMOCHI_IMP_FROM_DATE " & vbNewLine _
                                                 & "    ,HED.REMARK                   AS REMARK                  " & vbNewLine _
                                                 & "    ,HED.SKYU_NO_RELATED          AS SKYU_NO_RELATED         " & vbNewLine _
                                                 & "    ,HED.RB_FLG                   AS RB_FLG                  " & vbNewLine _
                                                 & "FROM $LM_TRN$..G_KAGAMI_HED HED                              " & vbNewLine _
                                                 & "WHERE                                                        " & vbNewLine _
                                                 & "      HED.SKYU_NO            = @SKYU_NO                      " & vbNewLine _
                                                 & "  AND HED.SYS_DEL_FLG        = '0'                           " & vbNewLine

    ''' <summary>
    ''' 請求明細ヘッダ検索処理(初期化対象データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CLEAR_DTL As String = "SELECT                                                       " & vbNewLine _
                                                 & "     DTL.SKYU_NO                  AS SKYU_NO                 " & vbNewLine _
                                                 & "    ,DTL.SKYU_SUB_NO              AS SKYU_SUB_NO             " & vbNewLine _
                                                 & "    ,DTL.GROUP_KB                 AS GROUP_KB                " & vbNewLine _
                                                 & "    ,DTL.SEIQKMK_CD               AS SEIQKMK_CD              " & vbNewLine _
                                                 & "    ,DTL.MAKE_SYU_KB              AS MAKE_SYU_KB             " & vbNewLine _
                                                 & "    ,DTL.BUSYO_CD                 AS BUSYO_CD                " & vbNewLine _
                                                 & "    ,DTL.KEISAN_TLGK              AS KEISAN_TLGK             " & vbNewLine _
                                                 & "    ,DTL.NEBIKI_RT                AS NEBIKI_RT               " & vbNewLine _
                                                 & "    ,DTL.NEBIKI_RTGK              AS NEBIKI_RTGK             " & vbNewLine _
                                                 & "    ,DTL.NEBIKI_GK                AS NEBIKI_GK               " & vbNewLine _
                                                 & "    ,DTL.TEKIYO                   AS TEKIYO                  " & vbNewLine _
                                                 & "    ,DTL.PRINT_SORT               AS PRINT_SORT              " & vbNewLine _
                                                 & "    ,DTL.TEMPLATE_IMP_FLG         AS TEMPLATE_IMP_FLG        " & vbNewLine _
                                                 & "FROM $LM_TRN$..G_KAGAMI_DTL DTL                              " & vbNewLine _
                                                 & "WHERE                                                        " & vbNewLine _
                                                 & "      DTL.SKYU_NO            = @SKYU_NO                      " & vbNewLine _
                                                 & "  AND DTL.SYS_DEL_FLG        = '0'                           " & vbNewLine

    ''' <summary>
    ''' 請求鑑ヘッダ新規追加用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED As String = "INSERT INTO $LM_TRN$..G_KAGAMI_HED " & vbNewLine _
                                              & "(                        " & vbNewLine _
                                              & " SKYU_NO                 " & vbNewLine _
                                              & ",STATE_KB                " & vbNewLine _
                                              & ",SKYU_DATE               " & vbNewLine _
                                              & ",NRS_BR_CD               " & vbNewLine _
                                              & ",SEIQTO_CD               " & vbNewLine _
                                              & ",SEIQTO_PIC              " & vbNewLine _
                                              & ",SEIQTO_NM               " & vbNewLine _
                                              & ",NEBIKI_RT1              " & vbNewLine _
                                              & ",NEBIKI_GK1              " & vbNewLine _
                                              & ",TAX_GK1                 " & vbNewLine _
                                              & ",TAX_HASU_GK1            " & vbNewLine _
                                              & ",NEBIKI_RT2              " & vbNewLine _
                                              & ",NEBIKI_GK2              " & vbNewLine _
                                              & ",STORAGE_KB              " & vbNewLine _
                                              & ",HANDLING_KB             " & vbNewLine _
                                              & ",UNCHIN_KB               " & vbNewLine _
                                              & ",SAGYO_KB                " & vbNewLine _
                                              & ",YOKOMOCHI_KB            " & vbNewLine _
                                              & ",CRT_KB                  " & vbNewLine _
                                              & ",UNCHIN_IMP_FROM_DATE    " & vbNewLine _
                                              & ",SAGYO_IMP_FROM_DATE     " & vbNewLine _
                                              & ",YOKOMOCHI_IMP_FROM_DATE " & vbNewLine _
                                              & ",REMARK                  " & vbNewLine _
                                              & ",SKYU_NO_RELATED         " & vbNewLine _
                                              & ",RB_FLG                  " & vbNewLine _
                                              & ",SYS_ENT_DATE            " & vbNewLine _
                                              & ",SYS_ENT_TIME            " & vbNewLine _
                                              & ",SYS_ENT_PGID            " & vbNewLine _
                                              & ",SYS_ENT_USER            " & vbNewLine _
                                              & ",SYS_UPD_DATE            " & vbNewLine _
                                              & ",SYS_UPD_TIME            " & vbNewLine _
                                              & ",SYS_UPD_PGID            " & vbNewLine _
                                              & ",SYS_UPD_USER            " & vbNewLine _
                                              & ",SYS_DEL_FLG             " & vbNewLine _
                                              & " )VALUES(                " & vbNewLine _
                                              & " @SKYU_NO                " & vbNewLine _
                                              & ",@STATE_KB               " & vbNewLine _
                                              & ",@SKYU_DATE              " & vbNewLine _
                                              & ",@NRS_BR_CD              " & vbNewLine _
                                              & ",@SEIQTO_CD              " & vbNewLine _
                                              & ",@SEIQTO_PIC             " & vbNewLine _
                                              & ",@SEIQTO_NM              " & vbNewLine _
                                              & ",@NEBIKI_RT1             " & vbNewLine _
                                              & ",@NEBIKI_GK1             " & vbNewLine _
                                              & ",@TAX_GK1                " & vbNewLine _
                                              & ",@TAX_HASU_GK1           " & vbNewLine _
                                              & ",@NEBIKI_RT2             " & vbNewLine _
                                              & ",@NEBIKI_GK2             " & vbNewLine _
                                              & ",@STORAGE_KB             " & vbNewLine _
                                              & ",@HANDLING_KB            " & vbNewLine _
                                              & ",@UNCHIN_KB              " & vbNewLine _
                                              & ",@SAGYO_KB               " & vbNewLine _
                                              & ",@YOKOMOCHI_KB           " & vbNewLine _
                                              & ",@CRT_KB                 " & vbNewLine _
                                              & ",@UNCHIN_IMP_FROM_DATE   " & vbNewLine _
                                              & ",@SAGYO_IMP_FROM_DATE    " & vbNewLine _
                                              & ",@YOKOMOCHI_IMP_FROM_DATE" & vbNewLine _
                                              & ",@REMARK                 " & vbNewLine _
                                              & ",@SKYU_NO_RELATED        " & vbNewLine _
                                              & ",@RB_FLG                 " & vbNewLine _
                                              & ",@SYS_ENT_DATE           " & vbNewLine _
                                              & ",@SYS_ENT_TIME           " & vbNewLine _
                                              & ",@SYS_ENT_PGID           " & vbNewLine _
                                              & ",@SYS_ENT_USER           " & vbNewLine _
                                              & ",@SYS_UPD_DATE           " & vbNewLine _
                                              & ",@SYS_UPD_TIME           " & vbNewLine _
                                              & ",@SYS_UPD_PGID           " & vbNewLine _
                                              & ",@SYS_UPD_USER           " & vbNewLine _
                                              & ",@SYS_DEL_FLG            " & vbNewLine _
                                              & ")                        " & vbNewLine

    ''' <summary>
    ''' 請求鑑明細新規追加用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DTL As String = "INSERT INTO $LM_TRN$..G_KAGAMI_DTL " & vbNewLine _
                                              & "(                        " & vbNewLine _
                                              & " SKYU_NO                 " & vbNewLine _
                                              & ",SKYU_SUB_NO             " & vbNewLine _
                                              & ",GROUP_KB                " & vbNewLine _
                                              & ",SEIQKMK_CD              " & vbNewLine _
                                              & ",MAKE_SYU_KB             " & vbNewLine _
                                              & ",BUSYO_CD                " & vbNewLine _
                                              & ",KEISAN_TLGK             " & vbNewLine _
                                              & ",NEBIKI_RT               " & vbNewLine _
                                              & ",NEBIKI_RTGK             " & vbNewLine _
                                              & ",NEBIKI_GK               " & vbNewLine _
                                              & ",TEKIYO                  " & vbNewLine _
                                              & ",PRINT_SORT              " & vbNewLine _
                                              & ",TEMPLATE_IMP_FLG        " & vbNewLine _
                                              & ",SYS_ENT_DATE            " & vbNewLine _
                                              & ",SYS_ENT_TIME            " & vbNewLine _
                                              & ",SYS_ENT_PGID            " & vbNewLine _
                                              & ",SYS_ENT_USER            " & vbNewLine _
                                              & ",SYS_UPD_DATE            " & vbNewLine _
                                              & ",SYS_UPD_TIME            " & vbNewLine _
                                              & ",SYS_UPD_PGID            " & vbNewLine _
                                              & ",SYS_UPD_USER            " & vbNewLine _
                                              & ",SYS_DEL_FLG             " & vbNewLine _
                                              & " )VALUES(                " & vbNewLine _
                                              & " @SKYU_NO                " & vbNewLine _
                                              & ",@SKYU_SUB_NO            " & vbNewLine _
                                              & ",@GROUP_KB               " & vbNewLine _
                                              & ",@SEIQKMK_CD             " & vbNewLine _
                                              & ",@MAKE_SYU_KB            " & vbNewLine _
                                              & ",@BUSYO_CD               " & vbNewLine _
                                              & ",@KEISAN_TLGK            " & vbNewLine _
                                              & ",@NEBIKI_RT              " & vbNewLine _
                                              & ",@NEBIKI_RTGK            " & vbNewLine _
                                              & ",@NEBIKI_GK              " & vbNewLine _
                                              & ",@TEKIYO                 " & vbNewLine _
                                              & ",@PRINT_SORT             " & vbNewLine _
                                              & ",@TEMPLATE_IMP_FLG       " & vbNewLine _
                                              & ",@SYS_ENT_DATE           " & vbNewLine _
                                              & ",@SYS_ENT_TIME           " & vbNewLine _
                                              & ",@SYS_ENT_PGID           " & vbNewLine _
                                              & ",@SYS_ENT_USER           " & vbNewLine _
                                              & ",@SYS_UPD_DATE           " & vbNewLine _
                                              & ",@SYS_UPD_TIME           " & vbNewLine _
                                              & ",@SYS_UPD_PGID           " & vbNewLine _
                                              & ",@SYS_UPD_USER           " & vbNewLine _
                                              & ",@SYS_DEL_FLG            " & vbNewLine _
                                              & ")                        " & vbNewLine
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
#End Region

#Region "請求データ出力処理"

    ''' <summary>
    ''' 請求データ出力用データ抽出
    ''' </summary>
    Private Const SQL_SELECT_CSV As String = "" _
            & "SELECT                                                               " & vbNewLine _
            & "   HED.SKYU_NO                                                       " & vbNewLine _
            & "  ,CASE WHEN HED.SKYU_DATE <> ''                                     " & vbNewLine _
            & "        THEN FORMAT(CONVERT(DATETIME,HED.SKYU_DATE),'yyyy/MM/dd')    " & vbNewLine _
            & "        ELSE '' END AS SKYU_DATE                                     " & vbNewLine _
            & "  ,HED.SEIQTO_CD                                                     " & vbNewLine _
            & "  ,DTL.TEKIYO                                                        " & vbNewLine _
            & "  ,CASE WHEN KMK.SEIQKMK_NM LIKE '%保管料%'                          " & vbNewLine _
            & "        THEN '9001'                                                  " & vbNewLine _
            & "        ELSE '9002' END AS SEIQKMK_CD                                " & vbNewLine _
            & "  ,CASE WHEN KMK.SEIQKMK_NM LIKE '%保管料%'                          " & vbNewLine _
            & "        THEN '保管料'                                                " & vbNewLine _
            & "        ELSE KMK.SEIQKMK_NM END AS SEIQKMK_NM                        " & vbNewLine _
            & "  ,DTL.KEISAN_TLGK - DTL.NEBIKI_RTGK - DTL.NEBIKI_GK AS SKYU_GK      " & vbNewLine _
            & "  ,CASE KB1.KBN_NM8                                                  " & vbNewLine _
            & "        WHEN '01' THEN '2' --課税                                    " & vbNewLine _
            & "        WHEN '02' THEN '0' --免税                                    " & vbNewLine _
            & "        WHEN '03' THEN '3' --非課税                                  " & vbNewLine _
            & "        WHEN '04' THEN '1' --内税                                    " & vbNewLine _
            & "        ELSE           ''                                            " & vbNewLine _
            & "        END AS TAX_KB                                                " & vbNewLine _
            & "  ,ISNULL(                                                           " & vbNewLine _
            & "    (                                                                " & vbNewLine _
            & "      SELECT                                                         " & vbNewLine _
            & "        TAX_RATE                                                     " & vbNewLine _
            & "      FROM                                                           " & vbNewLine _
            & "        $LM_MST$..M_TAX                                              " & vbNewLine _
            & "      WHERE                                                          " & vbNewLine _
            & "            TAX_CD = KB1.KBN_NM3                                     " & vbNewLine _
            & "        AND START_DATE IN (                                          " & vbNewLine _
            & "          SELECT                                                     " & vbNewLine _
            & "            MAX(START_DATE) AS START_DATE                            " & vbNewLine _
            & "          FROM                                                       " & vbNewLine _
            & "            $LM_MST$..M_TAX                                          " & vbNewLine _
            & "          WHERE                                                      " & vbNewLine _
            & "                TAX_CD = KB1.KBN_NM3                                 " & vbNewLine _
            & "            AND START_DATE <= HED.SKYU_DATE                          " & vbNewLine _
            & "            AND SYS_DEL_FLG = '0'                                    " & vbNewLine _
            & "          GROUP BY                                                   " & vbNewLine _
            & "              TAX_CD)                                                " & vbNewLine _
            & "        AND SYS_DEL_FLG = '0'                                        " & vbNewLine _
            & "      ),0                                                            " & vbNewLine _
            & "    ) AS TAX_RATE                                                    " & vbNewLine _
            & "  ,'' AS SHIHARAI_DATE                                               " & vbNewLine _
            & "FROM                                                                 " & vbNewLine _
            & "  $LM_TRN$..G_KAGAMI_HED AS HED                                      " & vbNewLine _
            & "INNER JOIN                                                           " & vbNewLine _
            & "  $LM_TRN$..G_KAGAMI_DTL AS DTL                                      " & vbNewLine _
            & "  ON  DTL.SKYU_NO = HED.SKYU_NO                                      " & vbNewLine _
            & "  AND DTL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
            & "LEFT JOIN                                                            " & vbNewLine _
            & "  $LM_MST$..M_SEIQKMK AS KMK                                         " & vbNewLine _
            & "  ON  KMK.GROUP_KB = DTL.GROUP_KB                                    " & vbNewLine _
            & "  AND KMK.SEIQKMK_CD = DTL.SEIQKMK_CD                                " & vbNewLine _
            & "  AND KMK.SEIQKMK_CD_S = DTL.SEIQKMK_CD_S                            " & vbNewLine _
            & "  AND KMK.SYS_DEL_FLG = '0'                                          " & vbNewLine _
            & "LEFT JOIN                                                            " & vbNewLine _
            & "  $LM_MST$..Z_KBN AS KB1                                             " & vbNewLine _
            & "  ON  KB1.KBN_GROUP_CD = 'Z001'                                      " & vbNewLine _
            & "  AND KB1.KBN_CD = KMK.TAX_KB                                        " & vbNewLine _
            & "  AND KB1.SYS_DEL_FLG = '0'                                          " & vbNewLine _
            & "WHERE                                                                " & vbNewLine _
            & "      HED.SKYU_NO = @SKYU_NO                                         " & vbNewLine _
            & "  AND HED.SYS_DEL_FLG = '0'                                          " & vbNewLine _
            & "ORDER BY                                                             " & vbNewLine _
            & "  DTL.SKYU_SUB_NO                                                    " & vbNewLine

    ''' <summary>
    ''' 請求データ出力用フラグ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CSV As String = "" _
            & "UPDATE                           " & vbNewLine _
            & "  $LM_TRN$..G_KAGAMI_HED         " & vbNewLine _
            & "SET                              " & vbNewLine _
            & "   STATE_KB = '04'               " & vbNewLine _
            & "  ,SKYU_CSV_FLG = '1'            " & vbNewLine _
            & "  ,SYS_UPD_DATE = @SYS_UPD_DATE  " & vbNewLine _
            & "  ,SYS_UPD_TIME = @SYS_UPD_TIME  " & vbNewLine _
            & "  ,SYS_UPD_PGID = @SYS_UPD_PGID  " & vbNewLine _
            & "  ,SYS_UPD_USER = @SYS_UPD_USER  " & vbNewLine _
            & "WHERE                            " & vbNewLine _
            & "  SKYU_NO = @SKYU_NO             " & vbNewLine _

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
    ''' 請求鑑ヘッダ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
#If False Then      'UPD 2018/08/21  依頼番号 : 002136  
         Me._StrSql.Append(LMG040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Call Me.SetConditionMasterSQL()                   '条件設定
#Else
        If ("1").Equals(inTbl.Rows(0).Item("KEIRI_MODOSHI_FLG").ToString.Trim) Then
            '削除済み経理戻し(黒)時のとき

            Me._StrSql.Append(LMG040DAC.SQL_SELECT_COUNT2)     'SQL構築(カウント用Select句)
            Call Me.SetConditionMasterSQL2()                   '条件設定

        Else
            Me._StrSql.Append(LMG040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
            Call Me.SetConditionMasterSQL()                   '条件設定

        End If

#End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '20210628 ベトナム対応Add
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20210628 ベトナム対応Add

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
#If False Then      'UPD 2018/08/09  依頼番号 : 002136  
            Me._StrSql.Append(LMG040DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
            Call Me.SetConditionMasterSQL()                   '条件設定
            Me._StrSql.Append(LMG040DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

#Else
        If ("1").Equals(inTbl.Rows(0).Item("KEIRI_MODOSHI_FLG").ToString.Trim) Then
            '削除済み経理戻し(黒)時のとき
            Me._StrSql.Append(LMG040DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
            Call Me.SetConditionMasterSQL2()                   '条件設定
            Me._StrSql.Append(LMG040DAC.SQL_ORDER_BY2)        'SQL構築(データ抽出用ORDER BY句)
        Else
            Me._StrSql.Append(LMG040DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
            Call Me.SetConditionMasterSQL()                   '条件設定
            Me._StrSql.Append(LMG040DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        End If


#End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_SOGAKU", "SEIQTO_SOGAKU")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("KAKUTEI_FLG", "KAKUTEI_FLG")
        map.Add("PRINT_FLG", "PRINT_FLG")
        map.Add("KEIRITORIKOMI_FLG", "KEIRITORIKOMI_FLG")
        map.Add("TAISHOGAI_FLG", "TAISHOGAI_FLG")
        map.Add("SKYU_CSV_FLG", "SKYU_CSV_FLG")
        map.Add("CRT_KB", "CRT_KB")
        map.Add("CRT_KB_NM", "CRT_KB_NM")
        map.Add("RB_FLG", "RB_FLG")
        map.Add("RB_FLG_NM", "RB_FLG_NM")
        map.Add("SKYU_NO_RELATED", "SKYU_NO_RELATED")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("STATE_KB", "STATE_KB")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("UNCHIN_IMP_FROM_DATE", "UNCHIN_IMP_FROM_DATE")
        map.Add("YOKOMOCHI_IMP_FROM_DATE", "YOKOMOCHI_IMP_FROM_DATE")
        map.Add("SAGYO_IMP_FROM_DATE", "SAGYO_IMP_FROM_DATE")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")                   'ADD 2018/08/09  依頼番号 : 002136 
        map.Add("SAP_NO", "SAP_NO")
        map.Add("SAP_OUT_USER", "SAP_OUT_USER")
        map.Add("SAP_OUT_USER_NM", "SAP_OUT_USER_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG040OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SKYU_MONTH").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SKYU_DATE LIKE @SKYU_MONTH")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SEIQTO_CD LIKE @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SKYU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SKYU_NO LIKE @SKYU_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SEIQTO_NM LIKE @SEIQTO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CRT_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_KB = @CRT_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("RB_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.RB_FLG = @RB_FLG")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SKYU_NO_RELATED").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SKYU_NO_RELATED LIKE @SKYU_NO_RELATED")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO_RELATED", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '進捗区分
            Dim arr As ArrayList = New ArrayList()
            If .Item("MIKAKUTEI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                arr.Add("'00'")
            End If
            If .Item("KAKUTEI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                arr.Add("'01'")
            End If
            If .Item("PRINT_FLG").ToString().Equals(LMConst.FLG.ON) Then
                arr.Add("'02'")
            End If
            If .Item("KEIRI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                arr.Add("'03'")
            End If
            If .Item("KEIRI_TAISHOGAI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                arr.Add("'04'")
            End If

            Dim stateKbn As String = Me.SetCheckBoxData(arr)

            Me._StrSql.Append(" AND HED.STATE_KB IN (" & stateKbn & ")")

            If .Item("SKYU_CSV_FLG").ToString().Equals(LMConst.FLG.ON) Then
                Me._StrSql.Append(" AND HED.SKYU_CSV_FLG <> '1'")
                Me._StrSql.Append(vbNewLine)
            End If

        End With


    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SKYU_MONTH").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SKYU_DATE LIKE @SKYU_MONTH")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SEIQTO_CD LIKE @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'whereStr = .Item("SKYU_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.SKYU_NO LIKE @SKYU_NO")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            'End If

            'whereStr = .Item("SEIQTO_NM").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.SEIQTO_NM LIKE @SEIQTO_NM")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            'End If

            'whereStr = .Item("CRT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CRT_KB = @CRT_KB")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", whereStr, DBDataType.CHAR))
            'End If

            'whereStr = .Item("RB_FLG").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.RB_FLG = @RB_FLG")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_FLG", whereStr, DBDataType.CHAR))
            'End If

            'whereStr = .Item("SKYU_NO_RELATED").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.SKYU_NO_RELATED LIKE @SKYU_NO_RELATED")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO_RELATED", String.Concat(whereStr, "%"), DBDataType.CHAR))
            'End If

            ''進捗区分
            'Dim arr As ArrayList = New ArrayList()
            'If .Item("MIKAKUTEI_FLG").ToString().Equals(LMConst.FLG.ON) Then
            '    arr.Add("'00'")
            'End If
            'If .Item("KAKUTEI_FLG").ToString().Equals(LMConst.FLG.ON) Then
            '    arr.Add("'01'")
            'End If
            'If .Item("PRINT_FLG").ToString().Equals(LMConst.FLG.ON) Then
            '    arr.Add("'02'")
            'End If
            'If .Item("KEIRI_FLG").ToString().Equals(LMConst.FLG.ON) Then
            '    arr.Add("'03'")
            'End If
            'If .Item("KEIRI_TAISHOGAI_FLG").ToString().Equals(LMConst.FLG.ON) Then
            '    arr.Add("'04'")
            'End If

            'Dim stateKbn As String = Me.SetCheckBoxData(arr)

            'Me._StrSql.Append(" AND HED.STATE_KB IN (" & stateKbn & ")")

        End With


    End Sub

    ''' <summary>
    ''' チェックボックスの条件を設定
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <remarks></remarks>
    Private Function SetCheckBoxData(ByVal arr As ArrayList) As String

        Dim rtnString As String = String.Empty

        If arr.Count = 0 Then
            rtnString = "'00','01','02','03','04'"
        Else
            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max

                If String.IsNullOrEmpty(rtnString) Then
                    rtnString = String.Concat(rtnString, arr(i))
                Else
                    rtnString = String.Concat(rtnString, ",", arr(i))
                End If

            Next

        End If

        Return rtnString

    End Function

#End Region

#Region "存在チェック"

    ''' <summary>
    ''' 請求鑑ヘッダ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Call Me.SetCheckSQL()                             '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCheckSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        whereStr = Me._Row.Item("SKYU_NO").ToString()
        If String.IsNullOrEmpty(whereStr) = False Then
            Me._StrSql.Append(" AND HED.SKYU_NO = @SKYU_NO")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", whereStr, DBDataType.CHAR))
        End If

    End Sub

#End Region

#Region "確定処理"

    ''' <summary>
    ''' 請求先マスタ存在チェックを行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索SQLの発行</remarks>
    Private Function ChkSeiqtoM(ByVal ds As DataSet, ByVal row As Integer) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Dim chkSql As StringBuilder = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(row)

        'SQL構築
        chkSql.Append(LMG000DAC.SQL_CHK_SEIQTO_M)  'チェック用SQL

        'SQL文のコンパイル
        Dim chkCmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(chkSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim reader As SqlDataReader = Nothing
        ChkSeiqtoM = False

        'SQLパラメータ初期化/設定
        Call Me.SetParamSeiqtoMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            chkCmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "UpStageKagamiHed", chkCmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(chkCmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount() = 0 Then
            ChkSeiqtoM = True
            MyBase.SetMessageStore("00" _
                                   , "E078" _
                                   , New String() {"請求先マスタ"} _
                                   , Me._Row.Item("RECORD_NO").ToString())
        End If

        chkCmd.Parameters.Clear()

        Return ChkSeiqtoM

    End Function

    ''' <summary>
    ''' 確定処理(請求鑑ヘッダステージ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpStageKagamiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG040DAC.SQL_UPDATE)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            If Me.ChkSeiqtoM(ds, i) = False Then

                'SQLパラメータ初期化/設定
                Call Me.SetParamUpdate()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMG040DAC", "UpStageKagamiHed", cmd)

                'SQLの発行
                If MyBase.GetUpdateResult(cmd) < 1 Then
                    MyBase.SetMessageStore("00" _
                       , "E011" _
                       , _
                       , Me._Row.Item("RECORD_NO").ToString() _
                       , "請求書番号" _
                       , Me._Row.Item("SKYU_NO").ToString())

                End If

                cmd.Parameters.Clear()

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' ステージアップ処理(請求鑑ヘッダステージ/取込開始日更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpKakuteiHed(ByVal ds As DataSet) As DataSet

        If Me.ChkSeiqtoM(ds, 0) = False Then

            'DataSetのIN情報を取得
            Dim inTbl As DataTable = ds.Tables("LMG040IN")

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(0)

            'SQL構築
            Me._StrSql.Append(LMG000DAC.SQL_UP_HED_KAKUTEI)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpHedKakutei()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG040DAC", "UpKakuteiHed", cmd)

            'SQLの発行
            If MyBase.GetUpdateResult(cmd) = 0 Then
                MyBase.SetMessageStore("00" _
                                       , "E011" _
                                       , _
                                       , Me._Row.Item("RECORD_NO").ToString())
            End If

            cmd.Parameters.Clear()

        End If

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求マスタ存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSeiqtoMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(確定処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpHedKakutei()

        'ヘッダ部更新時共通
        Call Me.SetParamUpdate()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", .Item("STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_IMP_FROM_DATE", .Item("UNCHIN_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_IMP_FROM_DATE", .Item("SAGYO_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_IMP_FROM_DATE", .Item("YOKOMOCHI_IMP_FROM_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

#End Region

#End Region

#Region "削除処理"

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 請求先マスタチェック呼び出し元
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function ChkSeiqtoMsub(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '請求先マスタ存在チェック
        If Me.ChkSeiqtoM(ds, 0) = True Then
            MyBase.SetMessage("E078", New String() {"請求先マスタ"})
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(鑑ヘッダ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpdateDeleteHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG040DAC.SQL_UPDATE_DEL_HED)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "UpdateDeleteHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
               , "E011" _
               , _
               , Me._Row.Item("RECORD_NO").ToString() _
               , "請求書番号" _
               , Me._Row.Item("SKYU_NO").ToString())
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(鑑明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑明細更新SQLの構築・発行</remarks>
    Private Function UpdateDeleteDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG040DAC.SQL_UPDATE_DEL_DTL)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetSkyuNo()
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "UpdateDeleteDtl", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
               , "E011" _
               , _
               , Me._Row.Item("RECORD_NO").ToString() _
               , "請求書番号" _
               , Me._Row.Item("SKYU_NO").ToString())

        End If

        Return ds

    End Function
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

#End Region

#Region "初期化処理"

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 初期化処理(確定済データの場合)(鑑ヘッダ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpdateStateKbHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG040DAC.SQL_UPDATE_CLEAR_STATEKB)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "UpdateStateKbHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
               , "E011" _
               , _
               , Me._Row.Item("RECORD_NO").ToString() _
               , "請求書番号" _
               , Me._Row.Item("SKYU_NO").ToString())
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(初期化対象データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectClearHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMG040DAC.SQL_SELECT_CLEAR_HED) 'SQL構築(データ抽出用Select句)
        Call Me.SetSkyuNo()                               '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectClearHed", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("STATE_KB", "STATE_KB")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_PIC", "SEIQTO_PIC")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("NEBIKI_RT1", "NEBIKI_RT1")
        map.Add("NEBIKI_GK1", "NEBIKI_GK1")
        map.Add("TAX_GK1", "TAX_GK1")
        map.Add("TAX_HASU_GK1", "TAX_HASU_GK1")
        map.Add("NEBIKI_RT2", "NEBIKI_RT2")
        map.Add("NEBIKI_GK2", "NEBIKI_GK2")
        map.Add("STORAGE_KB", "STORAGE_KB")
        map.Add("HANDLING_KB", "HANDLING_KB")
        map.Add("UNCHIN_KB", "UNCHIN_KB")
        map.Add("SAGYO_KB", "SAGYO_KB")
        map.Add("YOKOMOCHI_KB", "YOKOMOCHI_KB")
        map.Add("CRT_KB", "CRT_KB")
        map.Add("UNCHIN_IMP_FROM_DATE", "UNCHIN_IMP_FROM_DATE")
        map.Add("SAGYO_IMP_FROM_DATE", "SAGYO_IMP_FROM_DATE")
        map.Add("YOKOMOCHI_IMP_FROM_DATE", "YOKOMOCHI_IMP_FROM_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("SKYU_NO_RELATED", "SKYU_NO_RELATED")
        map.Add("RB_FLG", "RB_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG040HED")

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑明細検索処理(初期化対象データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑明細検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectClearDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMG040DAC.SQL_SELECT_CLEAR_DTL) 'SQL構築(データ抽出用Select句)
        Call Me.SetSkyuNo()                               '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectClearDtl", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_SUB_NO", "SKYU_SUB_NO")
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("MAKE_SYU_KB", "MAKE_SYU_KB")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_RTGK", "NEBIKI_RTGK")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG040DTL")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSkyuNo()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("SKYU_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", whereStr, DBDataType.CHAR))

        End With


    End Sub

    ''' <summary>
    ''' 請求鑑ヘッダ 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ新規登録SQLの構築・発行</remarks>
    Private Function InsertHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMG040DAC.SQL_INSERT_HED, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（共通項目）設定
            Call Me.SetParamCommonSystemIns()

            'SQLパラメータ（システム項目）設定
            Call Me.SetHedParameter()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG040DAC", "InsertHed", cmd)

            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHedParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", "00", DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_PIC", .Item("SEIQTO_PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", .Item("SEIQTO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT1", .Item("NEBIKI_RT1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK1", Convert.ToString(Convert.ToDecimal(.Item("NEBIKI_GK1").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_GK1", Convert.ToString(Convert.ToDecimal(.Item("TAX_GK1").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_HASU_GK1", Convert.ToString(Convert.ToDecimal(.Item("TAX_HASU_GK1").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT2", .Item("NEBIKI_RT2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK2", Convert.ToString(Convert.ToDecimal(.Item("NEBIKI_GK2").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_KB", .Item("STORAGE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_KB", .Item("HANDLING_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB", .Item("SAGYO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_KB", .Item("YOKOMOCHI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", .Item("CRT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_IMP_FROM_DATE", .Item("UNCHIN_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_IMP_FROM_DATE", .Item("SAGYO_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_IMP_FROM_DATE", .Item("YOKOMOCHI_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO_RELATED", String.Empty, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_FLG", "00", DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 請求鑑明細 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑明細新規登録SQLの構築・発行</remarks>
    Private Function InsertDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040DTL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMG040DAC.SQL_INSERT_DTL, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（共通項目）設定
            Call Me.SetParamCommonSystemIns()

            'SQLパラメータ（システム項目）設定
            Call Me.SetDtlParameter()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG040DAC", "InsertDtl", cmd)

            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑明細の登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDtlParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_SUB_NO", .Item("SKYU_SUB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAKE_SYU_KB", .Item("MAKE_SYU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEISAN_TLGK", Convert.ToString(Convert.ToDecimal(.Item("KEISAN_TLGK").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT", .Item("NEBIKI_RT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RTGK", Convert.ToString(Convert.ToDecimal(.Item("NEBIKI_RTGK").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK", Convert.ToString(Convert.ToDecimal(.Item("NEBIKI_GK").ToString()) * -1), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEKIYO", .Item("TEKIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEMPLATE_IMP_FLG", .Item("TEMPLATE_IMP_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

#End Region

#Region "言語取得"
    '20210628 ベトナム対応 add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function

    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '20210628 ベトナム対応 add End

#End Region

#Region "請求データ出力処理"

    ''' <summary>
    ''' 請求データ出力用データ抽出処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectCsvData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG040DAC.SQL_SELECT_CSV, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG040DAC", "SelectCsvData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("SKYU_GK", "SKYU_GK")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_RATE", "TAX_RATE")
        map.Add("SHIHARAI_DATE", "SHIHARAI_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG040CSVOUT")

        Return ds

    End Function

    ''' <summary>
    ''' 請求データ出力用フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateCsvData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG040IN")

        'データのループ
        For i As Integer = 0 To inTbl.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG040DAC.SQL_UPDATE_CSV, Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの設定
            Me._SqlPrmList = New ArrayList()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))

            Call Me.SetParamCommonSystemUpd()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG040DAC", "UpdateCsvData", cmd)

            'SQLの発行
            Call MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

#End Region

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

End Class
