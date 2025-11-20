' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL SELECT句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MONTHLYSNZ As String = " SELECT                                                                     " & vbNewLine _
                                                  & " MONTHLYSNZ.DEL_KB                            AS DEL_KB                     " & vbNewLine _
                                                  & ",MONTHLYSNZ.NRS_BR_CD                         AS NRS_BR_CD                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.JISSEKI_DATE                      AS JISSEKI_DATE               " & vbNewLine _
                                                  & ",MONTHLYSNZ.ZAI_REC_NO                        AS ZAI_REC_NO                 " & vbNewLine _
                                                  & ",MONTHLYSNZ.GOODS_CD_CUST                     AS GOODS_CD_CUST              " & vbNewLine _
                                                  & ",MONTHLYSNZ.GOODS_NM                          AS GOODS_NM                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.LOT_NO                            AS LOT_NO                     " & vbNewLine _
                                                  & ",MONTHLYSNZ.QT                                AS QT                         " & vbNewLine _
                                                  & ",MONTHLYSNZ.IRIME                             AS IRIME                      " & vbNewLine _
                                                  & ",MONTHLYSNZ.LOCA                              AS LOCA                       " & vbNewLine _
                                                  & ",MONTHLYSNZ.ORDER_NO                          AS ORDER_NO                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.TANAOROSHI_BI                     AS TANAOROSHI_BI              " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_FLG                          AS SEND_FLG                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_USER                         AS SEND_USER                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_DATE                         AS SEND_DATE                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_TIME                         AS SEND_TIME                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_USER                          AS CRT_USER                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_DATE                          AS CRT_DATE                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_TIME                          AS CRT_TIME                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_USER                          AS UPD_USER                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_DATE                          AS UPD_DATE                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_TIME                          AS UPD_TIME                   " & vbNewLine

#End Region

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL FROM句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MONTHLYSNZ As String = "FROM                                                                    " & vbNewLine _
                                                       & "$LM_TRN$..H_SENDMONTHLY_SNZ MONTHLYSNZ                                  " & vbNewLine

#End Region

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL WHERE句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_MONTHLYSNZ As String = "WHERE MONTHLYSNZ.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                                        & "  AND MONTHLYSNZ.JISSEKI_DATE = @JISSEKI_DATE                               " & vbNewLine _
                                                        & "  AND MONTHLYSNZ.SYS_DEL_FLG = '0'                                          " & vbNewLine

#End Region

#End Region

#End Region

#Region "月末在庫データの検索"

