' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB514    : 入荷チェックリスト印刷
'  作  成  者       :  Tsunehira
'  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB514DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB514DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	INL.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'02'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                        " & vbNewLine _
                            & "            BASE2.RPT_ID                 AS  RPT_ID              " & vbNewLine _
                            & "           ,BASE2.NRS_BR_CD              AS  NRS_BR_CD           " & vbNewLine _
                            & "           ,BASE2.INKA_NO_L              AS  INKA_NO_L           " & vbNewLine _
                            & "           ,BASE2.PRINT_SORT             AS  PRINT_SORT          " & vbNewLine _
                            & "           ,BASE2.INKA_NO_M              AS  INKA_NO_M           " & vbNewLine _
                            & "           ,BASE2.INKA_NO_S              AS  INKA_NO_S           " & vbNewLine _
                            & "           ,BASE2.USER_NM                AS  USER_NM             " & vbNewLine _
                            & "           ,BASE2.INKA_DATE              AS  INKA_DATE           " & vbNewLine _
                            & "           ,BASE2.CUST_CD_L              AS  CUST_CD_L           " & vbNewLine _
                            & "           ,BASE2.CUST_CD_M              AS  CUST_CD_M           " & vbNewLine _
                            & "           ,BASE2.CUST_NM_L              AS  CUST_NM_L           " & vbNewLine _
                            & "           ,BASE2.CUST_NM_M              AS  CUST_NM_M           " & vbNewLine _
                            & "           ,BASE2.OUTKA_FROM_ORD_NO_L    AS  OUTKA_FROM_ORD_NO_L " & vbNewLine _
                            & "           ,BASE2.BUYER_ORD_NO_L         AS  BUYER_ORD_NO_L      " & vbNewLine _
                            & "           ,BASE2.HOKAN_STR_DATE         AS  HOKAN_STR_DATE      " & vbNewLine _
                            & "           ,BASE2.NIYAKU_YN              AS  NIYAKU_YN           " & vbNewLine _
                            & "           ,BASE2.INKA_STATE_KB          AS  INKA_STATE_KB       " & vbNewLine _
                            & "           ,BASE2.INKA_TP                AS  INKA_TP             " & vbNewLine _
                            & "           ,BASE2.INKA_KB                AS  INKA_KB             " & vbNewLine _
                            & "           ,BASE2.INKA_TTL_NB            AS  INKA_TTL_NB         " & vbNewLine _
                            & "           ,BASE2.INKA_PLAN_QT           AS  INKA_PLAN_QT        " & vbNewLine _
                            & "           ,BASE2.INKA_PLAN_QT_UT        AS  INKA_PLAN_QT_UT     " & vbNewLine _
                            & "           ,BASE2.UKETSUKE_USER          AS  UKETSUKE_USER       " & vbNewLine _
                            & "           ,BASE2.UNCHIN_TP              AS  UNCHIN_TP           " & vbNewLine _
                            & "           ,BASE2.VCLE_KB                AS  VCLE_KB             " & vbNewLine _
                            & "           ,BASE2.UNSO_ONDO_KB           AS  UNSO_ONDO_KB        " & vbNewLine _
                            & "           ,BASE2.DEST_NM                AS  DEST_NM             " & vbNewLine _
                            & "           ,BASE2.UNSOCO_NM              AS  UNSOCO_NM           " & vbNewLine _
                            & "           ,BASE2.TARIFF_CD              AS  TARIFF_CD           " & vbNewLine _
                            & "           ,BASE2.TARIFF_REM             AS  TARIFF_REM          " & vbNewLine _
                            & "           ,BASE2.YOKO_REM               AS  YOKO_REM            " & vbNewLine _
                            & "           ,BASE2.PAY_UNCHIN             AS  PAY_UNCHIN          " & vbNewLine _
                            & "           ,BASE2.REMARK_HED             AS  REMARK_HED          " & vbNewLine _
                            & "           ,BASE2.CRT_USER               AS  CRT_USER            " & vbNewLine _
                            & "           ,BASE2.NRS_BR_NM              AS  NRS_BR_NM           " & vbNewLine _
                            & "           ,BASE2.WH_NM                  AS  WH_NM               " & vbNewLine _
                            & "           ,BASE2.GOODS_CD_CUST          AS  GOODS_CD_CUST        " & vbNewLine _
                            & "           ,BASE2.GOODS_NM               AS  GOODS_NM            " & vbNewLine _
                            & "           ,BASE2.ONDO_KB                AS  ONDO_KB             " & vbNewLine _
                            & "           ,BASE2.REMARK_MEI             AS  REMARK_MEI          " & vbNewLine _
                            & "           ,BASE2.LOT_NO                 AS  LOT_NO              " & vbNewLine _
                            & "           ,BASE2.IRIME                  AS  IRIME               " & vbNewLine _
                            & "           ,BASE2.IRIME_UT               AS  IRIME_UT            " & vbNewLine _
                            & "           ,BASE2.KONSU                  AS  KONSU               " & vbNewLine _
                            & "           ,BASE2.HASU                   AS  HASU                " & vbNewLine _
                            & "           ,BASE2.PKG_NB                 AS  PKG_NB              " & vbNewLine _
                            & "           ,BASE2.PKG_UT                 AS  PKG_UT              " & vbNewLine _
                            & "           ,BASE2.GOODS_CRT_DATE         AS  GOODS_CRT_DATE      " & vbNewLine _
                            & "           ,BASE2.LT_DATE                AS  LT_DATE             " & vbNewLine _
                            & "           ,BASE2.SERIAL_NO              AS  SERIAL_NO           " & vbNewLine _
                            & "           ,BASE2.DEST_CD                AS  DEST_CD             " & vbNewLine _
                            & "           ,BASE2.TOU_NO                 AS  TOU_NO              " & vbNewLine _
                            & "           ,BASE2.SITU_NO                AS  SITU_NO             " & vbNewLine _
                            & "           ,BASE2.ZONE_CD                AS  ZONE_CD             " & vbNewLine _
                            & "           ,BASE2.LOCA                   AS  LOCA                " & vbNewLine _
                            & "           ,BASE2.REMARK_OUT             AS  REMARK_OUT          " & vbNewLine _
                            & "           ,BASE2.REMARK_SYO             AS  REMARK_SYO          " & vbNewLine _
                            & "           ,BASE2.ALLOC_PRIORITY         AS  ALLOC_PRIORITY      " & vbNewLine _
                            & "           ,BASE2.GOODS_COND_KB_1        AS  GOODS_COND_KB_1     " & vbNewLine _
                            & "           ,BASE2.GOODS_COND_KB_2        AS  GOODS_COND_KB_2     " & vbNewLine _
                            & "           ,BASE2.GOODS_COND_KB_3        AS  GOODS_COND_KB_3     " & vbNewLine _
                            & "           ,BASE2.SPD_KB                 AS  SPD_KB              " & vbNewLine _
                            & "           ,BASE2.OFB_KB                 AS  OFB_KB              " & vbNewLine _
                            & "           ,BASE2.SEARCH_KEY_2           AS  SEARCH_KEY_2        " & vbNewLine _
                            & "           ,ML01.SAGYO_RYAK              AS  SAGYO_RYAK_HED_1    " & vbNewLine _
                            & "           ,ML02.SAGYO_RYAK              AS  SAGYO_RYAK_HED_2    " & vbNewLine _
                            & "           ,ML03.SAGYO_RYAK              AS  SAGYO_RYAK_HED_3    " & vbNewLine _
                            & "           ,ML04.SAGYO_RYAK              AS  SAGYO_RYAK_HED_4    " & vbNewLine _
                            & "           ,ML05.SAGYO_RYAK              AS  SAGYO_RYAK_HED_5    " & vbNewLine _
                            & "           ,MM01.SAGYO_RYAK              AS  SAGYO_RYAK_MEI_1    " & vbNewLine _
                            & "           ,MM02.SAGYO_RYAK              AS  SAGYO_RYAK_MEI_2    " & vbNewLine _
                            & "           ,MM03.SAGYO_RYAK              AS  SAGYO_RYAK_MEI_3    " & vbNewLine _
                            & "           ,MM04.SAGYO_RYAK              AS  SAGYO_RYAK_MEI_4    " & vbNewLine _
                            & "           ,MM05.SAGYO_RYAK              AS  SAGYO_RYAK_MEI_5    " & vbNewLine _
                            & "           ,BASE2.TAX_KB                 AS  TAX_KB              " & vbNewLine _
                            & "           ,BASE2.LOT_SP_KB              AS  LOT_SP_KB           " & vbNewLine _
                            & "--20120117 SHINOHARA ADD START                                   " & vbNewLine _
                            & "           ,BASE2.OUTKA_FROM_ORD_NO_M    AS  OUTKA_FROM_ORD_NO_M " & vbNewLine _
                            & "           ,BASE2.CUST_COST_CD2          AS  CUST_COST_CD2       " & vbNewLine _
                            & "--20120117 SHINOHARA ADD END                                     " & vbNewLine _
                            & "           --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 -- START --     " & vbNewLine _
                            & "           ,BASE2.UTI_UN                 AS  UTI_UN              " & vbNewLine _
                            & "           ,BASE2.UTI_NM                 AS  UTI_NM              " & vbNewLine _
                            & "           ,BASE2.UTI_IM                 AS  UTI_IM              " & vbNewLine _
                            & "           ,BASE2.UTI_PG                 AS  UTI_PG              " & vbNewLine _
                            & "           ,BASE2.UTI_FP                 AS  UTI_FP              " & vbNewLine _
                            & "           ,BASE2.UTI_EL                 AS  UTI_EL              " & vbNewLine _
                            & "           --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 --  END  --     " & vbNewLine _
                            & "           --(2015.10.28)tsunehira add --  Start  --             " & vbNewLine _
                            & "           ,BASE2.SEARCH_KEY_1 AS  SEARCH_KEY_1                  " & vbNewLine _
                            & "           ,BASE2.GOODS_NM_2 AS GOODS_NM_2                       " & vbNewLine _
                            & "           --(2015.10.28)tsunehira add --  END  --               " & vbNewLine _
                            & "           ,(SELECT                                                                              " & vbNewLine _
                            & "              SUM((S.KONSU * G.PKG_NB + S.HASU) * S.BETU_WT)                                     " & vbNewLine _
                            & "             FROM                                                                                " & vbNewLine _
                            & "              $LM_TRN$..B_INKA_S S                                                               " & vbNewLine _
                            & "             LEFT JOIN $LM_MST$..M_GOODS G                                                       " & vbNewLine _
                            & "             ON  G.NRS_BR_CD = BASE2.NRS_BR_CD                                                   " & vbNewLine _
                            & "             AND G.GOODS_CD_NRS = BASE2.GOODS_CD_NRS                                             " & vbNewLine _
                            & "             WHERE                                                                               " & vbNewLine _
                            & "                 S.NRS_BR_CD = BASE2.NRS_BR_CD                                                   " & vbNewLine _
                            & "             AND S.INKA_NO_L = BASE2.INKA_NO_L                                                   " & vbNewLine _
                            & "             AND S.INKA_NO_M = BASE2.INKA_NO_M                                                   " & vbNewLine _
                            & "             AND S.SYS_DEL_FLG = '0')                   AS WT                                    " & vbNewLine _
                            & "--(2013.04.26)要望番号2033 -- START --                                                           " & vbNewLine _
                            & "           ,BASE2.CUST_NM_S                             AS CUST_NM_S                             " & vbNewLine _
                            & "--(2013.04.26)要望番号2033 -- END   --                                                           " & vbNewLine _
                            & "          --(2016.01.28)LMB515対応START                                                          " & vbNewLine _
                            & "           ,BASE2.NB_UT                                 AS NB_UT                                 " & vbNewLine _
                            & "          --(2016.01.28)LMB515対応END                                                            " & vbNewLine _
                            & "          --20160607 シンガポール対応 tsunehira add                                              " & vbNewLine _
                            & "           ,(SELECT                                                                              " & vbNewLine _
                            & "                CEILING(SUM(S.KONSU + S.HASU / G.PKG_NB ))                                       " & vbNewLine _
                            & "             FROM $LM_TRN$..B_INKA_S S                                                           " & vbNewLine _
                            & "             LEFT JOIN $LM_TRN$..B_INKA_M M                                                      " & vbNewLine _
                            & "               ON M.NRS_BR_CD   = S.NRS_BR_CD                                                    " & vbNewLine _
                            & "              AND M.INKA_NO_L   = S.INKA_NO_L                                                    " & vbNewLine _
                            & "              AND M.INKA_NO_M   = S.INKA_NO_M                                                    " & vbNewLine _
                            & "              AND M.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                            & "                                                                                                 " & vbNewLine _
                            & "             LEFT JOIN $LM_MST$..M_GOODS G                                                       " & vbNewLine _
                            & "               ON G.NRS_BR_CD    = M.NRS_BR_CD                                                   " & vbNewLine _
                            & "              AND G.GOODS_CD_NRS = M.GOODS_CD_NRS                                                " & vbNewLine _
                            & "              AND G.SYS_DEL_FLG  = '0'                                                           " & vbNewLine _
                            & "                                                                                                 " & vbNewLine _
                            & "             WHERE                                                                               " & vbNewLine _
                            & "                  S.NRS_BR_CD   = BASE2.NRS_BR_CD                                                " & vbNewLine _
                            & "              AND S.INKA_NO_L   = BASE2.INKA_NO_L                                                " & vbNewLine _
                            & "              --AND S.INKA_NO_M   = BASE2.INKA_NO_M                                              " & vbNewLine _
                            & "              AND S.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                            & "                                                                                                 " & vbNewLine _
                            & "             GROUP BY                                                                            " & vbNewLine _
                            & "                  S.INKA_NO_L)                   AS KONSU_SHOSU                                  " & vbNewLine _
                            & "          --20160607 シンガポール対応 tsunehira end                                              " & vbNewLine _
                            & " FROM                                                                                            " & vbNewLine _
                            & "  (                                                                                              " & vbNewLine _
                            & " SELECT                                                                                          " & vbNewLine _
                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                " & vbNewLine _
                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                " & vbNewLine _
                            & "    ELSE MR3.RPT_ID END                                AS RPT_ID                                 " & vbNewLine _
                            & ",CASE WHEN INL.UNCHIN_KB = '10' AND CUST.UNTIN_CALCULATION_KB = '01' THEN                        " & vbNewLine _
                            & "      ISNULL((SELECT                                                                             " & vbNewLine _
                            & "               BASE.UNCHIN_TARIFF_REM                                                            " & vbNewLine _
                            & "              FROM                                                                               " & vbNewLine _
                            & "              (SELECT                                                                            " & vbNewLine _
                            & "                UNCHIN_TARIFF_REM AS UNCHIN_TARIFF_REM                                           " & vbNewLine _
                            & "               ,ROW_NUMBER() OVER (PARTITION BY UNCHIN_TARIFF_CD ORDER BY STR_DATE DESC) AS NUM  " & vbNewLine _
                            & "                FROM $LM_MST$..M_UNCHIN_TARIFF UT                                                " & vbNewLine _
                            & "                WHERE                                                                            " & vbNewLine _
                            & "                    UT.UNCHIN_TARIFF_CD = UL.SEIQ_TARIFF_CD                                      " & vbNewLine _
                            & "                AND UT.UNCHIN_TARIFF_CD_EDA = '000'                                              " & vbNewLine _
                            & "                AND UT.STR_DATE <= UL.OUTKA_PLAN_DATE                                            " & vbNewLine _
                            & "                AND UT.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                            & "               ) AS BASE                                                                         " & vbNewLine _
                            & "           WHERE BASE.NUM = 1),'')                                                               " & vbNewLine _
                            & "      WHEN INL.UNCHIN_KB = '10' AND CUST.UNTIN_CALCULATION_KB = '02' THEN                        " & vbNewLine _
                            & "      ISNULL((SELECT                                                                             " & vbNewLine _
                            & "               BASE.UNCHIN_TARIFF_REM                                                            " & vbNewLine _
                            & "              FROM                                                                               " & vbNewLine _
                            & "              (SELECT                                                                            " & vbNewLine _
                            & "                UNCHIN_TARIFF_REM AS UNCHIN_TARIFF_REM                                           " & vbNewLine _
                            & "               ,ROW_NUMBER() OVER (PARTITION BY UNCHIN_TARIFF_CD ORDER BY STR_DATE DESC) AS NUM  " & vbNewLine _
                            & "                FROM $LM_MST$..M_UNCHIN_TARIFF UT                                                  " & vbNewLine _
                            & "                WHERE                                                                            " & vbNewLine _
                            & "                    UT.UNCHIN_TARIFF_CD = UL.SEIQ_TARIFF_CD                                      " & vbNewLine _
                            & "                AND UT.UNCHIN_TARIFF_CD_EDA = '000'                                              " & vbNewLine _
                            & "                AND UT.STR_DATE <= UL.ARR_PLAN_DATE                                              " & vbNewLine _
                            & "                AND UT.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                            & "               ) AS BASE                                                                         " & vbNewLine _
                            & "           WHERE BASE.NUM = 1),'')                                                               " & vbNewLine _
                            & "      WHEN INL.UNCHIN_KB = '20' THEN YTH.YOKO_REM                                                " & vbNewLine _
                            & "      ELSE '' END                               AS TARIFF_REM                                    " & vbNewLine _
                            & ",INL.NRS_BR_CD                                  AS NRS_BR_CD           " & vbNewLine _
                            & ",INL.INKA_NO_L                                  AS INKA_NO_L           " & vbNewLine _
                            & ",INM.PRINT_SORT                                 AS PRINT_SORT          " & vbNewLine _
                            & ",INM.INKA_NO_M                                  AS INKA_NO_M           " & vbNewLine _
                            & ",INS.INKA_NO_S                                  AS INKA_NO_S           " & vbNewLine _
                            & "--ここから追記 BY SHINOHARA                                            " & vbNewLine _
                            & "          ,USR_PRT.USER_NM                      AS USER_NM             " & vbNewLine _
                            & "          ,INL.INKA_DATE                        AS INKA_DATE           " & vbNewLine _
                            & "          ,INL.CUST_CD_L                        AS CUST_CD_L           " & vbNewLine _
                            & "          ,INL.CUST_CD_M                        AS CUST_CD_M           " & vbNewLine _
                            & "          ,CUST.CUST_NM_L                       AS CUST_NM_L           " & vbNewLine _
                            & "          ,CUST.CUST_NM_M                       AS CUST_NM_M           " & vbNewLine _
                            & "          ,INL.OUTKA_FROM_ORD_NO_L              AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
                            & "          ,INL.BUYER_ORD_NO_L                   AS BUYER_ORD_NO_L      " & vbNewLine _
                            & "          ,INL.HOKAN_STR_DATE                   AS HOKAN_STR_DATE      " & vbNewLine _
                            & "          ,KBN_01.KBN_NM1                       AS NIYAKU_YN           " & vbNewLine _
                            & "          ,KBN_02.KBN_NM1                       AS INKA_STATE_KB       " & vbNewLine _
                            & "          ,KBN_03.KBN_NM1                       AS INKA_TP             " & vbNewLine _
                            & "          ,KBN_04.KBN_NM1                       AS INKA_KB             " & vbNewLine _
                            & "          ,INL.INKA_TTL_NB                      AS INKA_TTL_NB         " & vbNewLine _
                            & "          ,INL.INKA_PLAN_QT                     AS INKA_PLAN_QT        " & vbNewLine _
                            & "          ,INL.INKA_PLAN_QT_UT                  AS INKA_PLAN_QT_UT     " & vbNewLine _
                            & "          ,USR.USER_NM                          AS UKETSUKE_USER       " & vbNewLine _
                            & "          ,KBN_05.KBN_NM1                       AS UNCHIN_TP           " & vbNewLine _
                            & "          ,KBN_06.KBN_NM1                       AS VCLE_KB             " & vbNewLine _
                            & "          ,KBN_07.KBN_NM1                       AS UNSO_ONDO_KB        " & vbNewLine _
                            & "          ,CASE WHEN EDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM             " & vbNewLine _
                            & "           ELSE DESTL.DEST_NM                                          " & vbNewLine _
                            & "           END                                  AS DEST_NM             " & vbNewLine _
                            & "          ,USC.UNSOCO_NM                        AS UNSOCO_NM           " & vbNewLine _
                            & "          ,UL.SEIQ_TARIFF_CD                    AS TARIFF_CD           " & vbNewLine _
                            & "          ,YTH.YOKO_REM                         AS YOKO_REM            " & vbNewLine _
                            & "          ,(SELECT                                                     " & vbNewLine _
                            & "             SUM(SEIQ_UNCHIN)                                          " & vbNewLine _
                            & "            FROM                                                       " & vbNewLine _
                            & "            $LM_TRN$..F_UNCHIN_TRS UN                                  " & vbNewLine _
                            & "            WHERE                                                      " & vbNewLine _
                            & "                UN.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                            & "            AND UN.UNSO_NO_L = UL.UNSO_NO_L)    AS PAY_UNCHIN          " & vbNewLine _
                            & "          ,INL.REMARK                           AS REMARK_HED          " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + '000') AND    " & vbNewLine _
                            & "                      E.IOZS_KB='10' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 1),'')              AS SAGYO_CD_L01        " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + '000') AND    " & vbNewLine _
                            & "                      E.IOZS_KB='10' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 2),'')              AS SAGYO_CD_L02        " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + '000') AND    " & vbNewLine _
                            & "                      E.IOZS_KB='10' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 3),'')              AS SAGYO_CD_L03        " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + '000') AND    " & vbNewLine _
                            & "                      E.IOZS_KB='10' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 4),'')              AS SAGYO_CD_L04        " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + '000') AND    " & vbNewLine _
                            & "                      E.IOZS_KB='10' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 5),'')              AS SAGYO_CD_L05        " & vbNewLine _
                            & "          ,USR_PRT.USER_NM                      AS CRT_USER            " & vbNewLine _
                            & "          ,MNB.NRS_BR_NM                        AS NRS_BR_NM           " & vbNewLine _
                            & "          ,SOKO.WH_NM                           AS WH_NM               " & vbNewLine _
                            & "          ,INM.GOODS_CD_NRS                     AS GOODS_CD_NRS        " & vbNewLine _
                            & "          ,MG.GOODS_CD_CUST                     AS GOODS_CD_CUST       " & vbNewLine _
                            & "          ,MG.GOODS_NM_1                        AS GOODS_NM            " & vbNewLine _
                            & "          --20151028 tsunehira add start                               " & vbNewLine _
                            & "          ,MG.SEARCH_KEY_1                      AS SEARCH_KEY_1        " & vbNewLine _
                            & "          ,MG.GOODS_NM_2                        AS GOODS_NM_2          " & vbNewLine _
                            & "          --20151028 tsunehira add End                                 " & vbNewLine _
                            & "          ,KBN_08.KBN_NM1                       AS ONDO_KB             " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + INM.INKA_NO_M) AND " & vbNewLine _
                            & "                      E.IOZS_KB='11' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 1),'') AS SAGYO_CD_M01                     " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + INM.INKA_NO_M) AND  " & vbNewLine _
                            & "                      E.IOZS_KB='11' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 2),'') AS SAGYO_CD_M02                     " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + INM.INKA_NO_M) AND   " & vbNewLine _
                            & "                      E.IOZS_KB='11' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 3),'') AS SAGYO_CD_M03                     " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + INM.INKA_NO_M) AND " & vbNewLine _
                            & "                      E.IOZS_KB='11' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 4),'') AS SAGYO_CD_M04                     " & vbNewLine _
                            & "          ,ISNULL((SELECT BASE.SAGYO_CD                                " & vbNewLine _
                            & "           FROM                                                        " & vbNewLine _
                            & "               (SELECT                                                 " & vbNewLine _
                            & "              SAGYO_CD AS SAGYO_CD,ROW_NUMBER() OVER                   " & vbNewLine _
                            & "                 (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)   " & vbNewLine _
                            & "                 AS  NUM                                               " & vbNewLine _
                            & "                FROM $LM_TRN$..E_SAGYO E                               " & vbNewLine _
                            & "                WHERE E.NRS_BR_CD = INL.NRS_BR_CD AND                  " & vbNewLine _
                            & "                      E.INOUTKA_NO_LM = (INM.INKA_NO_L + INM.INKA_NO_M) AND  " & vbNewLine _
                            & "                      E.IOZS_KB='11' AND                               " & vbNewLine _
                            & "                      E.SYS_DEL_FLG = '0'                              " & vbNewLine _
                            & "               ) AS BASE                                               " & vbNewLine _
                            & "           WHERE BASE.NUM = 5),'') AS SAGYO_CD_M05                     " & vbNewLine _
                            & "          ,INM.REMARK                           AS REMARK_MEI          " & vbNewLine _
                            & "          ,INS.LOT_NO                           AS LOT_NO              " & vbNewLine _
                            & "          ,INS.IRIME                            AS IRIME               " & vbNewLine _
                            & "          ,MG.STD_IRIME_UT                      AS IRIME_UT            " & vbNewLine _
                            & "          ,INS.KONSU                            AS KONSU               " & vbNewLine _
                            & "          ,INS.HASU                             AS HASU                " & vbNewLine _
                            & "          ,MG.PKG_NB                            AS PKG_NB              " & vbNewLine _
                            & "          ,CASE WHEN MG.PKG_NB > 1 THEN MG.PKG_UT                      " & vbNewLine _
                            & "                ELSE MG.NB_UT                                          " & vbNewLine _
                            & "           END                                  AS PKG_UT              " & vbNewLine _
                            & "          ,INS.GOODS_CRT_DATE                   AS GOODS_CRT_DATE      " & vbNewLine _
                            & "          ,INS.LT_DATE                          AS LT_DATE             " & vbNewLine _
                            & "          ,INS.SERIAL_NO                        AS SERIAL_NO           " & vbNewLine _
                            & "          ,INS.DEST_CD                          AS DEST_CD             " & vbNewLine _
                            & "          ,INS.TOU_NO                           AS TOU_NO              " & vbNewLine _
                            & "          ,INS.SITU_NO                          AS SITU_NO             " & vbNewLine _
                            & "          ,INS.ZONE_CD                          AS ZONE_CD             " & vbNewLine _
                            & "          ,INS.LOCA                             AS LOCA                " & vbNewLine _
                            & "          ,INS.REMARK_OUT                       AS REMARK_OUT          " & vbNewLine _
                            & "          ,INS.REMARK                           AS REMARK_SYO          " & vbNewLine _
                            & "          ,CASE WHEN KBN_10.KBN_NM11 <> '' THEN KBN_10.KBN_NM11 ELSE KBN_10.KBN_NM1 END AS ALLOC_PRIORITY      " & vbNewLine _
                            & "          ,CASE WHEN KBN_11.KBN_NM11 <> '' THEN KBN_11.KBN_NM11 ELSE KBN_11.KBN_NM1 END AS GOODS_COND_KB_1     " & vbNewLine _
                            & "          ,CASE WHEN KBN_12.KBN_NM11 <> '' THEN KBN_12.KBN_NM11 ELSE KBN_12.KBN_NM1 END AS GOODS_COND_KB_2     " & vbNewLine _
                            & "          ,MCC.JOTAI_NM                         AS GOODS_COND_KB_3     " & vbNewLine _
                            & "          ,CASE WHEN KBN_14.KBN_NM11 <> '' THEN KBN_14.KBN_NM11 ELSE KBN_14.KBN_NM1 END AS SPD_KB              " & vbNewLine _
                            & "          ,CASE WHEN KBN_13.KBN_NM11 <> '' THEN KBN_13.KBN_NM11 ELSE KBN_13.KBN_NM1 END AS OFB_KB              " & vbNewLine _
                            & "          ,MG.SEARCH_KEY_2                      AS SEARCH_KEY_2        " & vbNewLine _
                            & "          ,KBN_15.KBN_NM1                       AS TAX_KB              " & vbNewLine _
                            & "          ,CASE WHEN CUST_D.SET_NAIYO IS NOT NULL THEN CUST_D.SET_NAIYO " & vbNewLine _
                            & "         ELSE             								              " & vbNewLine _
                            & "           ''		            					                  " & vbNewLine _
                            & "          END                                  AS LOT_SP_KB	          " & vbNewLine _
                            & "--20120117 SHINOHARA ADD START                              	          " & vbNewLine _
                            & "          ,INM.OUTKA_FROM_ORD_NO_M             AS OUTKA_FROM_ORD_NO_M  " & vbNewLine _
                            & "          ,MG.CUST_COST_CD2                    AS CUST_COST_CD2        " & vbNewLine _
                            & "--20120117 SHINOHARA ADD END                                	          " & vbNewLine _
                            & "          --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 -- START --            " & vbNewLine _
                            & "          ,MGD_UN.SET_NAIYO                     AS UTI_UN              " & vbNewLine _
                            & "          ,MGD_NM.SET_NAIYO                     AS UTI_NM              " & vbNewLine _
                            & "          ,MGD_IM.SET_NAIYO                     AS UTI_IM              " & vbNewLine _
                            & "          ,MGD_PG.SET_NAIYO                     AS UTI_PG              " & vbNewLine _
                            & "          ,MGD_FP.SET_NAIYO                     AS UTI_FP              " & vbNewLine _
                            & "          ,MGD_EL.SET_NAIYO                     AS UTI_EL              " & vbNewLine _
                            & "          --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 --  END  --            " & vbNewLine _
                            & "          --(2013.04.26)要望番号2033 -- START --                       " & vbNewLine _
                            & "          ,CUST2.CUST_NM_S                      AS CUST_NM_S           " & vbNewLine _
                            & "          --(2013.04.26)要望番号2033 -- END   --                       " & vbNewLine _
                            & "          --(2016.01.28)LMB515対応START                                " & vbNewLine _
                            & "           ,MG.NB_UT                            AS NB_UT               " & vbNewLine _
                            & "          --(2016.01.28)LMB515対応END                                  " & vbNewLine _

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                          " & vbNewLine _
                            & "--入荷L                                                                " & vbNewLine _
                            & "$LM_TRN$..B_INKA_L INL                                                 " & vbNewLine _
                            & "--入荷M                                                                " & vbNewLine _
                            & "LEFT JOIN $LM_TRN$..B_INKA_M INM                                       " & vbNewLine _
                            & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                                      " & vbNewLine _
                            & "AND  INL.INKA_NO_L = INM.INKA_NO_L                                     " & vbNewLine _
                            & "AND INM.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                            & "--入荷S                                                                " & vbNewLine _
                            & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                       " & vbNewLine _
                            & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                                      " & vbNewLine _
                            & "AND  INM.INKA_NO_L = INS.INKA_NO_L                                     " & vbNewLine _
                            & "AND INM.INKA_NO_M = INS.INKA_NO_M                                      " & vbNewLine _
                            & "AND INS.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                            & "--入荷EDIL                                                             " & vbNewLine _
                            & "LEFT JOIN                                                              " & vbNewLine _
                            & "(                                                                      " & vbNewLine _
                            & "  SELECT                                                               " & vbNewLine _
                            & "   NRS_BR_CD                                                           " & vbNewLine _
                            & "  ,INKA_CTL_NO_L                                                       " & vbNewLine _
                            & "  ,CUST_CD_L                                                           " & vbNewLine _
                            & "  ,OUTKA_MOTO                                                          " & vbNewLine _
                            & "  ,SYS_DEL_FLG                                                         " & vbNewLine _
                            & "  FROM                                                                 " & vbNewLine _
                            & "  $LM_TRN$..H_INKAEDI_L                                                " & vbNewLine _
                            & "  GROUP BY                                                             " & vbNewLine _
                            & "   NRS_BR_CD                                                           " & vbNewLine _
                            & "  ,INKA_CTL_NO_L                                                       " & vbNewLine _
                            & "  ,CUST_CD_L                                                           " & vbNewLine _
                            & "  ,OUTKA_MOTO                                                          " & vbNewLine _
                            & "  ,SYS_DEL_FLG                                                         " & vbNewLine _
                            & "  ) EDIL                                                               " & vbNewLine _
                            & "  ON  EDIL.NRS_BR_CD = INL.NRS_BR_CD                                   " & vbNewLine _
                            & "  AND EDIL.INKA_CTL_NO_L = INL.INKA_NO_L                               " & vbNewLine _
                            & "  AND EDIL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                            & "--商品                                                                 " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_GOODS MG                                         " & vbNewLine _
                            & "ON  INM.NRS_BR_CD = MG.NRS_BR_CD                                       " & vbNewLine _
                            & "AND  INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                " & vbNewLine _
                            & "--運送L                                                                " & vbNewLine _
                            & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                        " & vbNewLine _
                            & "ON  INL.NRS_BR_CD = UL.NRS_BR_CD                                       " & vbNewLine _
                            & "AND  INL.INKA_NO_L = UL.INOUTKA_NO_L                                   " & vbNewLine _
                            & "AND  UL.MOTO_DATA_KB = '10'                                            " & vbNewLine _
                            & "AND UL.SYS_DEL_FLG  = '0'                                              " & vbNewLine _
                            & "--運送LL                                                               " & vbNewLine _
                            & "LEFT JOIN $LM_TRN$..F_UNSO_LL ULL                                      " & vbNewLine _
                            & "ON  UL.TRIP_NO = ULL.TRIP_NO                                           " & vbNewLine _
                            & "AND ULL.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                            & "--入荷Lでの荷主帳票パターン取得                                        " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                    " & vbNewLine _
                            & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                            & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                                     " & vbNewLine _
                            & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                                     " & vbNewLine _
                            & "AND MCR1.CUST_CD_S = '00'                                              " & vbNewLine _
                            & "AND MCR1.PTN_ID = '02'                                                 " & vbNewLine _
                            & "--帳票パターン取得                                                     " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_RPT MR1                                          " & vbNewLine _
                            & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                            & "AND MR1.PTN_ID = MCR1.PTN_ID                                           " & vbNewLine _
                            & "AND MR1.PTN_CD = MCR1.PTN_CD                                           " & vbNewLine _
                            & "AND MR1.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                            & "--商品Mの荷主での荷主帳票パターン取得                                  " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                    " & vbNewLine _
                            & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                      " & vbNewLine _
                            & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                      " & vbNewLine _
                            & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                      " & vbNewLine _
                            & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                      " & vbNewLine _
                            & "AND MCR2.PTN_ID = '02'                                                 " & vbNewLine _
                            & "--帳票パターン取得                                                     " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_RPT MR2                                          " & vbNewLine _
                            & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                     " & vbNewLine _
                            & "AND MR2.PTN_ID = MCR2.PTN_ID                                           " & vbNewLine _
                            & "AND MR2.PTN_CD = MCR2.PTN_CD                                           " & vbNewLine _
                            & "AND MR2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                            & "--存在しない場合の帳票パターン取得                                     " & vbNewLine _
                            & "LEFT JOIN $LM_MST$..M_RPT MR3                                          " & vbNewLine _
                            & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                                      " & vbNewLine _
                            & "AND MR3.PTN_ID = '02'                                                  " & vbNewLine _
                            & "AND MR3.STANDARD_FLAG = '01'                                           " & vbNewLine _
                            & "AND MR3.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                            & "   --日陸営業所マスタ                                                  " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_NRS_BR MNB                                    " & vbNewLine _
                            & "   ON  MNB.NRS_BR_CD     = INL.NRS_BR_CD                               " & vbNewLine _
                            & "   --倉庫マスタ                                                        " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_SOKO SOKO                                     " & vbNewLine _
                            & "   ON  SOKO.NRS_BR_CD    = INL.NRS_BR_CD                               " & vbNewLine _
                            & "   AND SOKO.WH_CD        = INL.WH_CD                                   " & vbNewLine _
                            & "   --荷主マスタ                                                        " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_CUST CUST                                     " & vbNewLine _
                            & "   ON  CUST.NRS_BR_CD    = INL.NRS_BR_CD                               " & vbNewLine _
                            & "   AND CUST.CUST_CD_L    = INL.CUST_CD_L                               " & vbNewLine _
                            & "   AND CUST.CUST_CD_M    = INL.CUST_CD_M                               " & vbNewLine _
                            & "   AND CUST.CUST_CD_S    = '00'                                        " & vbNewLine _
                            & "   AND CUST.CUST_CD_SS   = '00'                                        " & vbNewLine _
                            & "   --届先マスタ(出荷元取得入荷L参照)                                   " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_DEST DESTL                                    " & vbNewLine _
                            & "   ON  DESTL.NRS_BR_CD    = INL.NRS_BR_CD                              " & vbNewLine _
                            & "   AND DESTL.CUST_CD_L    = INL.CUST_CD_L                              " & vbNewLine _
                            & "   AND DESTL.DEST_CD      = UL.ORIG_CD                                 " & vbNewLine _
                            & "   --届先マスタ(出荷元取得EDI参照)                                     " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_DEST DESTEDI                                  " & vbNewLine _
                            & "   ON  DESTEDI.NRS_BR_CD    = EDIL.NRS_BR_CD                           " & vbNewLine _
                            & "   AND DESTEDI.CUST_CD_L    = EDIL.CUST_CD_L                           " & vbNewLine _
                            & "   AND DESTEDI.DEST_CD      = EDIL.OUTKA_MOTO                          " & vbNewLine _
                            & "   --荷主別商品状態区分マスタ                                          " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_CUSTCOND MCC                                  " & vbNewLine _
                            & "   ON  MCC.NRS_BR_CD    = INL.NRS_BR_CD                                " & vbNewLine _
                            & "   AND MCC.CUST_CD_L    = INL.CUST_CD_L                                " & vbNewLine _
                            & "   AND MCC.JOTAI_CD     = INS.GOODS_COND_KB_3                          " & vbNewLine _
                            & "   --ユーザーマスタ(印刷)                                              " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..S_USER USR_PRT                                  " & vbNewLine _
                            & "   ON  USR_PRT.USER_CD      = INL.CHECKLIST_PRT_USER                   " & vbNewLine _
                            & "   --ユーザーマスタ                                                    " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..S_USER USR                                      " & vbNewLine _
                            & "   ON  USR.USER_CD      = INL.CHECKLIST_PRT_USER                       " & vbNewLine _
                            & "   --運送会社マスタ                                                    " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_UNSOCO USC                                    " & vbNewLine _
                            & "   ON  USC.NRS_BR_CD      = UL.NRS_BR_CD                               " & vbNewLine _
                            & "   AND USC.UNSOCO_CD      = UL.UNSO_CD                                 " & vbNewLine _
                            & "   AND USC.UNSOCO_BR_CD   = UL.UNSO_BR_CD                              " & vbNewLine _
                            & "   --横持タリフヘッダ                                                  " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD YTH                            " & vbNewLine _
                            & "   ON  YTH.NRS_BR_CD = UL.NRS_BR_CD                                    " & vbNewLine _
                            & "   AND UL.SEIQ_TARIFF_CD = YTH.YOKO_TARIFF_CD                          " & vbNewLine _
                            & "   --荷役料有無(区分01)                                                " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_01                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_01.KBN_GROUP_CD = 'U009'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_01.KBN_CD      = INL.NIYAKU_YN                                 " & vbNewLine _
                            & "   --入荷進捗区分(区分02)                                              " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_02                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_02.KBN_GROUP_CD = 'N004'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_02.KBN_CD      = INL.INKA_STATE_KB                             " & vbNewLine _
                            & "   --入荷種別(区分03)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_03                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_03.KBN_GROUP_CD = 'N007'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_03.KBN_CD      = INL.INKA_TP                                   " & vbNewLine _
                            & "   --入荷区分(区分04)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_04                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_04.KBN_GROUP_CD = 'N006'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_04.KBN_CD      = INL.INKA_KB                                   " & vbNewLine _
                            & "   --運賃種別(区分05)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_05                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_05.KBN_GROUP_CD = 'U005'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_05.KBN_CD      = INL.UNCHIN_TP                                 " & vbNewLine _
                            & "   --車種(区分06)                                                      " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_06                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_06.KBN_GROUP_CD = 'S012'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_06.KBN_CD      = UL.VCLE_KB                                    " & vbNewLine _
                            & "   --運送温度(区分07)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_07                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_07.KBN_GROUP_CD = 'U006'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_07.KBN_CD      = UL.UNSO_ONDO_KB                               " & vbNewLine _
                            & "   --温度管理区分(区分08)                                              " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_08                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_08.KBN_GROUP_CD = 'O002'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_08.KBN_CD      = MG.ONDO_KB                                    " & vbNewLine _
                            & "   --入目単位(区分09)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_09                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_09.KBN_GROUP_CD = 'I001'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_09.KBN_CD      = MG.STD_IRIME_UT                               " & vbNewLine _
                            & "   --引当優先度(区分10)                                                " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_10                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_10.KBN_GROUP_CD = 'W001'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_10.KBN_CD      = INS.ALLOC_PRIORITY                            " & vbNewLine _
                            & "   --商品状態中身(区分11)                                              " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_11                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_11.KBN_GROUP_CD = 'S005'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_11.KBN_CD      = INS.GOODS_COND_KB_1                           " & vbNewLine _
                            & "   --商品状態外観(区分12)                                              " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_12                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_12.KBN_GROUP_CD = 'S006'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_12.KBN_CD      = INS.GOODS_COND_KB_2                           " & vbNewLine _
                            & "   --簿外(区分13)                                                      " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_13                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_13.KBN_GROUP_CD = 'B002'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_13.KBN_CD      = INS.OFB_KB                                    " & vbNewLine _
                            & "   --保留(区分14)                                                      " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_14                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_14.KBN_GROUP_CD = 'H003'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_14.KBN_CD      = INS.SPD_KB                                    " & vbNewLine _
                            & "   --課税区分(区分15)                                                  " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_15                                   " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "    KBN_15.KBN_GROUP_CD = 'Z001'                                       " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "    KBN_15.KBN_CD      = INL.TAX_KB                                    " & vbNewLine _
                            & "   --荷主明細マスタ                                                    " & vbNewLine _
                            & "   LEFT JOIN  $LM_MST$..M_CUST_DETAILS CUST_D                          " & vbNewLine _
                            & "   ON                                                                  " & vbNewLine _
                            & "	CUST_D.NRS_BR_CD = INL.NRS_BR_CD                                      " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "	LEFT(CUST_D.CUST_CD , 8) = INL.CUST_CD_L + INL.CUST_CD_M              " & vbNewLine _
                            & "   AND                                                                 " & vbNewLine _
                            & "   	CUST_D.SUB_KB = '08'                                              " & vbNewLine _
                            & "   --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 --- START ---                 " & vbNewLine _
                            & "   --商品明細M                                                         " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='20' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_UN                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_UN.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_UN.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --商品明細M                                                                          " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='21' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_NM                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_NM.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_NM.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --商品明細M                                                                          " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='22' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_IM                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_IM.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_IM.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --商品明細M                                                                          " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='23' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_PG                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_PG.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_PG.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --商品明細M                                                                          " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='24' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_FP                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_FP.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_FP.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --商品明細M                                                                          " & vbNewLine _
                            & "   LEFT JOIN (SELECT NRS_BR_CD,GOODS_CD_NRS,MAX(SET_NAIYO) AS SET_NAIYO                 " & vbNewLine _
                            & "                FROM $LM_MST$..M_GOODS_DETAILS  WHERE SUB_KB ='25' AND SYS_DEL_FLG ='0' " & vbNewLine _
                            & "               GROUP BY  NRS_BR_CD,GOODS_CD_NRS ) MGD_EL                                " & vbNewLine _
                            & "          ON MG.NRS_BR_CD = MGD_EL.NRS_BR_CD                                            " & vbNewLine _
                            & "         AND MG.GOODS_CD_NRS = MGD_EL.GOODS_CD_NRS                                      " & vbNewLine _
                            & "   --(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 ---  END  ---                                  " & vbNewLine _
                            & "   --荷主マスタ                                                         " & vbNewLine _
                            & "   LEFT JOIN $LM_MST$..M_CUST CUST2                                     " & vbNewLine _
                            & "          ON  CUST2.NRS_BR_CD    = MG.NRS_BR_CD                         " & vbNewLine _
                            & "          AND CUST2.CUST_CD_L    = MG.CUST_CD_L                         " & vbNewLine _
                            & "          AND CUST2.CUST_CD_M    = MG.CUST_CD_M                         " & vbNewLine _
                            & "          AND CUST2.CUST_CD_S    = MG.CUST_CD_S                         " & vbNewLine _
                            & "          AND CUST2.CUST_CD_SS   = '00'                                 " & vbNewLine _
                            & "   WHERE INL.NRS_BR_CD  = @NRS_BR_CD                                    " & vbNewLine _
                            & "    AND INL.INKA_NO_L   = @INKA_NO_L                                    " & vbNewLine _
                            & "    AND INL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  ) AS BASE2                                                            " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO ML01                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_L01 = ML01.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO ML02                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_L02 = ML02.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO ML03                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_L03 = ML03.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO ML04                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_L04 = ML04.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO ML05                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_L05 = ML05.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO MM01                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_M01 = MM01.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO MM02                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_M02 = MM02.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO MM03                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_M03 = MM03.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO MM04                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_M04 = MM04.SAGYO_CD                                " & vbNewLine _
                            & "                                                                        " & vbNewLine _
                            & "  LEFT JOIN                                                             " & vbNewLine _
                            & "  $LM_MST$..M_SAGYO MM05                                                " & vbNewLine _
                            & "  ON  BASE2.SAGYO_CD_M05 = MM05.SAGYO_CD                                " & vbNewLine




    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_Mprt As String = "FROM                                                       " & vbNewLine _
                                            & "--入荷L                                                  " & vbNewLine _
                                            & "$LM_TRN$..B_INKA_L INL                                   " & vbNewLine _
                                            & "LEFT join $LM_TRN$..B_INKA_M INM                         " & vbNewLine _
                                            & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                        " & vbNewLine _
                                            & "AND  INL.INKA_NO_L = INM.INKA_NO_L                       " & vbNewLine _
                                            & "--入荷M                                                  " & vbNewLine _
                                            & "LEFT join $LM_TRN$..B_INKA_S INS                         " & vbNewLine _
                                            & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                        " & vbNewLine _
                                            & "AND  INM.INKA_NO_L = INS.INKA_NO_L                       " & vbNewLine _
                                            & "AND INM.INKA_NO_M = INS.INKA_NO_M                        " & vbNewLine _
                                            & "AND INM.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                            & "--入荷S                                                  " & vbNewLine _
                                            & "LEFT join $LM_MST$..M_GOODS MG                           " & vbNewLine _
                                            & "ON  INM.NRS_BR_CD = MG.NRS_BR_CD                         " & vbNewLine _
                                            & "AND  INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                  " & vbNewLine _
                                            & "AND INS.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                            & "--運送L                                                  " & vbNewLine _
                                            & "LEFT join $LM_TRN$..F_UNSO_L UL                          " & vbNewLine _
                                            & "ON  INL.NRS_BR_CD = UL.NRS_BR_CD                         " & vbNewLine _
                                            & "AND  INL.INKA_NO_L = UL.INOUTKA_NO_L                     " & vbNewLine _
                                            & "AND  UL.MOTO_DATA_KB = '10'                              " & vbNewLine _
                                            & "AND UL.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                            & "--入荷Lでの荷主帳票パターン取得                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                      " & vbNewLine _
                                            & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                            & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                       " & vbNewLine _
                                            & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                       " & vbNewLine _
                                            & "AND '00' = MCR1.CUST_CD_S                                " & vbNewLine _
                                            & "AND MCR1.PTN_ID = '02'                                   " & vbNewLine _
                                            & "--帳票パターン取得                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1                            " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                            & "AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                            & "AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & "--商品Mの荷主での荷主帳票パターン取得                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                      " & vbNewLine _
                                            & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                        " & vbNewLine _
                                            & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                        " & vbNewLine _
                                            & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                        " & vbNewLine _
                                            & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                        " & vbNewLine _
                                            & "AND MCR2.PTN_ID = '02'                                   " & vbNewLine _
                                            & "--帳票パターン取得                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                            " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                            & "AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                            & "AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                            & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                        " & vbNewLine _
                                            & "AND MR3.PTN_ID = '02'                                    " & vbNewLine _
                                            & "AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                            & "AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & "	  WHERE INL.NRS_BR_CD  = @NRS_BR_CD                  	" & vbNewLine _
                                            & "	   AND INL.INKA_NO_L   = @INKA_NO_L                  	" & vbNewLine _
                                            & "	   AND INL.SYS_DEL_FLG = '0'                         	" & vbNewLine


    ''' <summary>
    ''' ORDER BY（①入荷日、②管理番号）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                            " & vbNewLine _
                                         & "     BASE2.NRS_BR_CD                                " & vbNewLine _
                                         & "    ,BASE2.INKA_NO_L                                " & vbNewLine _
                                         & "    ,BASE2.PRINT_SORT                               " & vbNewLine _
                                         & "    ,BASE2.INKA_NO_M                                " & vbNewLine _
                                         & "    ,BASE2.INKA_NO_S                                " & vbNewLine

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB514DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMB514DAC.SQL_FROM_Mprt)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB510DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB510IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")      '(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 [追加]

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB514DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB514DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMB514DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB514DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("USER_NM", "USER_NM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        map.Add("INKA_TP", "INKA_TP")
        map.Add("INKA_KB", "INKA_KB")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("INKA_PLAN_QT", "INKA_PLAN_QT")
        map.Add("INKA_PLAN_QT_UT", "INKA_PLAN_QT_UT")
        map.Add("UKETSUKE_USER", "UKETSUKE_USER")
        map.Add("UNCHIN_TP", "UNCHIN_TP")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("TARIFF_CD", "TARIFF_CD")
        map.Add("TARIFF_REM", "TARIFF_REM")
        'map.Add("YOKO_REM", "YOKO_REM")
        map.Add("PAY_UNCHIN", "PAY_UNCHIN")
        map.Add("REMARK_HED", "REMARK_HED")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("REMARK_MEI", "REMARK_MEI")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("REMARK_SYO", "REMARK_SYO")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("SAGYO_RYAK_HED_1", "SAGYO_RYAK_HED_1")
        map.Add("SAGYO_RYAK_HED_2", "SAGYO_RYAK_HED_2")
        map.Add("SAGYO_RYAK_HED_3", "SAGYO_RYAK_HED_3")
        map.Add("SAGYO_RYAK_HED_4", "SAGYO_RYAK_HED_4")
        map.Add("SAGYO_RYAK_HED_5", "SAGYO_RYAK_HED_5")
        map.Add("SAGYO_RYAK_MEI_1", "SAGYO_RYAK_MEI_1")
        map.Add("SAGYO_RYAK_MEI_2", "SAGYO_RYAK_MEI_2")
        map.Add("SAGYO_RYAK_MEI_3", "SAGYO_RYAK_MEI_3")
        map.Add("SAGYO_RYAK_MEI_4", "SAGYO_RYAK_MEI_4")
        map.Add("SAGYO_RYAK_MEI_5", "SAGYO_RYAK_MEI_5")
        map.Add("WT", "WT")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("LOT_SP_KB", "LOT_SP_KB")
        '(2012.01.17) SHINOHARA ADD START
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        '(2012.01.17) SHINOHARA ADD END

        '(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 --- START ---
        map.Add("UTI_UN", "UTI_UN")
        map.Add("UTI_NM", "UTI_NM")
        map.Add("UTI_IM", "UTI_IM")
        map.Add("UTI_PG", "UTI_PG")
        map.Add("UTI_FP", "UTI_FP")
        map.Add("UTI_EL", "UTI_EL")
        '(2013.01.09)要望番号1727 ﾕｰﾃｨｱｲ対応 ---  END  ---
        map.Add("CUST_NM_S", "CUST_NM_S")

        '20151028 tsunehira add start
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        '20151028 tsunehira add end

        '2016.01.28 LMB515対応START
        map.Add("NB_UT", "NB_UT")
        '2016.01.28 LMB515対応END

        '20160603 シンガポール対応 tsunehira add start
        map.Add("KONSU_SHOSU", "KONSU_SHOSU")
        '20160603 シンガポール対応 tsunehira add end

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB510OUT")

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

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("INKA_NO_L").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))

            '印刷ユーザー
            whereStr = .Item("USER_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

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