#Region "月末在庫データの検索 SQL"

    ''' <summary>
    ''' 月末在庫データの検索 SQL 長いSQLで分けると、意味がわからなくなりそうなので、あえて分けずに記載
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIZAN As String = " SELECT                                                                      " & vbNewLine _
                                              & "         MAX(OUTPUT_DATE.NRS_BR_CD)   AS NRS_BR_CD                           " & vbNewLine _
                                              & "       , @JISSEKI_DATE                AS JISSEKI_DATE                        " & vbNewLine _
                                              & "       , MAX(OUTPUT_DATE.ZAI_REC_NO)  AS ZAI_REC_NO                          " & vbNewLine _
                                              & "       , OUTPUT_DATE.GOODS_CD_CUST    AS GOODS_CD_CUST                       " & vbNewLine _
                                              & "       , MAX(OUTPUT_DATE.GOODS_NM)    AS GOODS_NM                            " & vbNewLine _
                                              & "       , OUTPUT_DATE.LOT_NO           AS LOT_NO                              " & vbNewLine _
                                              & "       , SUM(OUTPUT_DATE.PORA_ZAI_QT) AS QT                                  " & vbNewLine _
                                              & "       , OUTPUT_DATE.IRIME            AS IRIME                               " & vbNewLine _
                                              & "       , OUTPUT_DATE.KBN_NM2          AS LOCA                                " & vbNewLine _
                                              & "       , ''                           AS ORDER_NO                            " & vbNewLine _
                                              & "       , SUBSTRING(@JISSEKI_DATE,3,6) AS TANAOROSHI_BI                       " & vbNewLine _
                                              & " FROM (SELECT                                                                " & vbNewLine _
                                              & "               BASE_DATE.NRS_BR_CD   AS NRS_BR_CD                            " & vbNewLine _
                                              & "             , BASE_DATE.ZAI_REC_NO  AS ZAI_REC_NO                           " & vbNewLine _
                                              & "             , MG.GOODS_CD_CUST      AS GOODS_CD_CUST                        " & vbNewLine _
                                              & "             , MG.GOODS_NM_2         AS GOODS_NM                             " & vbNewLine _
                                              & "             , ZAITRS.LOT_NO         AS LOT_NO                               " & vbNewLine _
                                              & "             , BASE_DATE.PORA_ZAI_QT AS PORA_ZAI_QT                          " & vbNewLine _
                                              & "             , ZAITRS.IRIME          AS IRIME                                " & vbNewLine _
                                              & "             , Z1.KBN_NM2            AS KBN_NM2                              " & vbNewLine _
                                              & "       FROM (SELECT                                                          " & vbNewLine _
                                              & "                     NRS_BR_CD   AS NRS_BR_CD                                " & vbNewLine _
                                              & "                   , CUST_CD_L   AS CUST_CD_L                                " & vbNewLine _
                                              & "                   , CUST_CD_M   AS CUST_CD_M                                " & vbNewLine _
                                              & "                   , ZAI_REC_NO  AS ZAI_REC_NO                               " & vbNewLine _
                                              & "                   , PORA_ZAI_NB AS PORA_ZAI_NB                              " & vbNewLine _
                                              & "                   , PORA_ZAI_QT AS PORA_ZAI_QT                              " & vbNewLine _
                                              & "             FROM (SELECT                                                    " & vbNewLine _
                                              & "                           NRS_BR_CD        AS NRS_BR_CD                     " & vbNewLine _
                                              & "                         , CUST_CD_L        AS CUST_CD_L                     " & vbNewLine _
                                              & "                         , CUST_CD_M        AS CUST_CD_M                     " & vbNewLine _
                                              & "                         , ZAI_REC_NO       AS ZAI_REC_NO                    " & vbNewLine _
                                              & "                         , SUM(PORA_ZAI_NB) AS PORA_ZAI_NB                   " & vbNewLine _
                                              & "                         , SUM(PORA_ZAI_QT) AS PORA_ZAI_QT                   " & vbNewLine _
                                              & "                   FROM                                                      " & vbNewLine _
                                              & "                        --①入荷データ(B_INKA_S)                             " & vbNewLine _
                                              & "                        (SELECT ''                                                    AS OUTKA_NO_L" & vbNewLine _
                                              & "                               , INL1.CUST_CD_L                                       AS CUST_CD_L" & vbNewLine _
                                              & "                               , INL1.CUST_CD_M                                       AS CUST_CD_M" & vbNewLine _
                                              & "                               , INS1.ZAI_REC_NO                                      AS ZAI_REC_NO" & vbNewLine _
                                              & "                               , (INS1.KONSU * MG1.PKG_NB) + INS1.HASU                AS PORA_ZAI_NB" & vbNewLine _
                                              & "                               , ((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME AS PORA_ZAI_QT" & vbNewLine _
                                              & "                               , INS1.NRS_BR_CD                                       AS NRS_BR_CD" & vbNewLine _
                                              & "                         FROM                                                " & vbNewLine _
                                              & "                              $LM_TRN$..B_INKA_L INL1                        " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..B_INKA_M INM1              " & vbNewLine _
                                              & "                              ON INM1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND INM1.NRS_BR_CD = INL1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND INM1.INKA_NO_L = INL1.INKA_NO_L            " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..B_INKA_S INS1              " & vbNewLine _
                                              & "                              ON INS1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND INS1.NRS_BR_CD = INM1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND INS1.INKA_NO_L = INM1.INKA_NO_L            " & vbNewLine _
                                              & "                              AND INS1.INKA_NO_M = INM1.INKA_NO_M            " & vbNewLine _
                                              & "                              LEFT JOIN $LM_MST$..M_GOODS MG1                " & vbNewLine _
                                              & "                              ON  MG1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND MG1.NRS_BR_CD = INL1.NRS_BR_CD             " & vbNewLine _
                                              & "                              AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS       " & vbNewLine _
                                              & "                         WHERE INL1.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND INL1.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')" & vbNewLine _
                                              & "                              AND INL1.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND INL1.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND INL1.INKA_DATE  <= @JISSEKI_DATE           " & vbNewLine _
                                              & "                              AND (INL1.INKA_DATE > '00000000' OR INL1.INKA_STATE_KB < '50')" & vbNewLine _
                                              & "                         --②在庫移動分を加減算(D_IDO_TRS)                   " & vbNewLine _
                                              & "                         --②移動後                                          " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT ''                                 AS OUTKA_NO_L  " & vbNewLine _
                                              & "                               , ZAI4.CUST_CD_L                    AS CUST_CD_L   " & vbNewLine _
                                              & "                               , ZAI4.CUST_CD_M                    AS CUST_CD_M   " & vbNewLine _
                                              & "                               , IDO1.N_ZAI_REC_NO                 AS ZAI_REC_NO  " & vbNewLine _
                                              & "                               , IDO1.N_PORA_ZAI_NB                AS PORA_ZAI_NB " & vbNewLine _
                                              & "                               , IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME AS PORA_ZAI_QT " & vbNewLine _
                                              & "                               , IDO1.NRS_BR_CD                    AS NRS_BR_CD   " & vbNewLine _
                                              & "                         FROM $LM_TRN$..D_IDO_TRS IDO1                       " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4             " & vbNewLine _
                                              & "                              ON  ZAI4.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "                              AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO        " & vbNewLine _
                                              & "                              LEFT JOIN $LM_MST$..M_GOODS MG                 " & vbNewLine _
                                              & "                              ON  ZAI4.NRS_BR_CD = MG.NRS_BR_CD              " & vbNewLine _
                                              & "                              AND ZAI4.GOODS_CD_NRS = MG.GOODS_CD_NRS        " & vbNewLine _
                                              & "                         WHERE IDO1.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND IDO1.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND ZAI4.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND ZAI4.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND IDO1.IDO_DATE   <= @JISSEKI_DATE           " & vbNewLine _
                                              & "                         --②移動前                                          " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT ''                                      AS OUTKA_NO_L  " & vbNewLine _
                                              & "                               , ZAI5.CUST_CD_L                         AS CUST_CD_L   " & vbNewLine _
                                              & "                               , ZAI5.CUST_CD_M                         AS CUST_CD_M   " & vbNewLine _
                                              & "                               , IDO2.O_ZAI_REC_NO                      AS ZAI_REC_NO  " & vbNewLine _
                                              & "                               , IDO2.N_PORA_ZAI_NB * -1                AS PORA_ZAI_NB " & vbNewLine _
                                              & "                               , IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1 AS PORA_ZAI_QT " & vbNewLine _
                                              & "                               , IDO2.NRS_BR_CD                         AS NRS_BR_CD   " & vbNewLine _
                                              & "                         FROM $LM_TRN$..D_IDO_TRS IDO2                       " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5             " & vbNewLine _
                                              & "                              ON ZAI5.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO        " & vbNewLine _
                                              & "                              LEFT JOIN $LM_MST$..M_GOODS MG                 " & vbNewLine _
                                              & "                              ON ZAI5.NRS_BR_CD = MG.NRS_BR_CD               " & vbNewLine _
                                              & "                              AND ZAI5.GOODS_CD_NRS = MG.GOODS_CD_NRS        " & vbNewLine _
                                              & "                         WHERE IDO2.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND IDO2.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND ZAI5.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND ZAI5.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND IDO2.IDO_DATE   <= @JISSEKI_DATE           " & vbNewLine _
                                              & "                         --③出荷データ(C_OUTKA_S)                           " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT                                              " & vbNewLine _
                                              & "                                 OUTKA_NO_L                                  " & vbNewLine _
                                              & "                               , CUST_CD_L                                   " & vbNewLine _
                                              & "                               , CUST_CD_M                                   " & vbNewLine _
                                              & "                               , ZAI_REC_NO                                  " & vbNewLine _
                                              & "                               , PORA_ZAI_NB                                 " & vbNewLine _
                                              & "                               , PORA_ZAI_QT                                 " & vbNewLine _
                                              & "                               , NRS_BR_CD                                   " & vbNewLine _
                                              & "                         FROM                                                " & vbNewLine _
                                              & "                             (SELECT                                         " & vbNewLine _
                                              & "                                      OUTS.OUTKA_NO_L         AS OUTKA_NO_L  " & vbNewLine _
                                              & "                                    , OUTL.CUST_CD_L          AS CUST_CD_L   " & vbNewLine _
                                              & "                                    , OUTL.CUST_CD_M          AS CUST_CD_M   " & vbNewLine _
                                              & "                                    , OUTS.ZAI_REC_NO         AS ZAI_REC_NO  " & vbNewLine _
                                              & "                                    , OUTS.ALCTD_NB * -1      AS PORA_ZAI_NB " & vbNewLine _
                                              & "                                    , OUTS.ALCTD_QT * -1      AS PORA_ZAI_QT " & vbNewLine _
                                              & "                                    , OUTS.NRS_BR_CD          AS NRS_BR_CD   " & vbNewLine _
                                              & "                              FROM $LM_TRN$..C_OUTKA_L OUTL                  " & vbNewLine _
                                              & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM        " & vbNewLine _
                                              & "                                   ON  OUTM.SYS_DEL_FLG = '0'                " & vbNewLine _
                                              & "                                   AND OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                              & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS        " & vbNewLine _
                                              & "                                   ON OUTS.SYS_DEL_FLG  = '0'                " & vbNewLine _
                                              & "                                   AND OUTS.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTS.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                              & "                                   AND OUTS.OUTKA_NO_M   = OUTM.OUTKA_NO_M   " & vbNewLine _
                                              & "                                   LEFT JOIN $LM_MST$..M_GOODS MG            " & vbNewLine _
                                              & "                                   ON OUTM.NRS_BR_CD    = MG.NRS_BR_CD       " & vbNewLine _
                                              & "                                   AND OUTM.GOODS_CD_NRS = MG.GOODS_CD_NRS   " & vbNewLine _
                                              & "                              WHERE OUTL.SYS_DEL_FLG      = '0'              " & vbNewLine _
                                              & "                                   AND OUTM.ALCTD_KB        <> '04'          " & vbNewLine _
                                              & "                                   AND OUTL.OUTKA_STATE_KB  >= '60'          " & vbNewLine _
                                              & "                                   AND OUTL.NRS_BR_CD        = @NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTL.CUST_CD_L        = @CUST_CD_L    " & vbNewLine _
                                              & "                                   AND OUTL.CUST_CD_M        = @CUST_CD_M    " & vbNewLine _
                                              & "                                   AND OUTL.OUTKA_PLAN_DATE <= @JISSEKI_DATE " & vbNewLine _
                                              & "                              ) BASE3                                        " & vbNewLine _
                                              & "                         ) BASE4                                             " & vbNewLine _
                                              & "                   WHERE CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                              & "                         AND CUST_CD_M = @CUST_CD_M                          " & vbNewLine _
                                              & "                   GROUP BY                                                  " & vbNewLine _
                                              & "                         CUST_CD_L                                           " & vbNewLine _
                                              & "                       , CUST_CD_M                                           " & vbNewLine _
                                              & "                       , ZAI_REC_NO                                          " & vbNewLine _
                                              & "                       , NRS_BR_CD                                           " & vbNewLine _
                                              & "             ) BASE                                                          " & vbNewLine _
                                              & "       WHERE (BASE.PORA_ZAI_NB > 0 OR BASE.PORA_ZAI_QT > 0)                  " & vbNewLine _
                                              & "       ) BASE_DATE                                                           " & vbNewLine _
                                              & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                        " & vbNewLine _
                                              & " ON ZAITRS.NRS_BR_CD    = BASE_DATE.NRS_BR_CD                                " & vbNewLine _
                                              & " AND ZAITRS.ZAI_REC_NO   = BASE_DATE.ZAI_REC_NO                              " & vbNewLine _
                                              & " AND ZAITRS.SYS_DEL_FLG  = '0'                                               " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..M_GOODS MG                                              " & vbNewLine _
                                              & " ON ZAITRS.NRS_BR_CD    = MG.NRS_BR_CD                                       " & vbNewLine _
                                              & " AND ZAITRS.GOODS_CD_NRS = MG.GOODS_CD_NRS                                   " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..M_CUST AS CUST                                          " & vbNewLine _
                                              & " ON MG.NRS_BR_CD   = CUST.NRS_BR_CD                                          " & vbNewLine _
                                              & " AND MG.CUST_CD_L   = CUST.CUST_CD_L                                         " & vbNewLine _
                                              & " AND MG.CUST_CD_M   = CUST.CUST_CD_M                                         " & vbNewLine _
                                              & " AND MG.CUST_CD_S   = CUST.CUST_CD_S                                         " & vbNewLine _
                                              & " AND MG.CUST_CD_SS  = CUST.CUST_CD_SS                                        " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..Z_KBN Z1                                                " & vbNewLine _
                                              & " ON Z1.KBN_GROUP_CD = 'O002'                                                 " & vbNewLine _
                                              & " AND Z1.KBN_CD = MG.ONDO_KB                                                  " & vbNewLine _
                                              & " ) OUTPUT_DATE                                                               " & vbNewLine _
                                              & " GROUP BY                                                                    " & vbNewLine _
                                              & "       OUTPUT_DATE.GOODS_CD_CUST                                             " & vbNewLine _
                                              & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                              & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine _
                                              & "     , OUTPUT_DATE.KBN_NM2                                                   " & vbNewLine _
                                              & " ORDER BY                                                                    " & vbNewLine _
                                              & "       OUTPUT_DATE.GOODS_CD_CUST                                             " & vbNewLine _
                                              & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                              & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine _
                                              & "     , OUTPUT_DATE.KBN_NM2                                                   " & vbNewLine

#End Region

#End Region

#Region "篠崎運送専用の月末在庫テーブル削除処理"

#Region "篠崎運送専用の月末在庫テーブル削除 SQL"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_MONTHLYSNZ As String = "DELETE FROM $LM_TRN$..H_SENDMONTHLY_SNZ                                     " & vbNewLine _
                                                  & " WHERE NRS_BR_CD    = @NRS_BR_CD                                            " & vbNewLine _
                                                  & "   AND JISSEKI_DATE = @JISSEKI_DATE                                         " & vbNewLine

#End Region

#End Region

#Region "篠崎運送専用の月末在庫テーブル作成処理"

#Region "篠崎運送専用の月末在庫テーブル作成 SQL"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_MONTHLYSNZ As String = "INSERT INTO $LM_TRN$..H_SENDMONTHLY_SNZ            " & vbNewLine _
                                                  & " ( 		                                        " & vbNewLine _
                                                  & " DEL_KB,                                           " & vbNewLine _
                                                  & " NRS_BR_CD,                                        " & vbNewLine _
                                                  & " JISSEKI_DATE,                                     " & vbNewLine _
                                                  & " ZAI_REC_NO,                                       " & vbNewLine _
                                                  & " GOODS_CD_CUST,                                    " & vbNewLine _
                                                  & " GOODS_NM,                                         " & vbNewLine _
                                                  & " LOT_NO,                                           " & vbNewLine _
                                                  & " QT,                                               " & vbNewLine _
                                                  & " IRIME,                                            " & vbNewLine _
                                                  & " LOCA,                                             " & vbNewLine _
                                                  & " ORDER_NO,                                         " & vbNewLine _
                                                  & " TANAOROSHI_BI,                                    " & vbNewLine _
                                                  & " SEND_FLG,                                         " & vbNewLine _
                                                  & " SEND_USER,                                        " & vbNewLine _
                                                  & " SEND_DATE,                                        " & vbNewLine _
                                                  & " SEND_TIME,                                        " & vbNewLine _
                                                  & " CRT_USER,                                         " & vbNewLine _
                                                  & " CRT_DATE,                                         " & vbNewLine _
                                                  & " CRT_TIME,                                         " & vbNewLine _
                                                  & " UPD_USER,                                         " & vbNewLine _
                                                  & " UPD_DATE,                                         " & vbNewLine _
                                                  & " UPD_TIME,                                         " & vbNewLine _
                                                  & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                  & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                  & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                  & " SYS_ENT_USER,                                     " & vbNewLine _
                                                  & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                  & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                  & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                  & " SYS_UPD_USER,                                     " & vbNewLine _
                                                  & " SYS_DEL_FLG                                       " & vbNewLine _
                                                  & " ) VALUES (                                        " & vbNewLine _
                                                  & " @DEL_KB,                                          " & vbNewLine _
                                                  & " @NRS_BR_CD,                                       " & vbNewLine _
                                                  & " @JISSEKI_DATE,                                    " & vbNewLine _
                                                  & " @ZAI_REC_NO,                                      " & vbNewLine _
                                                  & " @GOODS_CD_CUST,                                   " & vbNewLine _
                                                  & " @GOODS_NM,                                        " & vbNewLine _
                                                  & " @LOT_NO,                                          " & vbNewLine _
                                                  & " @QT,                                              " & vbNewLine _
                                                  & " @IRIME,                                           " & vbNewLine _
                                                  & " @LOCA,                                            " & vbNewLine _
                                                  & " @ORDER_NO,                                        " & vbNewLine _
                                                  & " @TANAOROSHI_BI,                                   " & vbNewLine _
                                                  & " @SEND_FLG,                                        " & vbNewLine _
                                                  & " @SEND_USER,                                       " & vbNewLine _
                                                  & " @SEND_DATE,                                       " & vbNewLine _
                                                  & " @SEND_TIME,                                       " & vbNewLine _
                                                  & " @CRT_USER,                                        " & vbNewLine _
                                                  & " @CRT_DATE,                                        " & vbNewLine _
                                                  & " @CRT_TIME,                                        " & vbNewLine _
                                                  & " @UPD_USER,                                        " & vbNewLine _
                                                  & " @UPD_DATE,                                        " & vbNewLine _
                                                  & " @UPD_TIME,                                        " & vbNewLine _
                                                  & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                  & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                  & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                  & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                  & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                  & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                  & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                  & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                  & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                  & " )                                                 " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

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

#Region "篠崎運送専用の月末在庫テーブルからデータ取得"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMonthlySnz(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI050IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI050DAC.SQL_SELECT_MONTHLYSNZ)       'SQL構築(Select句)
        Me._StrSql.Append(LMI050DAC.SQL_SELECT_FROM_MONTHLYSNZ)  'SQL構築(From句)
        Me._StrSql.Append(LMI050DAC.SQL_SELECT_WHERE_MONTHLYSNZ) 'SQL構築(Where句)

        'パラメータの設定
        Call SetSQLMonthlySnzParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI050DAC", "SelectMonthlySnz", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("QT", "QT")
        map.Add("IRIME", "IRIME")
        map.Add("LOCA", "LOCA")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("TANAOROSHI_BI", "TANAOROSHI_BI")
        map.Add("SEND_FLG", "SEND_FLG")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI050OUT_SNZ")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "月末在庫データの検索"

    ''' <summary>
    ''' 月末在庫データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiZanSnz(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI050IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI050DAC.SQL_SELECT_ZAIZAN)       'SQL構築

        'パラメータの設定
        Call SetSQLZaiZanParameter(inTbl.Rows(0))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI050DAC", "SelectZaiZanSnz", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("QT", "QT")
        map.Add("IRIME", "IRIME")
        map.Add("LOCA", "LOCA")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("TANAOROSHI_BI", "TANAOROSHI_BI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI050INOUT_ZAIZAN_SNZ")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "篠崎運送専用の月末在庫テーブル削除処理"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataSNZ(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI050IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI050DAC.SQL_DELETE_MONTHLYSNZ)      'SQL構築(Delete句)

        'パラメータの設定
        Call SetSQLMonthlySnzParameter(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI050DAC", "DeleteDataSNZ", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "篠崎運送専用の月末在庫テーブル作成処理"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertDataSNZ(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI050INOUT_ZAIZAN_SNZ")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI050DAC.SQL_INSERT_MONTHLYSNZ)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLMonthlySnzInsParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI050DAC", "InsertDataSNZ", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 篠崎運送専用の月末在庫テーブル"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLMonthlySnzParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))

            '実績日付
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 月末在庫データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLZaiZanParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード(大)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            '荷主コード(中)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            '実績日付
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 篠崎運送専用の月末在庫テーブル(新規追加)"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル(新規追加)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSQLMonthlySnzInsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT", .Item("QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORDER_NO", .Item("ORDER_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TANAOROSHI_BI", .Item("TANAOROSHI_BI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_FLG", "0", DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", String.Concat(Mid(MyBase.GetSystemTime(), 1, 2), ":", Mid(MyBase.GetSystemTime(), 3, 2), ":", Mid(MyBase.GetSystemTime(), 5, 2)), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", String.Empty, DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

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

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
